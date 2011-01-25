﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using BBQRMSSolution.Models;
using BBQRMSSolution.ServerProxy;
using Controls;
using Menu = BBQRMSSolution.ServerProxy.Menu;
using SampleMenu = BBQRMSSolution.Models.Menu;

namespace BBQRMSSolution.ViewModels
{
	public class CustomerOrderScreenViewModel : ViewModelBase
	{
	    private readonly BBQRMSEntities mDataService;
		private readonly ObservableCollection<OrderViewModel> _mPendingOrders;

		public ObservableCollection<SampleMenu> SampleMenus { get; set; }
	    public ObservableCollection<Menu> Menus { get; set; }
		public OrderViewModel Order { get;set;}

		private const decimal TAX_PERCENTAGE = 8.25m;

		public DelegateCommand Cancel { get { return new DelegateCommand(CancelOrder); } }
		public DelegateCommand Cashier { get { return new DelegateCommand(PlaceOrder); } }

		[Obsolete("Used only for design-time")]
		public CustomerOrderScreenViewModel()
		{
			_mPendingOrders = new ObservableCollection<OrderViewModel>();
			var r = new Random();
			Order = new OrderViewModel { OrderNumber = r.Next(1,100) };

			SampleMenus = BuildSampleMenus();
		}

		private ObservableCollection<SampleMenu> BuildSampleMenus()
		{
			return
				new ObservableCollection<SampleMenu>
					{
						new SampleMenu
							{
								Name = "Meats",
								BackColor = "#990000",
								TextColor = "#FFFFFF",
								MenuItems =
									{
										new MenuItem {Name = "Brisket",ImageSource="/Graphics/brisket.jpg", Id=10, Price=6.25m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem {Name = "Ribs",ImageSource="/Graphics/ribs.png", Id=11, Price=7.50m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem {Name = "Pulled Pork",ImageSource="/Graphics/pork.jpg", Id=12, Price=5.75m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem {Name = "Pulled Chicken",ImageSource="/Graphics/chicken.jpg", Id=13, Price=5.25m, DoAction = new DelegateCommand(AddMenuItem)}
									}
							},
						new SampleMenu
							{
								Name = "Sides",
								BackColor = "#009900",
								TextColor = "#FFFFFF",
								MenuItems =
									{
										new MenuItem {Name = "Fries",ImageSource="/Graphics/fries.jpg", Id = 1, Price = 3.50m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem {Name = "Beans",ImageSource="/Graphics/beans.jpg", Id = 2, Price = 4.00m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem {Name = "Potato Salad",ImageSource="/Graphics/potatosalad.jpg", Id = 3, Price = 5.00m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem {Name = "Coleslaw",ImageSource="/Graphics/coleslaw.jpg", Id = 4, Price = 1.25m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem
											{Name = "Corn on the Cob",ImageSource="/Graphics/corn.jpg", Id = 5, Price = 2.25m, DoAction = new DelegateCommand(AddMenuItem)}
									}

							},
						new SampleMenu
							{
								Name = "Drinks",
								BackColor = "#000099",
								TextColor = "#FFFFFF",
								MenuItems =
									{
										new MenuItem {Name = "Soda",ImageSource="/Graphics/soda.jpg", Id = 6, Price = 1.25m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem {Name = "Beer",ImageSource="/Graphics/beer.jpg", Id = 7, Price = 2.25m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem
											{Name = "Bottled Water",ImageSource="/Graphics/water.jpg", Id = 8, Price = 1.00m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem {Name = "Sobe",ImageSource="/Graphics/sobe.jpg", Id = 9, Price = 1.99m, DoAction = new DelegateCommand(AddMenuItem)}
									}
							}
					};
		}

		private readonly IMessageBus mMessageBus;
		public CustomerOrderScreenViewModel(BBQRMSEntities dataService, ObservableCollection<OrderViewModel> pendingOrders, IMessageBus navigationService)
		{
			mDataService = dataService;
			_mPendingOrders = pendingOrders;
			mMessageBus = navigationService;

			Order = new OrderViewModel();

		    Menus = new ObservableCollection<Menu>(mDataService.Menus.Execute());
		}
		
		public void AddMenuItem(Object mi)
		{
			var menuItem = (MenuItem) mi;

			var isFound = true;
			foreach (var oi in Order.Items)
			{
				isFound = true;
				if (oi.MenuItem.Id == menuItem.Id)
				{
					oi.Quantity++;
					NotifyPropertyChanged("oi");
					break;
				}
				isFound = false;
			}
			if (!isFound || Order.Items.Count == 0)
			{
				var orderItem = new OrderItem { MenuItem = menuItem, Quantity = 1, DoAction=new DelegateCommand(RemoveMenuItem) };
				Order.Items.Add(orderItem);
			}

			Order.SubTotal += menuItem.Price;
			Order.TaxAmount = Order.SubTotal * (TAX_PERCENTAGE / 100);
			Order.TotalPrice = Order.SubTotal + Order.TaxAmount;
		}

		public void RemoveMenuItem(Object oi)
		{
			var orderItem = (OrderItem)oi;

			if (orderItem.Quantity > 1) orderItem.Quantity--;
			else Order.Items.Remove(orderItem);

			Order.SubTotal -= orderItem.MenuItem.Price;
			Order.TaxAmount = Order.SubTotal * (TAX_PERCENTAGE / 100);
			Order.TotalPrice = Order.SubTotal + Order.TaxAmount;
		}

		public void CancelOrder()
		{
			Order = new OrderViewModel();
			NotifyPropertyChanged("order");
		}

		public void PlaceOrder()
		{
			_mPendingOrders.Add(Order);
			var orderCashierScreenViewModel = new OrderCashierScreenViewModel(Order,mMessageBus);

			Order = new OrderViewModel();
			NotifyPropertyChanged("order");

			mMessageBus.Publish(new ShowScreen(orderCashierScreenViewModel));
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using BBQRMSSolution.Models;

namespace BBQRMSSolution.ViewModels
{
	public class CustomerOrderScreenViewModel : ViewModelBase
	{
		private ObservableCollection<OrderViewModel> mPendingOrders;

		public ObservableCollection<Menu> menus { get; set; }
		public OrderViewModel order { get;set;}

		private const decimal TAX_PERCENTAGE = 8.25m;

		public DelegateCommand Cancel { get { return new DelegateCommand(cancelOrder); } }
		public DelegateCommand Cashier { get { return new DelegateCommand(placeOrder); } }

		[Obsolete("Used only for design-time")]
		public CustomerOrderScreenViewModel()
		{
			mPendingOrders = new ObservableCollection<OrderViewModel>();
			order = new OrderViewModel();

			menus = BuildSampleMenus();
		}

		private ObservableCollection<Menu> BuildSampleMenus()
		{
			return
				new ObservableCollection<Menu>
					{
						new Menu
							{
								name = "Meats",
								menuItems =
									{
										new MenuItem {name = "Brisket", id=10, price=6.25m, DoAction = new DelegateCommand(addMenuItem)},
										new MenuItem {name = "Ribs", id=11, price=7.50m, DoAction = new DelegateCommand(addMenuItem)},
										new MenuItem {name = "Pulled Pork", id=12, price=5.75m, DoAction = new DelegateCommand(addMenuItem)},
										new MenuItem {name = "Pulled Chicken", id=13, price=5.25m, DoAction = new DelegateCommand(addMenuItem)}
									}
							},
						new Menu
							{
								name = "Sides",
								menuItems =
									{
										new MenuItem {name = "Fries", id = 1, price = 3.50m, DoAction = new DelegateCommand(addMenuItem)},
										new MenuItem {name = "Beans", id = 2, price = 4.00m, DoAction = new DelegateCommand(addMenuItem)},
										new MenuItem {name = "Potato Salad", id = 3, price = 5.00m, DoAction = new DelegateCommand(addMenuItem)},
										new MenuItem {name = "Coleslaw", id = 4, price = 1.25m, DoAction = new DelegateCommand(addMenuItem)},
										new MenuItem
											{name = "Corn on the Cob", id = 5, price = 2.25m, DoAction = new DelegateCommand(addMenuItem)}
									}

							},
						new Menu
							{
								name = "Drinks",
								menuItems =
									{
										new MenuItem {name = "Soda", id = 6, price = 1.25m, DoAction = new DelegateCommand(addMenuItem)},
										new MenuItem {name = "Beer", id = 7, price = 2.25m, DoAction = new DelegateCommand(addMenuItem)},
										new MenuItem
											{name = "Bottled Water", id = 8, price = 1.00m, DoAction = new DelegateCommand(addMenuItem)},
										new MenuItem {name = "Sobe", id = 9, price = 1.99m, DoAction = new DelegateCommand(addMenuItem)}
									}
							}
					};
		}

		public CustomerOrderScreenViewModel(ObservableCollection<OrderViewModel> pendingOrders)
		{
			mPendingOrders = pendingOrders;
			order = new OrderViewModel();
			
			menus = BuildSampleMenus();
		}
		
		public void addMenuItem(Object mi)
		{
			MenuItem menuItem = (MenuItem)mi;

			bool isFound = true;
			foreach (OrderItem oi in order.Items)
			{
				isFound = true;
				if (oi.menuItem.id == menuItem.id)
				{
					oi.quantity++;
					NotifyPropertyChanged("oi");
					break;
				}
				else isFound = false;
			}
			if (!isFound || order.Items.Count == 0)
			{
				OrderItem orderItem = new OrderItem { menuItem = menuItem, quantity = 1, DoAction=new DelegateCommand(removeMenuItem) };
				order.Items.Add(orderItem);
			}

			order.subTotal += menuItem.price;
			order.taxAmount = order.subTotal * (TAX_PERCENTAGE / (decimal)100);
			order.totalPrice = order.subTotal + order.taxAmount;
		}

		public void removeMenuItem(Object oi)
		{
			OrderItem orderItem = (OrderItem)oi;

			if (orderItem.quantity > 1) orderItem.quantity--;
			else order.Items.Remove(orderItem);

			order.subTotal -= orderItem.menuItem.price;
			order.taxAmount = order.subTotal * (TAX_PERCENTAGE / (decimal)100);
			order.totalPrice = order.subTotal + order.taxAmount;
		}

		public void cancelOrder()
		{
			order = new OrderViewModel();
			NotifyPropertyChanged("order");
		}

		public void placeOrder()
		{
			mPendingOrders.Add(order);
			order = new OrderViewModel();
			NotifyPropertyChanged("order");
		}
	}
}

using System;
using System.Collections.ObjectModel;
using System.Drawing;
using BBQRMSSolution.Models;

namespace BBQRMSSolution.ViewModels
{
	public class CustomerOrderScreenViewModel : ViewModelBase
	{
		private readonly ObservableCollection<OrderViewModel> _mPendingOrders;

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

			Menus = BuildSampleMenus();
		}

		private ObservableCollection<Menu> BuildSampleMenus()
		{
			return
				new ObservableCollection<Menu>
					{
						new Menu
							{
								Name = "Meats",
								BackColor = "#FF0000",
								TextColor = "#FFFFFF",
								MenuItems =
									{
										new MenuItem {Name = "Brisket",ImageSource="/Graphics/brisket.jpg", Id=10, Price=6.25m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem {Name = "Ribs",ImageSource="/Graphics/ribs.png", Id=11, Price=7.50m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem {Name = "Pulled Pork",ImageSource="/Graphics/pork.jpg", Id=12, Price=5.75m, DoAction = new DelegateCommand(AddMenuItem)},
										new MenuItem {Name = "Pulled Chicken",ImageSource="/Graphics/chicken.jpg", Id=13, Price=5.25m, DoAction = new DelegateCommand(AddMenuItem)}
									}
							},
						new Menu
							{
								Name = "Sides",
								BackColor = "#00FF00",
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
						new Menu
							{
								Name = "Drinks",
								BackColor = "#0000FF",
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

		public CustomerOrderScreenViewModel(ObservableCollection<OrderViewModel> pendingOrders)
		{
			_mPendingOrders = pendingOrders;
			Order = new OrderViewModel();

			Menus = BuildSampleMenus();
		}

		private readonly INavigationService _mNavigationService;
		public CustomerOrderScreenViewModel(ObservableCollection<OrderViewModel> pendingOrders, INavigationService navigationService)
		{
			_mPendingOrders = pendingOrders;
			_mNavigationService = navigationService;

			Order = new OrderViewModel();

			Menus = BuildSampleMenus();
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
			var orderCashierScreenViewModel = new OrderCashierScreenViewModel(Order,_mNavigationService);

			Order = new OrderViewModel();
			NotifyPropertyChanged("order");

			_mNavigationService.Content = orderCashierScreenViewModel;
		}
	}
}

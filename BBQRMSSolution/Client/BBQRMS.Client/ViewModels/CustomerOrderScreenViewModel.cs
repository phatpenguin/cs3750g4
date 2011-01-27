using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BBQRMSSolution.Models;
using BBQRMSSolution.ServerProxy;
using Controls;
using Menu = BBQRMSSolution.ServerProxy.Menu;

namespace BBQRMSSolution.ViewModels
{
	public class CustomerOrderScreenViewModel : ViewModelBase
	{
		private readonly BBQRMSEntities mDataService;
		private readonly ObservableCollection<OrderViewModel> _mPendingOrders;

		public ObservableCollection<Menu> Menus { get; set; }
		public OrderViewModel Order { get; set; }

		private const decimal TAX_PERCENTAGE = 8.25m;

		public DelegateCommand Cancel { get { return new DelegateCommand(CancelOrder); } }
		public DelegateCommand Cashier { get { return new DelegateCommand(PlaceOrder); } }

		[Obsolete("Used only for design-time")]
		public CustomerOrderScreenViewModel()
		{
			_mPendingOrders = new ObservableCollection<OrderViewModel>();
			var r = new Random();
			Order = new OrderViewModel { OrderNumber = r.Next(1, 100) };
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
			var menuItem = (MenuItem)mi;

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
				var orderItem = new OrderItem { MenuItem = menuItem, Quantity = 1, DoAction = new DelegateCommand(RemoveMenuItem) };
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
			var orderCashierScreenViewModel = new OrderCashierScreenViewModel(Order, mMessageBus);

			Order = new OrderViewModel();
			NotifyPropertyChanged("order");

			mMessageBus.Publish(new ShowScreen(orderCashierScreenViewModel));
		}
	}
}

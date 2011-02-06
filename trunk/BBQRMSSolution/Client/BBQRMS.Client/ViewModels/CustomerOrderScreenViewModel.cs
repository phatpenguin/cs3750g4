using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.ViewModels.Messages;
using OrderItem = BBQRMSSolution.Models.OrderItem;
using BBQRMSSolution.ServerProxy;
using Controls;
using Menu = BBQRMSSolution.ServerProxy.Menu;
using MenuItem = BBQRMSSolution.ServerProxy.MenuItem;

namespace BBQRMSSolution.ViewModels
{
	public class CustomerOrderScreenViewModel : ViewModelBase
	{
		private readonly BBQRMSEntities _mDataService;
		private readonly ObservableCollection<OrderViewModel> _mPendingOrders;

        public DelegateCommand AddToOrder { get { return new DelegateCommand(Order.AddItem); } }

		public ObservableCollection<Menu> Menus { get; set; }
		public OrderViewModel Order { get; set; }

		public DelegateCommand Cancel { get { return new DelegateCommand(CancelOrder); } }
		public DelegateCommand Cashier { get { return new DelegateCommand(PlaceOrder); } }

		[Obsolete("Used only for design-time")]
		public CustomerOrderScreenViewModel()
		{
			_mPendingOrders = new ObservableCollection<OrderViewModel>();
			var r = new Random();
			Order = new OrderViewModel { OrderNumber = r.Next(1, 100) };
		}

		private readonly IMessageBus _mMessageBus;
		public CustomerOrderScreenViewModel(BBQRMSEntities dataService, 
            ObservableCollection<OrderViewModel> pendingOrders, IMessageBus navigationService)
		{
			_mDataService = dataService;
			_mPendingOrders = pendingOrders;
			_mMessageBus = navigationService;

			Order = new OrderViewModel();

		        Menus = new ObservableCollection<Menu>(_mDataService.Menus.Execute());
		        foreach(var m in Menus)
		        {
		            _mDataService.LoadProperty(m, "MenuItems");
		        }
		    foreach (Menu menu in Menus)
		    {
		        _mDataService.LoadProperty(menu, "MenuItems");
		    }
            
		}

		public void CancelOrder()
		{
			Order = new OrderViewModel();
			NotifyPropertyChanged("order");
		}

		public void PlaceOrder()
		{
			_mPendingOrders.Add(Order);
			var orderCashierScreenViewModel = new OrderCashierScreenViewModel(Order, _mMessageBus);

			Order = new OrderViewModel();
			NotifyPropertyChanged("order");

			_mMessageBus.Publish(new ShowScreen(orderCashierScreenViewModel));
		}
	}
}

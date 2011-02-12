using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.ViewModels.Messages;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class CustomerOrderScreenViewModel : ViewModelBase
	{
		private readonly BBQRMSEntities _mDataService;

        public DelegateCommand AddToOrder { get { return new DelegateCommand(Order.AddItem); } }

		public ObservableCollection<Menu> Menus { get; set; }
		public OrderViewModel Order { get; set; }

		public DelegateCommand Cancel { get { return new DelegateCommand(CancelOrder); } }
		public DelegateCommand Cashier { get { return new DelegateCommand(PlaceOrder); } }

		[Obsolete("Used only for design-time")]
		public CustomerOrderScreenViewModel()
		{
			var r = new Random();
		}

		private readonly IMessageBus _mMessageBus;
		public CustomerOrderScreenViewModel(BBQRMSEntities dataService, IMessageBus navigationService)
		{
			_mDataService = dataService;
			_mMessageBus = navigationService;

			Order = new OrderViewModel(_mMessageBus,_mDataService);

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
			Order = new OrderViewModel(_mMessageBus,_mDataService);
			NotifyPropertyChanged("order");
		}

		public void PlaceOrder()
		{
			var orderCashierScreenViewModel = new OrderCashierScreenViewModel(Order, _mMessageBus);

            Order = new OrderViewModel(_mMessageBus, _mDataService);
			NotifyPropertyChanged("order");

			_mMessageBus.Publish(new ShowScreen(orderCashierScreenViewModel));
		}
	}
}

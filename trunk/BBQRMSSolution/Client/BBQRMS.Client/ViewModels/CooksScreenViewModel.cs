using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class CooksScreenViewModel : ViewModelBase
	{
        private ObservableCollection<Order> _pendingOrders;

		[Obsolete("Used for design-time only", true)]
		public CooksScreenViewModel()
		{
		}

		public CooksScreenViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
		{
			DataService = dataService;
			MessageBus = messageBus;

		    PendingOrders = new ObservableCollection<Order>(DataService.Orders.Expand("OrderItems").Execute());

			CompleteOrderCommand = new DelegateCommand(HandleCompleteOrder);
		}

		public ObservableCollection<Order> PendingOrders
		{
			get { return _pendingOrders; }
            set { _pendingOrders = value; NotifyPropertyChanged("PendingOrders"); }
		}

		public DelegateCommand CompleteOrderCommand { get; private set; }

		private void HandleCompleteOrder(object parameter)
		{
			var orderViewModel = (Order) parameter;
			PendingOrders.Remove(orderViewModel);
		}
	}
}
using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class CooksScreenViewModel : ViewModelBase
	{
		private readonly ObservableCollection<OrderViewModel> _pendingOrders;

		[Obsolete("Used for design-time only", true)]
		public CooksScreenViewModel()
		{
		}

		public CooksScreenViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
		{
			DataService = dataService;
			MessageBus = messageBus;

			CompleteOrderCommand = new DelegateCommand(HandleCompleteOrder);
		}

		public ObservableCollection<OrderViewModel> PendingOrders
		{
			get { return _pendingOrders; }
		}

		public DelegateCommand CompleteOrderCommand { get; private set; }

		private void HandleCompleteOrder(object parameter)
		{
			var orderViewModel = (OrderViewModel) parameter;
			PendingOrders.Remove(orderViewModel);
		}
	}
}
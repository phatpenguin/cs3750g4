using System.Collections.ObjectModel;

namespace BBQRMSSolution.ViewModels
{
	public class CooksScreenViewModel : ViewModelBase
	{
		private readonly ObservableCollection<OrderViewModel> mPendingOrders;

		public CooksScreenViewModel()
		{
			CompleteOrderCommand = new DelegateCommand(HandleCompleteOrder);
		}

		public ObservableCollection<OrderViewModel> PendingOrders
		{
			get { return mPendingOrders; }
		}

		public DelegateCommand CompleteOrderCommand { get; private set; }

		private void HandleCompleteOrder(object parameter)
		{
			var orderViewModel = (OrderViewModel) parameter;
			PendingOrders.Remove(orderViewModel);
		}
	}
}
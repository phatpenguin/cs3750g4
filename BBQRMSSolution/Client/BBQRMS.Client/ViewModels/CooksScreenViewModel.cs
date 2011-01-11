using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.Models;
using BBQRMSSolution.SampleData;

namespace BBQRMSSolution.ViewModels
{
	public class CooksScreenViewModel : ViewModelBase
	{
		private readonly ObservableCollection<OrderViewModel> mPendingOrders;

		[Obsolete("Used for design-time only")]
		public CooksScreenViewModel()
		{
			mPendingOrders = SampleOrders.Sample;
		}

		public CooksScreenViewModel(ObservableCollection<OrderViewModel> pendingOrders)
		{
			mPendingOrders = pendingOrders;
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
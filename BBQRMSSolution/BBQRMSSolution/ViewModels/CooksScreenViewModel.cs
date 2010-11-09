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
		}

		public ObservableCollection<OrderViewModel> PendingOrders
		{
			get { return mPendingOrders; }
		}
	}
}
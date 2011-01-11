using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.Models;

namespace BBQRMSSolution.ViewModels
{
	class OrderCashierScreenViewModel : ViewModelBase
	{
		private INavigationService _mNavigationService;

		public OrderViewModel Order { get; set; }
		public ObservableCollection<MethodOfPayment> MopList { get; set; }

		public OrderCashierScreenViewModel()
		{
			Order = new OrderViewModel();

			MakeMethodOfPaymentList();
		}

		public OrderCashierScreenViewModel(OrderViewModel orderViewModel, INavigationService navigationService)
		{
			_mNavigationService = navigationService;
			Order = orderViewModel;

			MakeMethodOfPaymentList();
		}

		private void MakeMethodOfPaymentList()
		{
						MopList = new ObservableCollection<MethodOfPayment>
			          	{
			          		new MethodOfPayment {Description = "Cash",BackColor = "#999900",TextColor = "#FFFFFF"},
			          		new MethodOfPayment {Description = "Credit",BackColor = "#990099",TextColor = "#FFFFFF"},
			          		new MethodOfPayment {Description = "Check",BackColor = "#009999",TextColor = "#FFFFFF"}
			          	};
		}

	}
}

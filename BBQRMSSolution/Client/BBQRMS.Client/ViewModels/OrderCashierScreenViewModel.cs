using System.Collections.ObjectModel;
using BBQRMSSolution.Models;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	class OrderCashierScreenViewModel : ViewModelBase
	{
		private IMessageBus _mMessageBus;

		public OrderViewModel Order { get; set; }
		public ObservableCollection<MethodOfPayment> MopList { get; set; }

		public OrderCashierScreenViewModel()
		{
			Order = new OrderViewModel(null,null);

			MakeMethodOfPaymentList();
		}

		public OrderCashierScreenViewModel(OrderViewModel orderViewModel, IMessageBus navigationService)
		{
			_mMessageBus = navigationService;
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

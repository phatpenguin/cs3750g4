using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.Models;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class OrderCashierScreenViewModel : ViewModelBase
	{

		public OrderViewModel Order { get; set; }
		public ObservableCollection<MethodOfPayment> MopList { get; set; }

		[Obsolete("Used for design-time only", true)]
		public OrderCashierScreenViewModel()
		{
			Order = new OrderViewModel();

			MakeMethodOfPaymentList();
		}

		public OrderCashierScreenViewModel(OrderViewModel orderViewModel, IMessageBus messageBus)
		{
			MessageBus = messageBus;
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

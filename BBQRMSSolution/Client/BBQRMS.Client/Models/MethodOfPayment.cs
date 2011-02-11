using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBQRMSSolution.ViewModels;
using Controls;

namespace BBQRMSSolution.Models
{
	class MethodOfPayment : BaseModel
	{
		public String Description { get; set; }
		public String BackColor { get; set; }
		public String TextColor { get; set; }

		public MethodOfPayment()
		{
			NumberButtonClicked = new DelegateCommand(OnNumberButtonClicked);
			PaidAmount = "0.00";
		}

		public DelegateCommand NumberButtonClicked { get; set; }

		private decimal _mPaidAmount;
		public string PaidAmount { get; set; }

		public decimal OrderAmount { get; set; }
		public decimal ChangeAmount { get; private set; }

		public void OnNumberButtonClicked(Object digit)
		{
			PaidAmount = PaidAmount == "0.00" ? (string) digit : PaidAmount + (string)digit;
			NotifyPropertyChanged("PaidAmount");
		}

		public void OnEnterButtonClicked()
		{
			_mPaidAmount = Decimal.Parse(PaidAmount);
			ChangeAmount = OrderAmount - _mPaidAmount;
		}
	}
}

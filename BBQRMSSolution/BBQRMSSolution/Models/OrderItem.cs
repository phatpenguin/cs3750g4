using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution.Models
{
	public class OrderItem : BaseModel
	{
		public MenuItem MenuItem { get; set; }

		private int q;
		public int Quantity { get { return q; } set { q = value; NotifyPropertyChanged("quantity"); } }
		public DelegateCommand DoAction { get; set; }

		public OrderItem()
		{
			MenuItem = new MenuItem();
		}
	}
}

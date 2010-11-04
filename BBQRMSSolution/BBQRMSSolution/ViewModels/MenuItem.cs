using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBQRMSSolution.ViewModels
{
	public class MenuItem
	{
		public DelegateCommand AddItemToOrder { get; set; }
		public String name { get; set; }
		public int id { get; set; }
		public decimal price { get; set; }
	}
}

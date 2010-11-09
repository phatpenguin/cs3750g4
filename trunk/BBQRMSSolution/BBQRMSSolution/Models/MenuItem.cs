using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution.Models
{
	public class MenuItem
	{
		public DelegateCommand DoAction { get; set; }
		public String name { get; set; }
		public int id { get; set; }
		public decimal price { get; set; }
	}
}

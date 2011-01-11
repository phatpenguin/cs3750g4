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
		public String Name { get; set; }
		public int Id { get; set; }
		public decimal Price { get; set; }
		public String ImageSource { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBQRMSSolution.Models
{
	public class OrderItem
	{
		public MenuItem menuItem { get; set; }
		public int quantity { get; set; }

		public OrderItem()
		{
			menuItem = new MenuItem();
		}
	}
}

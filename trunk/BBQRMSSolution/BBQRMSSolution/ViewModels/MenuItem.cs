using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBQRMSSolution.ViewModels
{
	public delegate void MenuItemClickHandler(object sender, EventArgs e);

	public class MenuItem
	{
		public MenuItemClickHandler Clickr { get; set; }
		public String name { get; set; }
		public int id { get; set; }
		public decimal price { get; set; }
	}
}

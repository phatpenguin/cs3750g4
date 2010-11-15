using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace BBQRMSSolution.Models
{
	public class Menu
	{
		public String Name { get; set; }
		public ObservableCollection<MenuItem> MenuItems { get; set; }
		public String BackColor { get; set; }
		public String TextColor { get; set; }

		public Menu()
		{
			MenuItems = new ObservableCollection<MenuItem>();
		}

	}
}

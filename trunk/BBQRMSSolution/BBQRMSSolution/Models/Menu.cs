using System;
using System.Collections.ObjectModel;

namespace BBQRMSSolution.Models
{
	public class Menu
	{
		public String name { get; set; }
		public ObservableCollection<MenuItem> menuItems { get; set; }

		public Menu()
		{
			menuItems = new ObservableCollection<MenuItem>();
		}

	}
}

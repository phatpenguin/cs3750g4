using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.Models;
using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution.SampleData
{
	public class SampleOrders
	{
		static SampleOrders()
		{
			DateTime startDate = DateTime.Now;
			startDate = startDate.AddMilliseconds(-startDate.Millisecond);

			Sample =
				new ObservableCollection<OrderViewModel>
					{
						new OrderViewModel
							{
								OrderNumber = 56,
								OrderSubmittedDate = startDate.AddMinutes(-7).AddSeconds(-15),
								Items =
									{
										//new OrderItem {MenuItem = new MenuItem {Name = "Ribs"}, Quantity = 3},
										//new OrderItem {MenuItem = new MenuItem {Name = "Sodas"}, Quantity = 3},
									}
							},
						new OrderViewModel
							{
								OrderNumber = 57,
								OrderSubmittedDate = startDate.AddMinutes(-6).AddSeconds(-10),
								Items =
									{
										//new OrderItem {MenuItem = new MenuItem {Name = "Chicken"}, Quantity = 3},
										//new OrderItem {MenuItem = new MenuItem {Name = "Sodas"}, Quantity = 3},
									}
							},
					};
		}

		public static ObservableCollection<OrderViewModel> Sample { get; private set; }
	}
}
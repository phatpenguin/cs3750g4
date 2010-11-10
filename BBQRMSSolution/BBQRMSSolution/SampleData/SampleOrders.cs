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
										new OrderItem {menuItem = new MenuItem {name = "Ribs"}, quantity = 3},
										new OrderItem {menuItem = new MenuItem {name = "Sodas"}, quantity = 3},
									}
							},
						new OrderViewModel
							{
								OrderNumber = 57,
								OrderSubmittedDate = startDate.AddMinutes(-6).AddSeconds(-10),
								Items =
									{
										new OrderItem {menuItem = new MenuItem {name = "Chicken"}, quantity = 3},
										new OrderItem {menuItem = new MenuItem {name = "Sodas"}, quantity = 3},
									}
							},
						new OrderViewModel
							{
								OrderNumber = 58,
								OrderSubmittedDate = startDate.AddMinutes(-3).AddSeconds(-9),

							},
						new OrderViewModel
							{
								OrderNumber = 59,
								OrderSubmittedDate = startDate.AddMinutes(-1).AddSeconds(-52),
							},
					};
		}

		public static ObservableCollection<OrderViewModel> Sample { get; private set; }
	}
}
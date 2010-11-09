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
								OrderAge = startDate.AddMinutes(7).AddSeconds(15) - startDate,
								Items =
									{
										new OrderItem {menuItem = new MenuItem {name = "Ribs"}, quantity = 3},
										new OrderItem {menuItem = new MenuItem {name = "Sodas"}, quantity = 3},
									}
							},
						new OrderViewModel
							{
								OrderNumber = 57,
								OrderAge = startDate.AddMinutes(6).AddSeconds(10) - startDate,
								Items =
									{
										new OrderItem {menuItem = new MenuItem {name = "Chicken"}, quantity = 3},
										new OrderItem {menuItem = new MenuItem {name = "Sodas"}, quantity = 3},
									}
							},
						new OrderViewModel
							{
								OrderNumber = 58,
								OrderAge = startDate.AddMinutes(3).AddSeconds(9) - startDate,

							},
						new OrderViewModel
							{
								OrderNumber = 59,
								OrderAge = startDate.AddMinutes(1).AddSeconds(52) - startDate,
							},
					};
		}

		public static ObservableCollection<OrderViewModel> Sample { get; private set; }
	}
}
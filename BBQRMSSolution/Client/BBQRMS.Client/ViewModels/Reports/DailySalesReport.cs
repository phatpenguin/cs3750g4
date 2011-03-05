using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.IO;
using System.Linq;
using System.Reflection;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.ServerProxy;

namespace BBQRMSSolution.ViewModels.Reports
{
	public class DailySalesReport : ReportViewModel
	{
		private readonly ReportDateParameterViewModel _startDateParameter;
		private readonly ReportDateParameterViewModel _endDateParameter;

		public DailySalesReport(BBQRMSEntities dataService, IClientTimeProvider timeProvider)
			: base(dataService, timeProvider)
		{
			ReportName = "Daily Sales Report";
			Group = "Sales";
			HasChart = false;
			_startDateParameter = new ReportDateParameterViewModel {Name="StartDate", Prompt = "Starting From"};
			_endDateParameter = new ReportDateParameterViewModel {Name="EndDate", Prompt = "Through"};
			Parameters.Add(StartDateParameter);
			Parameters.Add(EndDateParameter);
		}

		public ReportDateParameterViewModel StartDateParameter
		{
			get { return _startDateParameter; }
		}

		public ReportDateParameterViewModel EndDateParameter
		{
			get { return _endDateParameter; }
		}

		public override void SetParameterDefaults()
		{
			StartDateParameter.Value = TheClock.Now.Date.AddDays(-7);
			EndDateParameter.Value = TheClock.Now.Date.AddDays(-1);
		}

		protected override Stream GetReportDefinition()
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream("BBQRMSSolution.Reports.DailySales.rdlc");
		}

		protected override IDictionary<string, IEnumerable> GetDataSets()
		{

			var startDate = StartDateParameter.Value.Date;
			var endDate = EndDateParameter.Value.Date;

			var menuItems = DataService.MenuItems.ToDictionary(mi => mi.Id);

			var orders = (DataServiceQuery<Order>) DataService.Orders.Expand("OrderItems").Where(o => o.OrderStateId == OrderStates.Closed && o.Date >= startDate && o.Date < endDate.AddDays(1));

			var dateAndItemGroups =
				from order in orders.ToList()
				from orderitem in order.OrderItems
				group orderitem by new {order.Date.Date, orderitem.MenuItemId}
				into grouping
				select
					new DailySalesRecord
						{
							Date = grouping.Key.Date,
							MenuItem = menuItems[grouping.Key.MenuItemId].Name,
							PerItemTotal = grouping.Sum(oi => oi.Quantity*oi.UnitPrice),
							PerItemTotalQuantity = grouping.Sum(oi => oi.Quantity)
						};

			return new Dictionary<string, IEnumerable> {{"DataSet1", dateAndItemGroups}};


/*
			toReturn.Add(
				"DataSet1",
				new[]
					{
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 1", PerItemTotal = 12.50m, PerItemTotalQuantity=1 },
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 2", PerItemTotal = 12.50m, PerItemTotalQuantity=12},
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 3", PerItemTotal = 12.50m, PerItemTotalQuantity=12},
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 4", PerItemTotal = 12.50m, PerItemTotalQuantity=12},
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 5", PerItemTotal = 12.50m, PerItemTotalQuantity=12},
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 6", PerItemTotal = 12.50m, PerItemTotalQuantity=12},
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 7", PerItemTotal = 12.50m, PerItemTotalQuantity=12},
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 8", PerItemTotal = 12.50m, PerItemTotalQuantity=12},
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 9", PerItemTotal = 12.50m, PerItemTotalQuantity=12},
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 10", PerItemTotal = 12.50m, PerItemTotalQuantity=1 },
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 11", PerItemTotal = 12.50m, PerItemTotalQuantity=12},
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 12", PerItemTotal = 12.50m, PerItemTotalQuantity=12},
						new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 13", PerItemTotal = 12.50m, PerItemTotalQuantity=12},
						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 1", PerItemTotal = 12.50m},
						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-2), MenuItem = "item 2", PerItemTotal = 12.50m},
						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-3), MenuItem = "item 3", PerItemTotal = 12.50m},
						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-4), MenuItem = "item 4", PerItemTotal = 222.50m},
						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-5), MenuItem = "item 5", PerItemTotal = 12.50m},
						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-6), MenuItem = "item 6", PerItemTotal = 12.50m},
//						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-7), MenuItem = "item 7", PerItemTotal = 12.50m},
//						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-8), MenuItem = "item 8", PerItemTotal = 12.50m},
//						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-9), MenuItem = "item 9", PerItemTotal = 12.50m},
//						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-10), MenuItem = "item 10", PerItemTotal = 12.50m},
//						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-11), MenuItem = "item 11", PerItemTotal = 12.50m},
//						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-12), MenuItem = "item 12", PerItemTotal = 12.50m},
//						new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-13), MenuItem = "item 13", PerItemTotal = 12.50m},
					}
				);
*/
		}
	}
}
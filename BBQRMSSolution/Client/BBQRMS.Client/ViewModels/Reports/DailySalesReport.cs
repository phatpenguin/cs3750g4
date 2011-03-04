using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using BBQRMSSolution.ServerProxy;

namespace BBQRMSSolution.ViewModels.Reports
{
	public class DailySalesReport : ReportViewModel
	{
		private readonly ReportDateParameterViewModel _startDateParameter;
		private readonly ReportDateParameterViewModel _endDateParameter;

		public DailySalesReport(BBQRMSEntities dataService)
			: base(dataService)
		{
			ReportName = "Daily Sales Report";
			Group = "Sales";
			HasChart = false;
			_startDateParameter = new ReportDateParameterViewModel {Prompt = "Start Date"};
			_endDateParameter = new ReportDateParameterViewModel { Prompt = "End Date" };
			Parameters.Add(_startDateParameter);
			Parameters.Add(_endDateParameter);
		}

		protected override Stream GetReportDefinition()
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream("BBQRMSSolution.Reports.DailySales.rdlc");
		}

		protected override IEnumerable GetData()	
		{
			var startDate = _startDateParameter.Value;
			var endDate = _endDateParameter.Value;
			IQueryable<Order> queryable = DataService.Orders.Expand("OrderItems").Where(o => o.OrderStateId == 6 && o.Date >= startDate && o.Date < endDate.AddDays(1));
			var orderItems = queryable.ToList().SelectMany(o => o.OrderItems);

/*
			return new[]
			       	{
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 1", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 2", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 3", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 4", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 5", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 6", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 7", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 8", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 9", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 10", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 11", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 12", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date, MenuItem = "item 13", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 1", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 2", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 3", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 4", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 5", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 6", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 7", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 8", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 9", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 10", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 11", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 12", PerItemTotal = 12.50m},
			       		new DailySalesRecord {Date = DateTime.Now.Date.AddDays(-1), MenuItem = "item 13", PerItemTotal = 12.50m},
			       	};
*/

			return orderItems.Select(oi => new DailySalesRecord {Date = oi.Order.Date, PerItemTotal = oi.UnitPrice * oi.Quantity, MenuItem = "AAA"});
		}
	}
}
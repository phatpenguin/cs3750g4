using System;

namespace BBQRMSSolution.ViewModels.Reports
{
	public class DailySalesRecord
	{
		public DateTime Date { get; set; }
		public string MenuItem { get; set; }
		public decimal PerItemTotal { get; set; }
		public decimal PerItemTotalQuantity { get; set; }
	}
}
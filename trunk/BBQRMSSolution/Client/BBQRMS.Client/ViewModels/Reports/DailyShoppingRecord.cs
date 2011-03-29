namespace BBQRMSSolution.ViewModels.Reports
{
	public class DailyShoppingRecord
	{
		public string ItemName { get; set; }
		public string UnitOfMeasure { get; set; }
		public decimal CurrentlyOnHand { get; set; }
		public decimal MinQuantity { get; set; }
		public decimal MaxQuantity { get; set; }

		public decimal PastAverageSameWeekdayConsumption { get; set; }
		public decimal ToPurchase { get; set; }
	}
}
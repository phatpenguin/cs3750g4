using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Services.Client;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels.Reports
{
	public class DailyShoppingReport : ReportViewModel
	{
		private readonly ReportOptionParameterViewModel _when;
		private readonly ReportBoolParameterViewModel _includeAllConsumption;
		private readonly ReportOptionParameterViewModel _averageOverWeeks;

		[Obsolete("Used for design-time only", true)]
		public DailyShoppingReport()
		{
		}

		public DailyShoppingReport(BBQRMSEntities dataService, IClientTimeProvider timeProvider)
			: base(dataService, timeProvider)
		{
			ReportName = "Daily Shopping list";
			Group = "Inventory";
			HasChart = false;
			_when =
				new ReportOptionParameterViewModel
					{
						Name = "When",
						Prompt = "For",
						Options = {"Today", "Tomorrow"},
						SelectedOption = "Tomorrow"
					};
			_includeAllConsumption =
				new ReportBoolParameterViewModel
					{
						Name = "IncludeAll",
						Prompt = "Include all inventory usage?",
						Value = false
					};
			_averageOverWeeks =
				new ReportOptionParameterViewModel
					{
						Name = "Weeks",
						Prompt = "Based on how many weeks?",
						Options = {"1", "2", "3", "4"},
						SelectedOption = "4"
					};
			Parameters.Add(When);
			Parameters.Add(AverageOverWeeks);
			Parameters.Add(IncludeAllConsumption);
		}

		public ReportBoolParameterViewModel IncludeAllConsumption { get { return _includeAllConsumption; } }

		public ReportOptionParameterViewModel When { get { return _when; } }

		public ReportOptionParameterViewModel AverageOverWeeks { get { return _averageOverWeeks; } }

		protected override Stream GetReportDefinition()
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream("BBQRMSSolution.Reports.DailyShoppingList.rdlc");
		}

		protected override IDictionary<string, IEnumerable> GetDataSets()
		{
			var consumptionRecordsQuery = (DataServiceQuery<ConsumedInventory>)DataService.ConsumedInventories.Where(ci => ci.ConsumptionTypeId == ConsumptionTypes.Used);

			if (!IncludeAllConsumption.Value)
				consumptionRecordsQuery =
					(DataServiceQuery<ConsumedInventory>) consumptionRecordsQuery.Where(ci => ci.ExcludeFromDailyShopping == false);
			
			int howManyWeeks = int.Parse(AverageOverWeeks.StringValue);
			DateTime targetDate = TheClock.Now.Date;
			if (When.StringValue == "Tomorrow")
				targetDate = targetDate.AddDays(1);

			// now make an expression out of the time ranges for the same day in prior weeks.
			var lookAtDates = new Expression<Func<ConsumedInventory, bool>>[howManyWeeks];
			for (int i = 0; i < howManyWeeks; i++)
			{
				DateTime currentLookAtDate = targetDate.AddDays((i + 1)*-7);
				Expression<Func<ConsumedInventory, bool>> expr = ci => ci.DateConsumed >= currentLookAtDate && ci.DateConsumed < currentLookAtDate.AddDays(1);
				lookAtDates[i] = expr;
			}
			if (howManyWeeks > 1)
			{
				var expr = Expression.OrElse(lookAtDates[0].Body, lookAtDates[1].Body);
				for (int i = 2; i < lookAtDates.Length; i++)
				{
					expr = Expression.OrElse(expr, lookAtDates[i].Body);
				}
				var whereLambda = Expression.Lambda<Func<ConsumedInventory, bool>>(expr, lookAtDates[0].Parameters);
				consumptionRecordsQuery = (DataServiceQuery<ConsumedInventory>) consumptionRecordsQuery.Where(whereLambda);
			}
			else
			{
				consumptionRecordsQuery = (DataServiceQuery<ConsumedInventory>) consumptionRecordsQuery.Where(lookAtDates[0]);
			}

			List<ConsumedInventory> consumptionRecords = consumptionRecordsQuery.Execute().ToList();

			var dailySummary =
				from ci in consumptionRecords
				group ci by new {ci.DateConsumed.Date, ci.MasterInventoryId}
				into byDateAndItem
				select new
				       	{
//				       		Date = byDateAndItem.Key.Date,
				       		MasterInventoryId = byDateAndItem.Key.MasterInventoryId,
				       		Quantity = byDateAndItem.Sum(ci => ci.Quantity)
				       	};

			var allInventoryItems = DataService.MasterInventories.Where(mi => mi.IsActive).ToList();

			var averageUsage =
				from dailyItem in dailySummary
				group dailyItem by dailyItem.MasterInventoryId
				into byItem
				select new
				       	{
									MasterInventoryId = byItem.Key,
									PastUsage = byItem.Average(i => i.Quantity)
				       	};

			IEnumerable<DailyShoppingRecord> finalResults = Enumerable.Empty<DailyShoppingRecord>();

			finalResults =
				from item in allInventoryItems
				let usage = averageUsage.FirstOrDefault(u => u.MasterInventoryId == item.Id)
				let pastUsage = usage == null ? 0 : usage.PastUsage
				select
					new DailyShoppingRecord
						{
							ItemName = item.Name,
							CurrentlyOnHand = item.UnitQty,
							MinQuantity = item.MinQuantity,
							MaxQuantity = item.MaxQuantity,
							UnitOfMeasure = item.UnitOfMeasure,
							PastAverageSameWeekdayConsumption = pastUsage,
							ToPurchase = ComputeNeededPurchase(item.UnitQty, pastUsage, item.MinQuantity, item.MaxQuantity)
						};

			return
				new Dictionary<string, IEnumerable>
					{
						{"DataSet1", finalResults},
						{"DataSet2", Enumerable.Repeat(new DailyShoppingReportHeader {TargetDate = targetDate}, 1)}
					};
		}

		private decimal ComputeNeededPurchase(int onHand, decimal pastUsage, int minQuantity, int maxQuantity)
		{
			decimal needToHave = Math.Max(pastUsage, minQuantity);
			if (onHand > needToHave)
				return 0;
			return Math.Ceiling(Math.Min(needToHave - onHand, maxQuantity - onHand));
		}
	}

	public class DailyShoppingReportHeader
	{
		public DateTime TargetDate { get; set; }
	}
}
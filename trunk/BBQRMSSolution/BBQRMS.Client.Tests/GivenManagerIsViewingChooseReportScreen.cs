using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.IO;
using System.Linq;
using BBQRMS.WCFServices;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ServerProxy;
using BBQRMSSolution.ViewModels;
using BBQRMSSolution.ViewModels.Reports;
using Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServerTimeProvider = BBQRMS.WCFServices.TimeProvider;
using BBQRMSEntities = BBQRMSSolution.ServerProxy.BBQRMSEntities;
using ClientTimeProvider = BBQRMSSolution.BusinessLogic.TimeProvider;

namespace BBQRMS.Client.Tests
{
	[TestClass]
	public class GivenManagerIsViewingChooseReportScreen
	{
		private static Uri _serviceAddress;
		private static BBQRMSEntities _dataService;
		private static Mock<ISecurityContext> _mockSecurityContext;
		private static Mock<IMessageBus> _mockMessageBus;

		private static readonly TimeProviderForTesting Time = new TimeProviderForTesting();
		private static ChooseReportViewModel _chooseReportViewModel;

		[ClassInitialize]
		public static void BeforeAllTests(TestContext testContext)
		{
			ServerTimeProvider.Current = Time;
			ClientTimeProvider.Current = Time;
			// start the data service
			_serviceAddress = new Uri("http://localhost:80/Temporary_Listen_Addresses/BBQRMSTestingGivenManagerIsViewingChooseReportScreen/");
			Host.Open(_serviceAddress);
			_dataService = new BBQRMSEntities(_serviceAddress);

			_mockMessageBus = new Mock<IMessageBus>();

			_mockSecurityContext = new Mock<ISecurityContext>();
			var reportingRole = PrepareRole.Named("Reporting").WithPrivelege(Privileges.RunReports);
			var mockEmployee = PrepareEmployee.With(firstName: "Bob", lastName: "Marley").AndRole(reportingRole);

			_mockSecurityContext.SetupGet(s => s.CurrentUser).Returns(mockEmployee);
			
			_chooseReportViewModel = new ChooseReportViewModel(_dataService, _mockMessageBus.Object, Time);
		}

		[ClassCleanup]
		public static void AfterAllTests()
		{
			Host.Close();
			ServerTimeProvider.ResetToDefault();
			ClientTimeProvider.ResetToDefault();
		}

		[TestMethod]
		public void WhenScreenIsOpen_ThenDailySalesReportCanBeRunAndDefaultsToLastWeek()
		{
			var dailySalesReport = _chooseReportViewModel.Reports.FirstOrDefault(rpt => rpt is DailySalesReport) as DailySalesReport;
			Assert.IsNotNull(dailySalesReport);
			_chooseReportViewModel.SelectedReport = dailySalesReport;
			Assert.IsTrue(_chooseReportViewModel.RunReportCommand.CanExecute(null));

			_chooseReportViewModel.RunReportCommand.Execute(null);
			_mockMessageBus.Verify(m => m.Publish(It.Is((ShowScreen msg) => msg.ViewModelToShow == dailySalesReport)));
			Assert.AreEqual(dailySalesReport.StartDateParameter.Value, Time.Now.Date.AddDays(-7));
			Assert.AreEqual(dailySalesReport.EndDateParameter.Value, Time.Now.Date.AddDays(-1));
		}

		[TestMethod]
		public void WhenDailySalesReportIsRun_ThenNoExceptionsAreThrown()
		{
			PutTwoClosedOrdersIntoDatabase();

			var dailySalesReport = _chooseReportViewModel.Reports.FirstOrDefault(rpt => rpt is DailySalesReport) as DailySalesReport;
			Assert.IsNotNull(dailySalesReport);
			_chooseReportViewModel.SelectedReport = dailySalesReport;
			Assert.IsTrue(_chooseReportViewModel.RunReportCommand.CanExecute(null));

			_chooseReportViewModel.RunReportCommand.Execute(null);

			var mockViewer = new Mock<IReportViewer>();

			dailySalesReport.StartDateParameter.Value = new DateTime(2011, 3, 4);
			dailySalesReport.EndDateParameter.Value = new DateTime(2011, 3, 4);

			var datasets = new Dictionary<string, IEnumerable>();

			mockViewer
				.Setup(v => v.AddDataSource(It.IsAny<string>(), It.IsAny<IEnumerable>()))
				.Callback((string name, IEnumerable data) => datasets.Add(name, data));

			dailySalesReport.RunReport(mockViewer.Object);

			mockViewer.Verify(v => v.AddParameter("StartDate", new DateTime(2011, 3, 4).ToString()));
			mockViewer.Verify(v => v.AddParameter("EndDate", new DateTime(2011, 3, 4).ToString()));
			mockViewer.Verify(v => v.AddDataSource("DataSet1", It.IsAny<IEnumerable>()));
			mockViewer.Verify(v => v.LoadReportDefinition(It.IsAny<Stream>()));
			foreach (KeyValuePair<string, IEnumerable> keyValuePair in datasets)
			{
				var reportRecords = keyValuePair.Value.OfType<DailySalesRecord>().ToList();
				Assert.AreEqual(3, reportRecords.Count);
				Assert.IsTrue(reportRecords.All(rec => rec.Date == new DateTime(2011, 3, 4)));
				Assert.IsTrue(reportRecords.Any(rec => rec.MenuItem == "Beer"));
				Assert.IsTrue(reportRecords.Any(rec => rec.MenuItem == "Soda"));
				Assert.IsTrue(reportRecords.Any(rec => rec.MenuItem == "Ribs"));
			}
		}

		[TestMethod]
		public void WhenDailyShoppingListReportIsRun_ThenNoExceptionsAreThrown()
		{
			var mockViewer = new Mock<IReportViewer>();
			var datasets = new Dictionary<string, IEnumerable>();
			mockViewer
				.Setup(v => v.AddDataSource(It.IsAny<string>(), It.IsAny<IEnumerable>()))
				.Callback((string name, IEnumerable data) => datasets.Add(name, data));

			var toTest = new DailyShoppingReport(_dataService, Time);
			toTest.IncludeAllConsumption.Value = true;
			toTest.RunReport(mockViewer.Object);

			//TODO: 
		}

		private void PutTwoClosedOrdersIntoDatabase()
		{
			var item1 = PrepareMenuItem.Of("Beer", 3m, "Beer");
			var item2 = PrepareMenuItem.Of("Soda", 1.50m, "Soda");
			var item3 = PrepareMenuItem.Of("Ribs", 6m, "Ribs");
			_dataService.AddToMenuItems(item1);
			_dataService.AddToMenuItems(item2);
			_dataService.AddToMenuItems(item3);
			_dataService.SaveChanges(SaveChangesOptions.Batch);

			var firstOrder = PrepareOrder
				.For(new DateTime(2011, 3, 4, 0, 0, 0), orderState: OrderStates.Closed, paymentState: PaymentStates.Paid)
				.ForItem(1, item1)
				.ForItem(2, item2)
				.ForItem(3, item3)
				.WithPayment(12m, PaymentTypes.Cash);

			var lastOrder = PrepareOrder
				.For(new DateTime(2011, 3, 4, 23, 59, 59), orderState: OrderStates.Closed, paymentState: PaymentStates.Paid)
				.ForItem(1, item1)
				.ForItem(2, item2)
				.ForItem(3, item3)
				.WithPayment(12m, PaymentTypes.Cash);
			_dataService.AddToOrders(firstOrder);
			foreach (var orderItem in firstOrder.OrderItems)
			{
				_dataService.AddToOrderItems(orderItem);
				_dataService.AddLink(firstOrder, "OrderItems", orderItem);
			}
			foreach (var payment in firstOrder.Payments)
			{
				_dataService.AddToPayments(payment);
				_dataService.AddLink(firstOrder, "Payments", payment);
			}
			_dataService.AddToOrders(lastOrder);
			foreach (var orderItem in lastOrder.OrderItems)
			{
				_dataService.AddToOrderItems(orderItem);
				_dataService.AddLink(lastOrder, "OrderItems", orderItem);
			}
			foreach (var payment in lastOrder.Payments)
			{
				_dataService.AddToPayments(payment);
				_dataService.AddLink(lastOrder, "Payments", payment);
			}
			_dataService.SaveChanges(SaveChangesOptions.Batch);
		}
	}
}
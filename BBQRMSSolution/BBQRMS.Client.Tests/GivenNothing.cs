using System;
using System.Data.Services.Client;
using System.Linq;
using BBQRMS.WCFServices;
using BBQRMSSolution;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ViewModels;
using Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using ServerTimeProvider = BBQRMS.WCFServices.TimeProvider;
using BBQRMSEntities = BBQRMSSolution.ServerProxy.BBQRMSEntities;
using Employee = BBQRMSSolution.ServerProxy.Employee;
using ApplicationUser = BBQRMSSolution.ServerProxy.ApplicationUser;
using ClientTimeProvider = BBQRMSSolution.BusinessLogic.TimeProvider;

// ReSharper disable InconsistentNaming
namespace BBQRMS.Client.Tests
{

	[TestClass]
	public class GivenNothing
	{
		private static Uri _serviceAddress;

		private static readonly TimeProviderForTesting Time = new TimeProviderForTesting();
		private static readonly IPOSDeviceManager POSDevices = new MockPOSDeviceManager();
		private static Employee _employee;

		[ClassInitialize]
		public static void BeforeAllTests(TestContext testContext)
		{
			ServerTimeProvider.Current = Time;
			ClientTimeProvider.Current = Time;
			// start the data service
			_serviceAddress = new Uri("http://localhost:80/Temporary_Listen_Addresses/BBQRMSTestingGivenNothing/");
			Host.Open(_serviceAddress);

			var _dataService = new BBQRMSEntities(_serviceAddress);
			_employee = PrepareEmployee.With(firstName: "Paul", lastName: "McCartney");
			_dataService.AddToEmployees(_employee);
			var user = new ApplicationUser { IdPart = "1", PersonalPart = "11" };
			_dataService.AddToApplicationUsers(user);
			_dataService.AddLink(_employee, "ApplicationUsers", user);
			_dataService.SaveChanges(SaveChangesOptions.Batch);

			Assert.IsTrue(_employee.Id > 0);
		}

		[ClassCleanup]
		public static void AfterAllTests()
		{
			Host.Close();
			ServerTimeProvider.ResetToDefault();
			ClientTimeProvider.ResetToDefault();
		}

		[TestMethod]
		public void WhenApplicationStarts_ThenLoginViewModelIsCurrentFullScreenContent()
		{
			MainWindowViewModel toTest = new MainWindowViewModel(_serviceAddress, new MessageBus(), new SecurityContext(), Time, POSDevices);
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof (LoginViewModel));
		}

		[TestMethod]
		public void WhenUserLogsInWithValidPIN_ThenUserLoggedInMessageIsPublished()
		{
			Mock<IMessageBus> mockEvents = new Mock<IMessageBus>();
			MainWindowViewModel toTest = new MainWindowViewModel(_serviceAddress, mockEvents.Object, new SecurityContext(), Time, POSDevices);
			var loginViewModel = (LoginViewModel) toTest.FullScreenContent;
			loginViewModel.HandleLogin("1011");
			//Make sure a message was published with the correct data.
			mockEvents.Verify(e => e.Publish(It.Is<UserLoggedIn>(u => u.Employee.Id == _employee.Id)));
			mockEvents.Verify(e => e.Publish(It.IsAny<InvalidPinEntered>()), Times.Never());
			//TODO: somehow verify that the loginViewModel's "PrepareTimeClock" method was called for the employee.
		}

		[TestMethod]
		public void WhenUserLogsInFirstTime_ThenEmployeeTimeClockIsCreated()
		{
			Mock<IMessageBus> mockEvents = new Mock<IMessageBus>();
			BBQRMSEntities dataService = new BBQRMSEntities(_serviceAddress);
			var newEmp = PrepareEmployee.With(firstName:"Ringo",lastName:"Starr").HiredOn(Time.Now.Date);
			dataService.AddToEmployees(newEmp);
			var resp = dataService.SaveChanges(SaveChangesOptions.Batch);

			var startTime = Time.UtcNow;

			LoginViewModel toTest = new LoginViewModel(dataService, mockEvents.Object);

			toTest.PrepareTimeClock(newEmp);

			//We should have a new timeclock record.
			var openClocks = dataService.EmployeeTimeClocks.Where(tc => tc.EmployeeId == newEmp.Id).ToList();
			Assert.AreEqual(1, openClocks.Count);
			Assert.IsNull(openClocks[0].ClockOutTimeUTC);
			Assert.IsTrue(openClocks[0].ClockInTimeUTC >= startTime);
		}

		[TestMethod]
		public void WhenUserLogsInMultipleTimes_ThenEmployeeTimeClockRecordIsReused()
		{
			Mock<IMessageBus> mockEvents = new Mock<IMessageBus>();
			BBQRMSEntities dataService = new BBQRMSEntities(_serviceAddress);
			var newEmp = PrepareEmployee.With(firstName:"George", lastName:"Harrison").HiredOn(Time.Now.Date);
			dataService.AddToEmployees(newEmp);
			var resp = dataService.SaveChanges(SaveChangesOptions.Batch);

			var startTime = Time.UtcNow;

			LoginViewModel toTest = new LoginViewModel(dataService, mockEvents.Object);

			//login once
			toTest.PrepareTimeClock(newEmp);

			Time.SkipForwardBy(TimeSpan.FromMinutes(5));
			var afterFirstLoginTime = Time.UtcNow;
			//login again
			toTest.PrepareTimeClock(newEmp);

			Time.SkipForwardBy(TimeSpan.FromMinutes(5));
			//login a third time
			toTest.PrepareTimeClock(newEmp);

			//We should still only have one TimeClock record.
			var openClocks = dataService.EmployeeTimeClocks.Where(tc => tc.EmployeeId == newEmp.Id).ToList();
			Assert.AreEqual(1, openClocks.Count);
			Assert.IsNull(openClocks[0].ClockOutTimeUTC);
			Assert.IsTrue(openClocks[0].ClockInTimeUTC >= startTime);
			Assert.IsTrue(openClocks[0].ClockInTimeUTC < afterFirstLoginTime);
		}

		[TestMethod]
		public void WhenUserForgetsToClockOut_ThenNewTimeClockRecordIsCreatedOnNextLogin()
		{
			Assert.Inconclusive("No decision yet on how to accomplish this.");
		}

		[TestMethod]
		public void WhenUserLoggedInMessageIsReceived_ThenFullScreenContentSwitchesToPostLoginViewModel()
		{
			MessageBus messageBus = new MessageBus();
			MainWindowViewModel toTest = new MainWindowViewModel(_serviceAddress, messageBus, new SecurityContext(), Time, POSDevices);
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof(LoginViewModel));
			messageBus.Publish(new UserLoggedIn(new Employee()));
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof(PostLoginViewModel));
		}

		[TestMethod]
		public void WhenUserEntersWrongPINFormat_ThenLoginViewModelIsStillCurrentAndErrorIsDisplayed()
		{
			Mock<IMessageBus> mockEvents = new Mock<IMessageBus>();
			MainWindowViewModel toTest = new MainWindowViewModel(_serviceAddress, mockEvents.Object, new SecurityContext(), Time, POSDevices);
			var loginViewModel = (LoginViewModel) toTest.FullScreenContent;
			loginViewModel.HandleLogin("9999999999");
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof (LoginViewModel));
			mockEvents.Verify(e => e.Publish(It.IsAny<UserLoggedIn>()), Times.Never());
			mockEvents.Verify(e => e.Publish(It.IsAny<InvalidPinEntered>()));
		}

		[TestMethod]
		public void WhenUserEntersUnrecognizedPIN_ThenLoginViewModelIsStillCurrentAndErrorIsDisplayed()
		{
			Mock<IMessageBus> mockEvents = new Mock<IMessageBus>();
			MainWindowViewModel toTest = new MainWindowViewModel(_serviceAddress, mockEvents.Object, new SecurityContext(), Time, POSDevices);
			var loginViewModel = (LoginViewModel)toTest.FullScreenContent;
			loginViewModel.HandleLogin("9999999999011111");
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof(LoginViewModel));
			mockEvents.Verify(e => e.Publish(It.IsAny<UserLoggedIn>()), Times.Never());
			mockEvents.Verify(e => e.Publish(It.IsAny<InvalidPinEntered>()));
		}
	}
}
// ReSharper restore InconsistentNaming

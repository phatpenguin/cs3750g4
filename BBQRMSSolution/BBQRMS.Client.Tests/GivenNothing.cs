using System;
using System.Data.Services.Client;
using System.Linq;
using BBQRMS.WCFServices;
using BBQRMSSolution;
using BBQRMSSolution.ViewModels;
using BBQRMSSolution.ViewModels.Messages;
using Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using ServerTimeProvider = BBQRMS.WCFServices.TimeProvider;
using BBQRMSEntities = BBQRMSSolution.ServerProxy.BBQRMSEntities;
using Employee = BBQRMSSolution.ServerProxy.Employee;
using ClientTimeProvider = BBQRMSSolution.TimeProvider;

// ReSharper disable InconsistentNaming
namespace BBQRMS.Client.Tests
{

	[TestClass]
	public class GivenNothing
	{
		private static Uri mServiceAddress;

		private static readonly TimeProviderForTesting time = new TimeProviderForTesting();

		[ClassInitialize]
		public static void BeforeAllTests(TestContext testContext)
		{
			ServerTimeProvider.Current = time;
			ClientTimeProvider.Current = time;
			// start the data service
			mServiceAddress = new Uri("http://localhost:80/Temporary_Listen_Addresses/BBQRMSTestingGivenNothing/");
			Host.Open(mServiceAddress);
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
			MainWindowViewModel toTest = new MainWindowViewModel(mServiceAddress, new MessageBus(), new SecurityContext());
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof (LoginViewModel));
		}

		[TestMethod]
		public void WhenUserLogsInWithValidPIN_ThenUserLoggedInMessageIsPublished()
		{
			Mock<IMessageBus> mockEvents = new Mock<IMessageBus>();
			MainWindowViewModel toTest = new MainWindowViewModel(mServiceAddress, mockEvents.Object, new SecurityContext());
			var loginViewModel = (LoginViewModel) toTest.FullScreenContent;
			loginViewModel.HandleLogin("1011");
			//Make sure a message was published with the correct data.
			mockEvents.Verify(e => e.Publish(It.Is<UserLoggedIn>(u => u.Employee.Id == 1)));
			mockEvents.Verify(e => e.Publish(It.IsAny<InvalidPinEntered>()), Times.Never());
			//TODO: somehow verify that the loginViewModel's "PrepareTimeClock" method was called for the employee.
		}

		[TestMethod]
		public void WhenUserLogsInFirstTime_ThenEmployeeTimeClockIsCreated()
		{
			Mock<IMessageBus> mockEvents = new Mock<IMessageBus>();
			BBQRMSEntities dataService = new BBQRMSEntities(mServiceAddress);
			var newEmp = GivenPostLoginView.MakeNewTestEmployee();
			dataService.AddToEmployees(newEmp);
			var resp = dataService.SaveChanges(SaveChangesOptions.Batch);

			var startTime = time.UtcNow;

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
			BBQRMSEntities dataService = new BBQRMSEntities(mServiceAddress);
			var newEmp = GivenPostLoginView.MakeNewTestEmployee();
			dataService.AddToEmployees(newEmp);
			var resp = dataService.SaveChanges(SaveChangesOptions.Batch);

			var startTime = time.UtcNow;

			LoginViewModel toTest = new LoginViewModel(dataService, mockEvents.Object);

			//login once
			toTest.PrepareTimeClock(newEmp);

			time.SkipForwardBy(TimeSpan.FromMinutes(5));
			var afterFirstLoginTime = time.UtcNow;
			//login again
			toTest.PrepareTimeClock(newEmp);

			time.SkipForwardBy(TimeSpan.FromMinutes(5));
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
			Assert.Inconclusive("Needs to be written");
		}

		[TestMethod]
		public void WhenUserLoggedInMessageIsReceived_ThenFullScreenContentSwitchesToPostLoginViewModel()
		{
			MessageBus messageBus = new MessageBus();
			MainWindowViewModel toTest = new MainWindowViewModel(mServiceAddress, messageBus, new SecurityContext());
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof(LoginViewModel));
			messageBus.Publish(new UserLoggedIn(new Employee()));
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof(PostLoginViewModel));
		}

		[TestMethod]
		public void WhenUserEntersWrongPINFormat_ThenLoginViewModelIsStillCurrentAndErrorIsDisplayed()
		{
			Mock<IMessageBus> mockEvents = new Mock<IMessageBus>();
			MainWindowViewModel toTest = new MainWindowViewModel(mServiceAddress, mockEvents.Object, new SecurityContext());
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
			MainWindowViewModel toTest = new MainWindowViewModel(mServiceAddress, mockEvents.Object, new SecurityContext());
			var loginViewModel = (LoginViewModel)toTest.FullScreenContent;
			loginViewModel.HandleLogin("9999999999011111");
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof(LoginViewModel));
			mockEvents.Verify(e => e.Publish(It.IsAny<UserLoggedIn>()), Times.Never());
			mockEvents.Verify(e => e.Publish(It.IsAny<InvalidPinEntered>()));
		}
	}
}
// ReSharper restore InconsistentNaming

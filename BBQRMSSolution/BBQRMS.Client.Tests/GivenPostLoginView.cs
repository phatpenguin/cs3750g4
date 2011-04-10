using System.Data.Services.Client;
using System.Linq;
using BBQRMS.WCFServices;
using BBQRMSSolution;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ViewModels;
using Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

using ServerTimeProvider = BBQRMS.WCFServices.TimeProvider;
using BBQRMSEntities = BBQRMSSolution.ServerProxy.BBQRMSEntities;
using Employee = BBQRMSSolution.ServerProxy.Employee;
using ApplicationUser = BBQRMSSolution.ServerProxy.ApplicationUser;
using ClientTimeProvider = BBQRMSSolution.BusinessLogic.TimeProvider;

namespace BBQRMS.Client.Tests
{
	[TestClass]
	public class GivenPostLoginView
	{
		private static Uri _serviceAddress;
		private static BBQRMSEntities _dataService;
		private Mock<ISecurityContext> _mockSecurityContext;
		private Mock<IMessageBus> _mockMessageBus;

		private static readonly TimeProviderForTesting Time = new TimeProviderForTesting();
		private static readonly IPOSDeviceManager POSDevices = new MockPOSDeviceManager();

		[ClassInitialize]
		public static void BeforeAllTests(TestContext testContext)
		{
			ServerTimeProvider.Current = Time;
			ClientTimeProvider.Current = Time;
			// start the data service
			_serviceAddress = new Uri("http://localhost:80/Temporary_Listen_Addresses/BBQRMSTestingGivenPostLoginView/");
			Host.Open(_serviceAddress);
			_dataService = new BBQRMSEntities(_serviceAddress);

			var emp = PrepareEmployee.With(firstName: "George", lastName: "Jones");
			_dataService.AddToEmployees(emp);
			var user = new ApplicationUser {IdPart = "2", PersonalPart = "22"};
			_dataService.AddToApplicationUsers(user);
			_dataService.AddLink(emp, "ApplicationUsers", user);
			_dataService.SaveChanges(SaveChangesOptions.Batch);
		}

		[ClassCleanup]
		public static void AfterAllTests()
		{
			Host.Close();
			ServerTimeProvider.ResetToDefault();
			ClientTimeProvider.ResetToDefault();
		}

		[TestInitialize]
		public void BeforeEachTest()
		{
			_mockMessageBus = new Mock<IMessageBus>();
			//Create new employee to use for this test run.
			var employee = PrepareEmployee.With().HiredOn(DateTime.Today);
			_dataService.AddToEmployees(employee);
			var resp = _dataService.SaveChanges(SaveChangesOptions.Batch);
			Assert.IsTrue(resp.BatchStatusCode >= 200 && resp.BatchStatusCode < 300);

			var login = new LoginViewModel(_dataService, _mockMessageBus.Object);

			login.PrepareTimeClock(employee);

			_mockSecurityContext = new Mock<ISecurityContext>();
			//Make this the logged-in employee.
			_mockSecurityContext.SetupGet(s => s.CurrentUser).Returns(employee);
		}

		[TestMethod]
		public void WhenUserClocksOutAndConfirms_ThenClockOutTimeIsRecordedAndUserIsLoggedOut()
		{
			var target = new PostLoginViewModel(_dataService, _mockMessageBus.Object, _mockSecurityContext.Object, Time, POSDevices);
			Time.SkipForwardBy(TimeSpan.FromMinutes(5));
			target.HandleClockOut();
			Assert.IsTrue(target.ClockOutVisible);
			target.ConfirmClockOut();
			Assert.IsFalse(target.ClockOutVisible);
			//TODO: verify that the correct EmployeeTimeClock record was updated

			var timeClocks = _dataService.EmployeeTimeClocks.Where(tc => tc.EmployeeId == _mockSecurityContext.Object.CurrentUser.Id).ToList();
			Assert.AreEqual(1, timeClocks.Count);
			Assert.IsNotNull(timeClocks[0].ClockOutTimeUTC);
			Assert.IsTrue(timeClocks[0].ClockOutTimeUTC > timeClocks[0].ClockInTimeUTC);
			Assert.IsTrue(timeClocks[0].ClockOutTimeUTC - timeClocks[0].ClockInTimeUTC > new TimeSpan(0, 4, 50));

			_mockMessageBus.Verify(m => m.Publish(It.IsAny<UserLoggingOut>()));

			//TODO: what if the user wasn't actually clocked in at the time? (i.e. two login sessions on different terminals.)
			//TODO: what if the dataService is unavailable or unresponsive?

		}

		[TestMethod]
		public void WhenLogoutIsClicked_ThenScreenRevertsToLoginScreenAndCurrentUserIsCleared()
		{
			var messageBus = new MessageBus();
			var securityContext = new SecurityContext();
			//App.xaml.cs does this at runtime:
			messageBus.Subscribe(securityContext);

			// the MainWindowViewModel (and the view models created by it) subscribe themselves to the messageBus.
			var toTest = new MainWindowViewModel(_serviceAddress, messageBus, securityContext, Time, POSDevices);
			var loginViewModel = (LoginViewModel)toTest.FullScreenContent;
			Assert.IsNull(securityContext.CurrentUser);

			//Login as a known, valid user.
			loginViewModel.HandleLogin("2022");
			Assert.IsNotNull(securityContext.CurrentUser);
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof(PostLoginViewModel));
			// ReSharper disable PossibleInvalidCastException
			var postLoginViewModel = (PostLoginViewModel)toTest.FullScreenContent;
			// ReSharper restore PossibleInvalidCastException

			postLoginViewModel.HandleLogout();
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof(LoginViewModel));
			Assert.IsNull(securityContext.CurrentUser);
		}

	}
}

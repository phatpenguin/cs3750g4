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
using ClientTimeProvider = BBQRMSSolution.BusinessLogic.TimeProvider;

namespace BBQRMS.Client.Tests
{
	[TestClass]
	public class GivenPostLoginView
	{
		private static Uri mServiceAddress;
		private static BBQRMSEntities mDataService;
		private Mock<ISecurityContext> mMockSecurityContext;
		private Mock<IMessageBus> mMockMessageBus;

		private static readonly TimeProviderForTesting time = new TimeProviderForTesting();

		[ClassInitialize]
		public static void BeforeAllTests(TestContext testContext)
		{
			ServerTimeProvider.Current = time;
			ClientTimeProvider.Current = time;
			// start the data service
			mServiceAddress = new Uri("http://localhost:80/Temporary_Listen_Addresses/BBQRMSTestingGivenPostLoginView/");
			Host.Open(mServiceAddress);
			mDataService = new BBQRMSEntities(mServiceAddress);
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
			mMockMessageBus = new Mock<IMessageBus>();
			//Create new employee to use for this test run.
			var employee = MakeNewTestEmployee();
			mDataService.AddToEmployees(employee);
			var resp = mDataService.SaveChanges(SaveChangesOptions.Batch);

			LoginViewModel login = new LoginViewModel(mDataService, mMockMessageBus.Object);

			login.PrepareTimeClock(employee);

			mMockSecurityContext = new Mock<ISecurityContext>();
			//Make this the logged-in employee.
			mMockSecurityContext.SetupGet(s => s.CurrentUser).Returns(employee);
		}

		public static Employee MakeNewTestEmployee()
		{
			return new Employee
			       	{FirstName = "A", LastName = "B", HireDate = DateTime.Today, PayTypeId = 1, PayAmount = 9.75m};
		}


		[TestMethod]
		public void WhenUserClocksOutAndConfirms_ThenClockOutTimeIsRecordedAndUserIsLoggedOut()
		{
			PostLoginViewModel target = new PostLoginViewModel(mDataService, mMockMessageBus.Object, mMockSecurityContext.Object);
			time.SkipForwardBy(TimeSpan.FromMinutes(5));
			target.HandleClockOut();
			Assert.IsTrue(target.ClockOutVisible);
			target.ConfirmClockOut();
			Assert.IsFalse(target.ClockOutVisible);
			//TODO: verify that the correct EmployeeTimeClock record was updated

			var timeClocks = mDataService.EmployeeTimeClocks.Where(tc => tc.EmployeeId == mMockSecurityContext.Object.CurrentUser.Id).ToList();
			Assert.AreEqual(1, timeClocks.Count);
			Assert.IsNotNull(timeClocks[0].ClockOutTimeUTC);
			Assert.IsTrue(timeClocks[0].ClockOutTimeUTC > timeClocks[0].ClockInTimeUTC);
			Assert.IsTrue(timeClocks[0].ClockOutTimeUTC - timeClocks[0].ClockInTimeUTC > new TimeSpan(0, 4, 50));

			mMockMessageBus.Verify(m => m.Publish(It.IsAny<UserLoggingOut>()));

			//TODO: what if the user wasn't actually clocked in at the time? (i.e. two login sessions on different terminals.)
			//TODO: what if the dataService is unavailable or unresponsive?


		}

		[TestMethod]
		public void WhenLogoutIsClicked_ThenScreenRevertsToLoginScreenAndCurrentUserIsCleared()
		{
			MessageBus messageBus = new MessageBus();
			SecurityContext securityContext = new SecurityContext();
			//App.xaml.cs does this at runtime:
			messageBus.Subscribe(securityContext);

			// the MainWindowViewModel (and the view models created by it) subscribe themselves to the messageBus.
			MainWindowViewModel toTest = new MainWindowViewModel(mServiceAddress, messageBus, securityContext);
			var loginViewModel = (LoginViewModel)toTest.FullScreenContent;
			Assert.IsNull(securityContext.CurrentUser);

			//Login as a known, valid user.
			loginViewModel.HandleLogin("1011");
			Assert.IsNotNull(securityContext.CurrentUser);
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof(PostLoginViewModel));
			var postLoginViewModel = (PostLoginViewModel)toTest.FullScreenContent;

			postLoginViewModel.HandleLogout();
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof(LoginViewModel));
			Assert.IsNull(securityContext.CurrentUser);
		}

	}
}

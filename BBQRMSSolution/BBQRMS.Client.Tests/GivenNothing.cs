using System;
using BBQRMS.WCFServices;
using BBQRMSSolution;
using BBQRMSSolution.ViewModels;
using Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Employee = BBQRMSSolution.ServerProxy.Employee;

// ReSharper disable InconsistentNaming
namespace BBQRMS.Client.Tests
{

	[TestClass]
	public class GivenNothing
	{
		private static Uri mServiceAddress;

		[ClassInitialize]
		public static void BeforeAllTests(TestContext testContext)
		{
			// start the data service
			mServiceAddress = new Uri("http://localhost:80/Temporary_Listen_Addresses/BBQRMSTestingGivenNothing/");
			Host.Open(mServiceAddress);
		}

		[ClassCleanup]
		public static void AfterAllTests()
		{
			Host.Close();
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
			mockEvents.Verify(e => e.Publish(It.Is<UserLoggedIn>(u => u.Employee.FirstName == "First")));
			mockEvents.Verify(e => e.Publish(It.IsAny<InvalidPinEntered>()), Times.Never());
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

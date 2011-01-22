using System;
using BBQRMS.WCFServices;
using BBQRMSSolution;
using BBQRMSSolution.ViewModels;
using Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable InconsistentNaming
namespace BBQRMS.Client.Tests
{
	[TestClass]
	public class GivenLoggedInUser
	{
		private static Uri mServiceAddress;

		[ClassInitialize]
		public static void BeforeAllTests(TestContext testContext)
		{
			// start the data service
			mServiceAddress = new Uri("http://localhost:80/Temporary_Listen_Addresses/BBQRMSTestingGivenLoggedInUser/");
			Host.Open(mServiceAddress);
		}

		[ClassCleanup]
		public static void AfterAllTests()
		{
			Host.Close();
		}

		[TestMethod]
		public void WhenLogoutIsClicked_ThenScreenRevertsToLoginScreenAndCurrentUserIsCleared()
		{
			MessageBus messageBus = new MessageBus();
			SecurityContext securityContext = new SecurityContext();
			//GlobalApplicationState.cs does this at runtime:
			messageBus.Subscribe(securityContext);
			
			// the MainWindowViewModel (and the view models created by it) subscribe themselves to the messageBus.
			MainWindowViewModel toTest = new MainWindowViewModel(mServiceAddress, messageBus, securityContext);
			var loginViewModel = (LoginViewModel)toTest.FullScreenContent;
			Assert.IsNull(securityContext.CurrentUser);

			loginViewModel.HandleLogin("1011");
			Assert.IsNotNull(securityContext.CurrentUser);
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof(PostLoginViewModel));
			var postLoginViewModel = (PostLoginViewModel) toTest.FullScreenContent;
		
			postLoginViewModel.HandleLogout();
			Assert.IsInstanceOfType(toTest.FullScreenContent, typeof(LoginViewModel));
			Assert.IsNull(securityContext.CurrentUser);
		}
	}
}
// ReSharper restore InconsistentNaming

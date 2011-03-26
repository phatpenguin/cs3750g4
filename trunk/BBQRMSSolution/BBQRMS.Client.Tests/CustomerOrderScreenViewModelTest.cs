using BBQRMS.WCFServices;
using BBQRMSSolution;
using BBQRMSSolution.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using Controls;
using Moq;
using BBQRMSEntities = BBQRMSSolution.ServerProxy.BBQRMSEntities;
using Menu = BBQRMSSolution.ServerProxy.Menu;
using MenuItem = BBQRMSSolution.ServerProxy.MenuItem;
using ClientTimeProvider = BBQRMSSolution.BusinessLogic.TimeProvider;
using ServerTimeProvider = BBQRMS.WCFServices.TimeProvider;

namespace BBQRMS.Client.Tests
{
    
    
    /// <summary>
    ///This is a test class for CustomerOrderScreenViewModelTest and is intended
    ///to contain all CustomerOrderScreenViewModelTest Unit Tests
    ///</summary>
    [TestClass]
    public class CustomerOrderScreenViewModelTest
    {

        private static Uri _mServiceAddress;

        private static readonly TimeProviderForTesting MTime = new TimeProviderForTesting();
        private static BBQRMSEntities _mDataService;
        private static CustomerOrderScreenViewModel _target;

        [ClassInitialize]
        public static void BeforeAllTests(TestContext testContext)
        {
            ServerTimeProvider.Current = MTime;
            ClientTimeProvider.Current = MTime;
            // start the data service
						_mServiceAddress = new Uri("http://localhost:80/Temporary_Listen_Addresses/BBQRMSTestingCustomerOrderScreenViewModelTest/");
            Host.Open(_mServiceAddress);
            _mDataService = new BBQRMSEntities(_mServiceAddress);

        	var mockDeviceManager = new Mock<IPOSDeviceManager>();

        	_target = new CustomerOrderScreenViewModel(_mDataService, new MessageBus(), mockDeviceManager.Object);
        }

        [ClassCleanup]
        public static void AfterAllTests()
        {
            Host.Close();
            ServerTimeProvider.ResetToDefault();
            ClientTimeProvider.ResetToDefault();
        }


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Payment
        ///</summary>
        [TestMethod]
        public void PaymentTest()
        {
            OrderViewModel orderTest = new OrderViewModel(new MessageBus(),_mDataService, new MockPOSDeviceManager());
            var expected = new PaymentViewModel(new BBQRMSEntities(_mServiceAddress), new MessageBus(),orderTest, new MockPOSDeviceManager());
            _target.Payment = expected;
            PaymentViewModel actual = _target.Payment;
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Order
        ///</summary>
        [TestMethod]
        public void OrderTest()
        {
            OrderViewModel expected = new OrderViewModel(new MessageBus(), _mDataService, new MockPOSDeviceManager());
            _target.Order = expected;
            OrderViewModel actual = _target.Order;
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Menus
        ///</summary>
        [TestMethod]
        public void MenusTest()
        {
            var m1 = Menu.CreateMenu(0, "FOOD", true);
            var m2 = Menu.CreateMenu(1, "FOOD2", true);
            var m3 = Menu.CreateMenu(2, "FOOD3", true);

            var mi1 = new MenuItem { Description = "MenuItem1", Id = 1, Price = 1.25m };
            var mi2 = new MenuItem { Description = "MenuItem2", Id = 2, Price = 2.25m };
            var mi3 = new MenuItem { Description = "MenuItem3", Id = 3, Price = 3.25m };

            m1.MenuItems.Add(mi1);
            m2.MenuItems.Add(mi2);
            m3.MenuItems.Add(mi3);

            var expected = new ObservableCollection<Menu> { m1, m2, m3 }; ; // TODO: Initialize to an appropriate value
            _target.Menus = expected;
            ObservableCollection<Menu> actual = _target.Menus;
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Cook
        ///</summary>
        [TestMethod]
        public void CookTest()
        {
            DelegateCommand actual = _target.Cook;
            Assert.IsNotNull(actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Cancel
        ///</summary>
        [TestMethod]
        public void CancelTest()
        {
            DelegateCommand actual = _target.Cancel;
            Assert.IsNotNull(actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddToOrder
        ///</summary>
        [TestMethod]
        public void AddToOrderTest()
        {
            DelegateCommand actual = _target.AddToOrder;
            Assert.IsNotNull(actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddPayment
        ///</summary>
        [TestMethod]
        public void AddPaymentTest()
        {
            DelegateCommand actual = _target.AddPayment;
            Assert.IsNotNull(actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for NewPayment
        ///</summary>
        [TestMethod]
        public void NewPaymentTest()
        {
            _target.NewPayment();
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CustomerOrderScreenViewModel Constructor
        ///</summary>
        [TestMethod]
        public void CustomerOrderScreenViewModelConstructorTest()
        {
					var mockDeviceManager = new Mock<IPOSDeviceManager>();
					try { _target = new CustomerOrderScreenViewModel(new BBQRMSEntities(_mServiceAddress), new MessageBus(), mockDeviceManager.Object); }
            catch(Exception){ Assert.Fail("Exception in Constructor");}
            finally {Assert.IsTrue(true);}
        }
    }
}

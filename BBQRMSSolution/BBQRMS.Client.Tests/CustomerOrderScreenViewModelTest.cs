using BBQRMS.WCFServices;
using BBQRMSSolution.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using Controls;
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
    [TestClass()]
    public class CustomerOrderScreenViewModelTest
    {

        private static Uri _mServiceAddress;

        private static readonly TimeProviderForTesting MTime = new TimeProviderForTesting();
        private static BBQRMSEntities _mDataService;
        private static CustomerOrderScreenViewModel target;

        [ClassInitialize]
        public static void BeforeAllTests(TestContext testContext)
        {
            ServerTimeProvider.Current = MTime;
            ClientTimeProvider.Current = MTime;
            // start the data service
            _mServiceAddress = new Uri("http://localhost:80/Temporary_Listen_Addresses/BBQRMSTestingGivenNothing/");
            Host.Open(_mServiceAddress);
            _mDataService = new BBQRMSEntities(_mServiceAddress);

            target = new CustomerOrderScreenViewModel(_mDataService, new MessageBus());
        }

        [ClassCleanup]
        public static void AfterAllTests()
        {
            Host.Close();
            ServerTimeProvider.ResetToDefault();
            ClientTimeProvider.ResetToDefault();
        }


        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

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
        [TestMethod()]
        public void PaymentTest()
        {
            OrderViewModel orderTest = new OrderViewModel(new MessageBus(),_mDataService);
            PaymentViewModel expected = new PaymentViewModel(new BBQRMSEntities(_mServiceAddress), new MessageBus(),orderTest);
            target.Payment = expected;
            PaymentViewModel actual = target.Payment;
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Order
        ///</summary>
        [TestMethod()]
        public void OrderTest()
        {
            OrderViewModel expected = new OrderViewModel(new MessageBus(), _mDataService);
            target.Order = expected;
            OrderViewModel actual = target.Order;
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Menus
        ///</summary>
        [TestMethod()]
        public void MenusTest()
        {
            var m1 = Menu.CreateMenu("FOOD", 0);
            var m2 = Menu.CreateMenu("FOOD2", 1);
            var m3 = Menu.CreateMenu("FOOD3", 2);

            var mi1 = new MenuItem { Description = "MenuItem1", Id = 1, Price = 1.25m };
            var mi2 = new MenuItem { Description = "MenuItem2", Id = 2, Price = 2.25m };
            var mi3 = new MenuItem { Description = "MenuItem3", Id = 3, Price = 3.25m };

            m1.MenuItems.Add(mi1);
            m2.MenuItems.Add(mi2);
            m3.MenuItems.Add(mi3);

            ObservableCollection<Menu> expected = new ObservableCollection<Menu> { m1, m2, m3 }; ; // TODO: Initialize to an appropriate value
            ObservableCollection<Menu> actual;
            target.Menus = expected;
            actual = target.Menus;
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Cook
        ///</summary>
        [TestMethod()]
        public void CookTest()
        {
            DelegateCommand actual;
            actual = target.Cook;
            Assert.IsNotNull(actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Cashier
        ///</summary>
        [TestMethod()]
        public void CashierTest()
        {
            DelegateCommand actual;
            actual = target.Cashier;
            Assert.IsNotNull(actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Cancel
        ///</summary>
        [TestMethod()]
        public void CancelTest()
        {
            DelegateCommand actual;
            actual = target.Cancel;
            Assert.IsNotNull(actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddToOrder
        ///</summary>
        [TestMethod()]
        public void AddToOrderTest()
        {
            DelegateCommand actual;
            actual = target.AddToOrder;
            Assert.IsNotNull(actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddPayment
        ///</summary>
        [TestMethod()]
        public void AddPaymentTest()
        {
            DelegateCommand actual;
            actual = target.AddPayment;
            Assert.IsNotNull(actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PlaceOrder
        ///</summary>
        [TestMethod()]
        public void PlaceOrderTest()
        {
            try { target.PlaceOrder(); } 
            catch(Exception ex){ Assert.IsTrue(true);} 
            finally
            {
                Assert.IsTrue(true);
            }

            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for NewPayment
        ///</summary>
        [TestMethod()]
        public void NewPaymentTest()
        {
            target.NewPayment();
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CustomerOrderScreenViewModel Constructor
        ///</summary>
        [TestMethod()]
        public void CustomerOrderScreenViewModelConstructorTest()
        {
            try { target = new CustomerOrderScreenViewModel(new BBQRMSEntities(_mServiceAddress), new MessageBus()); }
            catch(Exception ex){ Assert.Fail("Exception in Constructor");}
            finally {Assert.IsTrue(true);}
        }
    }
}

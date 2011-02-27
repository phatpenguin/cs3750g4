using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class CustomerOrderScreenViewModel : ViewModelBase
	{

	    private bool _paymentVisible;

        public DelegateCommand AddToOrder { get { return new DelegateCommand(Order.AddItem); } }

		public ObservableCollection<Menu> Menus { get; set; }
		public OrderViewModel Order { get; set; }

		public DelegateCommand Cancel { get { return new DelegateCommand(Order.CancelOrder); } }
		public DelegateCommand Cashier { get { return new DelegateCommand(PlaceOrder); } }
        public DelegateCommand Cook { get { return new DelegateCommand(Order.SendToCook); } }
        public DelegateCommand Payment { get { return new DelegateCommand(AddPayment); } }

        public bool PaymentVisible
        {
            get
            {
                return _paymentVisible;
            }
            set
            {
                if (value != _paymentVisible)
                {
                    _paymentVisible = value;
                    MessageBus.Publish(new ClockOutMode(_paymentVisible));
                    NotifyPropertyChanged("PaymentVisible");
                }
            }
        }


		[Obsolete("Used for design-time only", true)]
		public CustomerOrderScreenViewModel()
		{
		    PaymentVisible = false;

		    var m1 = Menu.CreateMenu("FOOD", 0);
		    var m2 = Menu.CreateMenu("FOOD2", 1);
		    var m3 = Menu.CreateMenu("FOOD3", 2);

		    var mi1 = new MenuItem {Description = "MenuItem1", Id = 1, Price = 1.25m};
		    var mi2 = new MenuItem {Description = "MenuItem2", Id = 2, Price = 2.25m};
		    var mi3 = new MenuItem {Description = "MenuItem3", Id = 3, Price = 3.25m};

            m1.MenuItems.Add(mi1);
            m2.MenuItems.Add(mi2);
            m3.MenuItems.Add(mi3);

            Menus = new ObservableCollection<Menu> {m1, m2, m3};

		    Order = new OrderViewModel();
		}

		public CustomerOrderScreenViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
		{
			DataService = dataService;
			MessageBus = messageBus;

			Order = new OrderViewModel(MessageBus,DataService);

		    Menus = new ObservableCollection<Menu>(DataService.Menus.Execute());
		    foreach (var m in Menus)
		        DataService.LoadProperty(m, "MenuItems");

		    foreach (var menu in Menus)
		        DataService.LoadProperty(menu, "MenuItems");
		}

		public void PlaceOrder()
		{
			var orderCashierScreenViewModel = new OrderCashierScreenViewModel(Order, MessageBus);

            Order = new OrderViewModel(MessageBus, DataService);
			NotifyPropertyChanged("order");

			MessageBus.Publish(new ShowScreen(orderCashierScreenViewModel));
		}

        public void AddPayment()
        {
            PaymentVisible = true;
        }
	}
}

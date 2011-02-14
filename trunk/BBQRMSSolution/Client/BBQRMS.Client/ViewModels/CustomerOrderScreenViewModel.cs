using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class CustomerOrderScreenViewModel : ViewModelBase
	{

        public DelegateCommand AddToOrder { get { return new DelegateCommand(Order.AddItem); } }

		public ObservableCollection<Menu> Menus { get; set; }
		public OrderViewModel Order { get; set; }

		public DelegateCommand Cancel { get { return new DelegateCommand(CancelOrder); } }
		public DelegateCommand Cashier { get { return new DelegateCommand(PlaceOrder); } }

		[Obsolete("Used for design-time only", true)]
		public CustomerOrderScreenViewModel()
		{
		    Menu m1 = Menu.CreateMenu("FOOD", 0);
		    Menu m2 = Menu.CreateMenu("FOOD2", 1);
		    Menu m3 = Menu.CreateMenu("FOOD3", 2);

            Menus = new ObservableCollection<Menu>();
            Menus.Add(m1);
            Menus.Add(m2);
            Menus.Add(m3);

		}

		public CustomerOrderScreenViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
		{
			DataService = dataService;
			MessageBus = messageBus;

			Order = new OrderViewModel(MessageBus,DataService);

		    Menus = new ObservableCollection<Menu>(DataService.Menus.Execute());
		    foreach(var m in Menus)
		    {
		        DataService.LoadProperty(m, "MenuItems");
		    }

		    foreach (Menu menu in Menus)
		    {
		        DataService.LoadProperty(menu, "MenuItems");
		    }
            
		}

		public void CancelOrder()
		{
			Order = new OrderViewModel(MessageBus,DataService);
			NotifyPropertyChanged("order");
		}

		public void PlaceOrder()
		{
			var orderCashierScreenViewModel = new OrderCashierScreenViewModel(Order, MessageBus);

            Order = new OrderViewModel(MessageBus, DataService);
			NotifyPropertyChanged("order");

			MessageBus.Publish(new ShowScreen(orderCashierScreenViewModel));
		}
	}
}

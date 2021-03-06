﻿using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class CustomerOrderScreenViewModel : ViewModelBase
	{
		private readonly IPOSDeviceManager _posDeviceManager;
		public DelegateCommand AddToOrder { get { return new DelegateCommand(Order.AddItem); } }

		public ObservableCollection<Menu> Menus { get; set; }
		public OrderViewModel Order { get; set; }
        public PaymentViewModel Payment { get; set; }
        public DiscountViewModel Discount { get; set; }

		public DelegateCommand Cancel { get { return new DelegateCommand(Order.CancelOrder); } }
        public DelegateCommand Cook { get { return new DelegateCommand(Order.SendToCook); } }
        public DelegateCommand AddPayment { get { return new DelegateCommand(NewPayment); } }
        public DelegateCommand AddDiscount { get { return new DelegateCommand(NewDiscount); } }
        public DelegateCommand RemoveItem { get { return new DelegateCommand(Order.RemoveItem); } }
        public DelegateCommand Clear {get {return new DelegateCommand(Order.ClearItems);}}

		[Obsolete("Used for design-time only", true)]
		public CustomerOrderScreenViewModel()
		{
		    var m1 = Menu.CreateMenu(0,"FOOD", true);
            var m2 = Menu.CreateMenu(1, "FOOD2", true);
            var m3 = Menu.CreateMenu(2, "FOOD3", true);

		    var mi1 = new MenuItem {Description = "MenuItem1", Id = 1, Price = 1.25m};
		    var mi2 = new MenuItem {Description = "MenuItem2", Id = 2, Price = 2.25m};
		    var mi3 = new MenuItem {Description = "MenuItem3", Id = 3, Price = 3.25m};

            m1.MenuItems.Add(mi1);
            m2.MenuItems.Add(mi2);
            m3.MenuItems.Add(mi3);

            Menus = new ObservableCollection<Menu> {m1, m2, m3};

		    Order = new OrderViewModel();
            Payment = new PaymentViewModel();
		}

		public CustomerOrderScreenViewModel(BBQRMSEntities dataService, IMessageBus messageBus, IPOSDeviceManager posDeviceManager)
		{
			_posDeviceManager = posDeviceManager;
			DataService = dataService;
			MessageBus = messageBus;

			Order = new OrderViewModel(MessageBus,DataService, posDeviceManager);

		    Menus = new ObservableCollection<Menu>(DataService.Menus.Execute());

		    foreach (var menu in Menus)
		        DataService.LoadProperty(menu, "MenuItems");
		}

        public CustomerOrderScreenViewModel(BBQRMSEntities dataService, IMessageBus messageBus, IPOSDeviceManager posDeviceManager, Order order)
        {
            _posDeviceManager = posDeviceManager;
            DataService = dataService;
            MessageBus = messageBus;

            Order = new OrderViewModel(MessageBus, DataService, posDeviceManager, order);

            Menus = new ObservableCollection<Menu>(DataService.Menus.Execute());

            foreach (var menu in Menus)
                DataService.LoadProperty(menu, "MenuItems");
        }

        public void NewPayment()
        {
            Payment = new PaymentViewModel(DataService,MessageBus,Order, _posDeviceManager);
            NotifyPropertyChanged("Payment");
        }

        public void NewDiscount()
        {
            Discount = new DiscountViewModel(DataService, MessageBus, Order);
            NotifyPropertyChanged("Discount");
        }

				private void ClaimPOSDevices()
				{
					//  open (if not already opened), claim and enable the cash drawer and receipt printer.
					ICashDrawer drawer = _posDeviceManager.GetCashDrawer();
					drawer.Claim();
					drawer.Enable();
					IReceiptPrinter printer = _posDeviceManager.GetReceiptPrinter();
					printer.Claim();
					printer.Enable();
				}

				private void UnclaimDevices()
				{
					ICashDrawer drawer = _posDeviceManager.GetCashDrawer();
					drawer.Release();
					IReceiptPrinter printer = _posDeviceManager.GetReceiptPrinter();
					printer.Release();
				}
				
		public override void Open()
				{
					base.Open();
					ClaimPOSDevices();
				}

				public override void Close()
				{
					UnclaimDevices();
					base.Close();
				}
	}
}

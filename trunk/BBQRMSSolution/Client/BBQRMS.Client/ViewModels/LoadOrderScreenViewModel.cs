using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
    public class LoadOrderScreenViewModel : ViewModelBase
    {
        public ICollectionView Orders { get; set; }
        public Order Order { get; set; }

        private readonly IPOSDeviceManager _posDeviceManager;
        private int run = 0;

        [Obsolete("Used for design-time only", true)]
        public LoadOrderScreenViewModel()
        {
            Orders = CollectionViewSource.GetDefaultView(new ObservableCollection<Order>
                                                             {
                                                                 new Order
                                                                     {
                                                                         Date = DateTime.Now,
                                                                         DinerTypeId = 1,
                                                                         Number = 1,
                                                                         OrderStateId = 1,
                                                                         PaymentStateId = 1,
                                                                         Memo = "TEST"
                                                                     },
                                                                 new Order()
                                                                     {
                                                                         Date = DateTime.Now,
                                                                         DinerTypeId = 2,
                                                                         Number = 2,
                                                                         OrderStateId = 2,
                                                                         PaymentStateId = 2,
                                                                         Memo = "TEST2"
                                                                     }
                                                             });
        }

        public LoadOrderScreenViewModel(BBQRMSEntities dataService, IMessageBus messageBus, IPOSDeviceManager posDeviceManager)
        {
            DataService = dataService;
            MessageBus = messageBus;
            _posDeviceManager = posDeviceManager;

            Orders = CollectionViewSource.GetDefaultView(new ObservableCollection<Order>(DataService.Orders.Execute().Where(x => x.OrderStateId != 6)));
            Orders.CurrentChanged += new EventHandler(Orders_CurrentChanged);
            
            foreach (var order in Orders)
            {
                DataService.LoadProperty(order, "OrderState");
                DataService.LoadProperty(order, "DinerType");
                DataService.LoadProperty(order, "PaymentState");
            }
        }

        void Orders_CurrentChanged(object sender, EventArgs e)
        {
            if(run > 1)
            MessageBus.Publish(new ShowScreen(new CustomerOrderScreenViewModel(DataService, MessageBus, _posDeviceManager, (Order)Orders.CurrentItem)));
            run++;
        }
    }
}

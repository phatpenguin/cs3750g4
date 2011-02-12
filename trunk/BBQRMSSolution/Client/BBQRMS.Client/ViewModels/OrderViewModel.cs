using System;
using System.Data.Services.Client;
using System.Threading;
using Controls;
using BBQRMSSolution.ServerProxy;

namespace BBQRMSSolution.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private Timer _t;
        private decimal _st, _tp, _ta;

        private readonly IMessageBus _mMessageBus;
        private readonly BBQRMSEntities _mDataService;

        public Order Order { get; set; }

        private const decimal TAX_PERCENTAGE = 8.25m;

        public decimal SubTotal { get { return _st; } set { _st = value; NotifyPropertyChanged("subTotal"); } }
        public decimal TotalPrice { get { return _tp; } set { _tp = value; NotifyPropertyChanged("totalPrice"); } }
        public decimal TaxAmount { get { return _ta; } set { _ta = value; NotifyPropertyChanged("taxAmount"); } }

        public OrderViewModel(IMessageBus mMessageBus, BBQRMSEntities mDataService)
        {
            _mMessageBus = mMessageBus;
            _mDataService = mDataService;

            DateTime now = DateTime.Now;
            now = now.AddMilliseconds(-now.Millisecond);
            OrderSubmittedDate = now;
            _t = new Timer(UpdateAge, null, 0, 1000);

            TotalPrice = 0.00m;
            SubTotal = 0.00m;
            TaxAmount = 0.00m;

            //id,ordertypeid,number,date,dinertypeid,paymentstatusid,orderstatusid
            Order = Order.CreateOrder(0, 1, DateTime.Now, 1, 1, 1, 1);
            _mDataService.AddToOrders(Order);
            DataServiceResponse dataServiceResponse = _mDataService.SaveChanges();
        }

        private void UpdateAge(object state)
        {
            OrderAge = DateTime.Now - OrderSubmittedDate;
        }

        public DateTime OrderSubmittedDate { get; set; }
        private int _mOrderNumber;

        public int OrderNumber
        {
            get { return _mOrderNumber; }
            set
            {
                if (value != _mOrderNumber)
                {
                    _mOrderNumber = value;
                    NotifyPropertyChanged("OrderNumber");
                }
            }
        }

        private TimeSpan _mOrderAge;

        public TimeSpan OrderAge
        {
            get { return _mOrderAge; }
            set
            {
                if (value == _mOrderAge) return;
                _mOrderAge = value;
                NotifyPropertyChanged("OrderAge");
            }
        }

        public void AddItem(Object mi)
        {
            var menuItem = (MenuItem)mi;
            var isFound = true;

            foreach (var oi in Order.OrderItems)
            {
                isFound = true;

                if (oi.MenuItemId == menuItem.Id)
                {
                    oi.Quantity++;
                    NotifyPropertyChanged("oi");
                    break;
                }
                isFound = false;
            }
            if (!isFound || Order.OrderItems.Count == 0)
            {
                //int id, int orderId, String name, int qty, decimal unitprice, decimal unittax, int menuitemid
                var orderItem = OrderItem.CreateOrderItem(0, Order.Id, menuItem.Name, 1, (decimal)menuItem.Price,
                    (decimal)(menuItem.Price*TAX_PERCENTAGE), menuItem.Id);

                Order.OrderItems.Add(orderItem);
                _mDataService.SaveChanges();
            }

            SubTotal += (decimal)menuItem.Price;
            TaxAmount = SubTotal * (TAX_PERCENTAGE / 100);
            TotalPrice = SubTotal + TaxAmount;
        }

        public void RemoveItem(Object oi)
        {
            var orderItem = (OrderItem)oi;

            if (orderItem.Quantity > 1) orderItem.Quantity--;
            else Order.OrderItems.Remove(orderItem);

            SubTotal -= (decimal)orderItem.MenuItem.Price;
            TaxAmount = SubTotal * (TAX_PERCENTAGE / 100);
            TotalPrice = SubTotal + TaxAmount;
        }
    }
}
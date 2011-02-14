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


        public Order Order { get; set; }

        private const decimal TAX_PERCENTAGE = .0825m;

        public decimal SubTotal { get { return _st; } set { _st = value; NotifyPropertyChanged("subTotal"); } }
        public decimal TotalPrice { get { return _tp; } set { _tp = value; NotifyPropertyChanged("totalPrice"); } }
        public decimal TaxAmount { get { return _ta; } set { _ta = value; NotifyPropertyChanged("taxAmount"); } }

			[Obsolete("Used for design-time only", true)]
    	public OrderViewModel()
    	{
    		//TODO: give all the properties some simulated data for the VS designer.
    	}
        public OrderViewModel(IMessageBus messageBus, BBQRMSEntities dataService)
        {
            MessageBus = messageBus;
            DataService = dataService;

            DateTime now = DateTime.Now;
            now = now.AddMilliseconds(-now.Millisecond);
            OrderSubmittedDate = now;
            _t = new Timer(UpdateAge, null, 0, 1000);

            TotalPrice = 0.00m;
            SubTotal = 0.00m;
            TaxAmount = 0.00m;

            //id,ordertypeid,number,date,dinertypeid,paymentstatusid,orderstatusid
            Order = Order.CreateOrder(0, 1, DateTime.Now, 1, 1, 1, 1);
            DataService.AddToOrders(Order);
            DataServiceResponse dataServiceResponse = DataService.SaveChanges();
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
                    DataService.UpdateObject(oi);
                    DataService.SaveChanges();
                    break;
                }
                isFound = false;
            }
            if (!isFound || Order.OrderItems.Count == 0)
            {
                //int id, int orderId, String name, int qty, decimal unitprice, decimal unittax, int menuitemid
                var orderItem = OrderItem.CreateOrderItem(0, Order.Id, menuItem.Name, 1, (decimal)menuItem.Price,
                    (decimal)(menuItem.Price*TAX_PERCENTAGE), menuItem.Id);

                DataService.AddToOrderItems(orderItem);
                Order.OrderItems.Add(orderItem);
                DataService.UpdateObject(Order);
                DataService.SaveChanges();
            }

            CalculateTotals();
        }

        public void RemoveItem(Object oi)
        {
            var orderItem = (OrderItem)oi;

            if (orderItem.Quantity > 1) orderItem.Quantity--;
            else Order.OrderItems.Remove(orderItem);

            CalculateTotals();
        }

        private void CalculateTotals()
        {
            SubTotal = 0m;
            TaxAmount = 0m;
            foreach (var oi in Order.OrderItems)
            {
                SubTotal += oi.UnitPrice*oi.Quantity;
                TaxAmount += Math.Round(oi.UnitTax*oi.Quantity,2);
            }
            TotalPrice = SubTotal + TaxAmount;
        }
    }
}
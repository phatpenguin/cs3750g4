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
        private decimal _st, _tp, _ta, _da, _pa, _ra;

        private const int DEFAULT_ORDER_TYPE_ID = 2;
        private const int DEFAULT_DINER_TYPE_ID = 1;
        private const int DEFAULT_PAYMENT_STATE_ID = 1;
        private const int DEFAULT_ORDER_STATE_ID = 5;

        public Order Order { get; set; }

        private const decimal TAX_PERCENTAGE = .0825m;

        public decimal SubTotal { get { return _st; } set { _st = value; NotifyPropertyChanged("SubTotal"); } }
        public decimal TotalPrice { get { return _tp; } set { _tp = value; NotifyPropertyChanged("TotalPrice"); } }
        public decimal DiscountAmount { get { return _da; } set { _da = value; NotifyPropertyChanged("DiscountAmount"); } }
        public decimal PaymentAmount { get { return _pa; } set { _pa = value; NotifyPropertyChanged("PaymentAmount"); } }
        public decimal RemainingAmount { get { return _ra; } set { _ra = value; NotifyPropertyChanged("RemainingAmount"); } }
        public decimal TaxAmount { get { return _ta; } set { _ta = value; NotifyPropertyChanged("TaxAmount"); } }

		[Obsolete("Used for design-time only", true)]
    	public OrderViewModel()
		{
		    Order = new Order()
		                {
		                    Date = DateTime.Now,
		                    DinerTypeId = DEFAULT_DINER_TYPE_ID,
		                    Id = 0,
		                    Number = 0,
		                    OrderStateId = 1
		                };

            var mi1 = new MenuItem { Description = "MenuItem1", Id = 1, Price = 1.25m };
            var mi2 = new MenuItem { Description = "MenuItem2", Id = 2, Price = 2.25m };
            var mi3 = new MenuItem { Description = "MenuItem3", Id = 3, Price = 3.25m };

		    Order.OrderItems.Add(new OrderItem { MenuItemId = 1, Name = "Item1", Quantity = 1, UnitPrice = 3.50m, UnitTax = 1.00m });
            Order.OrderItems.Add(new OrderItem { MenuItemId = 1, Name = "Item1", Quantity = 1, UnitPrice = 3.50m, UnitTax = 1.00m });
            Order.OrderItems.Add(new OrderItem { MenuItemId = 1, Name = "Item1", Quantity = 1, UnitPrice = 3.50m, UnitTax = 1.00m });
		}

        public OrderViewModel(IMessageBus messageBus, BBQRMSEntities dataService, int number=0)
        {
            MessageBus = messageBus;
            DataService = dataService;

            LoadNewOrder();
        }

        private void LoadNewOrder ()
        {
            var now = DateTime.Now;
            now = now.AddMilliseconds(-now.Millisecond);
            OrderSubmittedDate = now;
            _t = new Timer(UpdateAge, null, 0, 1000);

            Order = new Order
            {
                DinerTypeId = DEFAULT_DINER_TYPE_ID,
                Number = 0,
                Date = DateTime.Now,
                OrderTypeId = DEFAULT_ORDER_TYPE_ID,
                PaymentStateId = DEFAULT_PAYMENT_STATE_ID,
                OrderStateId = DEFAULT_ORDER_STATE_ID
            };

            CalculateTotals();

            DataService.AddToOrders(Order);
            DataService.SaveChanges();
            Order.Number = Order.Id % 1000;
            DataService.UpdateObject(Order);
            DataService.SaveChanges();
            NotifyPropertyChanged("Order");
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
                if (value == _mOrderNumber) return;
                _mOrderNumber = value;
                NotifyPropertyChanged("OrderNumber");
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
                var orderItem = new OrderItem { Id = 0, OrderId = Order.Id, Name = menuItem.Name, Quantity = 1, UnitPrice = (decimal)menuItem.Price,
                UnitTax = (decimal)(menuItem.Price*TAX_PERCENTAGE),MenuItemId = menuItem.Id};

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

            if (orderItem.Quantity > 1)
            {
                orderItem.Quantity--;
                DataService.UpdateObject(orderItem);
                DataService.UpdateObject(Order);
                DataService.SaveChanges();
            }
            else
            {
                DataService.DeleteObject(orderItem);
                Order.OrderItems.Remove(orderItem);
                DataService.UpdateObject(Order);
                DataService.SaveChanges();
            }

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
            RemainingAmount = TotalPrice - PaymentAmount - DiscountAmount;
        }

        public void SendToCook()
        {
            Order.OrderStateId = 1;

            DataService.UpdateObject(Order);
            DataService.SaveChanges();

            LoadNewOrder();
        }

        public void CancelOrder()
        {
            foreach (var oi in Order.OrderItems)
            {
                DataService.DeleteObject(oi);
            }

            DataService.DeleteObject(Order);
            DataService.SaveChanges();

            LoadNewOrder();
        }

        public void AddPayment()
        {
            
        }
    }
}
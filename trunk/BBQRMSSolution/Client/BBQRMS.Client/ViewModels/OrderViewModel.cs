using System;
using System.Linq;
using System.Windows;
using Controls;
using BBQRMSSolution.ServerProxy;

namespace BBQRMSSolution.ViewModels
{
	public class OrderViewModel : ViewModelBase
	{
		private readonly IPOSDeviceManager _posDeviceManager;
		private decimal _st, _tp, _ta, _da, _pa, _ra;

		private const int DEFAULT_ORDER_TYPE_ID = OrderTypes.WalkIn;
		private const int DEFAULT_DINER_TYPE_ID = DinerTypes.DineIn;
		private const int DEFAULT_PAYMENT_STATE_ID = PaymentStates.Unpaid;
		private const int DEFAULT_ORDER_STATE_ID = OrderStates.InProgress;

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
			Order = new Order
									{
										Date = DateTime.Now,
										DinerTypeId = DEFAULT_DINER_TYPE_ID,
										Id = 0,
										Number = 0,
										OrderStateId = OrderStates.InProgress
									};

			Order.OrderItems.Add(new OrderItem { MenuItemId = 1, Name = "Item1", Quantity = 1, UnitPrice = 3.50m, UnitTax = 1.00m });
			Order.OrderItems.Add(new OrderItem { MenuItemId = 1, Name = "Item1", Quantity = 1, UnitPrice = 3.50m, UnitTax = 1.00m });
			Order.OrderItems.Add(new OrderItem { MenuItemId = 1, Name = "Item1", Quantity = 1, UnitPrice = 3.50m, UnitTax = 1.00m });
		}

		public OrderViewModel(IMessageBus messageBus, BBQRMSEntities dataService, IPOSDeviceManager posDeviceManager)
		{
			_posDeviceManager = posDeviceManager;
			MessageBus = messageBus;
			DataService = dataService;

			LoadNewOrder();
		}

        public OrderViewModel(IMessageBus messageBus, BBQRMSEntities dataService, IPOSDeviceManager posDeviceManager, Order order)
        {
            _posDeviceManager = posDeviceManager;
            MessageBus = messageBus;
            DataService = dataService;

            Order = DataService.Orders.Where(x => x.Id == order.Id).FirstOrDefault();
            DataService.LoadProperty(Order, "OrderItems");
            DataService.LoadProperty(Order, "Payments");

            CalculateTotals();
        }

		private void LoadNewOrder()
		{
			var now = DateTime.Now;
			now = now.AddMilliseconds(-now.Millisecond);
			OrderSubmittedDate = now;

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


			NotifyPropertyChanged("Order");
		}

        private void SaveOrder()
        {
            if (Order.Number < 1)
            {
                DataService.AddToOrders(Order);
                DataService.SaveChanges();
                Order.Number = Order.Id % 1000;
                foreach (var oi in Order.OrderItems)
                {
                    oi.OrderId = Order.Id;
                    DataService.AddToOrderItems(oi);
                }
                foreach (var p in Order.Payments)
                {
                    p.OrderId = Order.Id;
                    DataService.AddToPayments(p);
                }
                DataService.UpdateObject(Order);
                DataService.SaveChanges();

                DataService.LoadProperty(Order, "OrderItems");
                DataService.LoadProperty(Order,"Payments");
            }
            else
            {
                UpdateOrderItems();
                //UpdatePayments();

                DataService.UpdateObject(Order);
                DataService.SaveChanges();
            }


            NotifyPropertyChanged("Order");
        }

        private void UpdateOrderItems()
        {
            var oldOrderItems =
                DataService
                    .Links
                    .Where(ld => ld.Source == Order && ld.SourceProperty == "OrderItems")
                    .Select(ld => ld.Target)
                    .OfType<OrderItem>();

            var newOrderItems = Order.OrderItems;

            var toAdd = newOrderItems.Except(oldOrderItems);
            var toRemove = oldOrderItems.Except(newOrderItems);

            foreach (var orderItem in toRemove)
            {
                DataService.DeleteObject(orderItem);
            }
            foreach (var orderItem in toAdd)
            {
                DataService.AddToOrderItems(orderItem);
            }
            foreach (var orderItem in Order.OrderItems)
            {
                DataService.UpdateObject(orderItem);
            }

            DataService.SaveChanges();
        }

        /*private void UpdatePayments()
        {
            var oldPayments =
                DataService
                    .Links
                    .Where(ld => ld.Source == Order && ld.SourceProperty == "Payments")
                    .Select(ld => ld.Target)
                    .OfType<Payment>();

            var newPayments = Order.Payments;

            var toAdd = newPayments.Except(oldPayments);
            var toRemove = oldPayments.Except(newPayments);

            foreach (var payment in toRemove)
            {
                DataService.DeleteObject(payment);
            }
            foreach (var payment in toAdd)
            {
                DataService.AddToPayments(payment);
            }
            foreach (var payment in Order.Payments)
            {
                DataService.UpdateObject(payment);
            }

            DataService.SaveChanges();
        }*/

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
					break;
				}
				isFound = false;
			}
			if (!isFound || Order.OrderItems.Count == 0)
			{
				var orderItem = new OrderItem
				{
					Id = 0,
					OrderId = Order.Id,
					Name = menuItem.Name,
					Quantity = 1,
					UnitPrice = menuItem.Price,
					UnitTax = menuItem.Price * TAX_PERCENTAGE,
					MenuItemId = menuItem.Id
				};

				Order.OrderItems.Add(orderItem);
			}

			CalculateTotals();
		}

		public void RemoveItem(Object oi)
		{
			var orderItem = (OrderItem)oi;

			if (orderItem.Quantity > 1)
			{
				orderItem.Quantity--;
			}
			else
			{
				Order.OrderItems.Remove(orderItem);
			}

			CalculateTotals();
		}

		private void CalculateTotals()
		{
			SubTotal = 0m;
			TaxAmount = 0m;
		    PaymentAmount = 0m;

			foreach (var oi in Order.OrderItems)
			{
				SubTotal += oi.UnitPrice * oi.Quantity;
				TaxAmount += Math.Round(oi.UnitTax * oi.Quantity, 2);
			}
            foreach (var p in Order.Payments)
            {
                PaymentAmount += p.Amount;
            }
			TotalPrice = SubTotal + TaxAmount;
			RemainingAmount = TotalPrice - PaymentAmount - DiscountAmount;
		}

		public void SendToCook()
		{
			Order.OrderStateId = OrderStates.Cooking;

		    SaveOrder();

			LoadNewOrder();
		}

		public void CancelOrder()
		{
		    Order.OrderStateId = OrderStates.Cancelled;
            if (Order.Number < 1)
            {
                SaveOrder();
            }
            else
            {
                DataService.UpdateObject(Order);
                DataService.SaveChanges();               
            }

		    LoadNewOrder();
		}

		public void AddPayment(Payment payment)
		{
            Order.Payments.Add(payment);
            if (Order.Payments.Count == 1)
		    {
            	SaveOrder();
		    }
		    else
            {
                DataService.AddToPayments(payment);
                DataService.UpdateObject(payment);
            }

			CalculateTotals();
			if (RemainingAmount <= 0)
			{
				PrintReceipt();
				//TODO: now that we've printed the receipt, everything should be done and we need to close out this order.
			    Order.PaymentStateId = PaymentStates.Paid;
			}
            else
			{
			    Order.PaymentStateId = PaymentStates.PartiallyPaid;
			}


            DataService.UpdateObject(Order);
            DataService.SaveChanges();
		}

		public void PrintReceipt()
		{
			var prnt = _posDeviceManager.GetReceiptPrinter();

			// print heading
			prnt.PrintStoredLogo(1);
			prnt.PrintLine("\x1b|cAThis came from BBQRMS!!!!");
			prnt.PrintLine("\x1b|cA" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
			
			// print order number
			prnt.PrintLine("");
			prnt.PrintLine("\x1b|2C\x1b|cAOrder #: " + Order.Number);
			
			// print out items
			foreach (OrderItem item in Order.OrderItems)
			{
				prnt.PrintLine(item.Name);
				var itemPriceLine = FormatReceiptItemLine(item);
				prnt.PrintLine(itemPriceLine);
			}

			// print out discounts
			if (Order.Discounts.Any())
			{
				prnt.PrintLine("");
				foreach (Discount discount in Order.Discounts)
				{
					prnt.PrintLine(string.Format("\x1b|rA\x1b|bCDiscount: \x1b|N{0:C}", discount.Amount));
				}
			}

			// print out totals
			prnt.PrintLine(string.Format("\x1b|rA\x1b|2CSubtotal:\x1b|1C {0:C}", SubTotal));
			prnt.PrintLine(string.Format("\x1b|rATax: {0:C}", TaxAmount));
			prnt.PrintLine(string.Format("\x1b|rA\x1b|2CTotal: {0:C}", TotalPrice));

			// print out payments
			prnt.PrintLine("");
			foreach (var paymentByType in Order.Payments.GroupBy(p => p.PaymentTypeId))
			{
				string desc = "Paid";
				switch (paymentByType.Key)
				{
						//TODO: a better lookup
					case PaymentTypes.Cash:
						desc = "Cash";
						break;
					case PaymentTypes.CreditCard:
						desc = "Credit";
						break;
					case PaymentTypes.Check:
						desc = "Check";
						break;
				}
				var totalForType = paymentByType.Sum(pmt => pmt.Amount);
				prnt.PrintLine(string.Format("\x1b|rA{0}: {1:C}", desc, totalForType));
			}

			// print out change due.
			decimal totalPaid = Order.Payments.Sum(p => p.Amount);
			if(totalPaid > TotalPrice)
			{
				decimal change = totalPaid - TotalPrice;
				prnt.PrintLine(string.Format("\x1b|rA\x1b|2CChange due: \x1b|1C{0:C}", change));
			}
			prnt.FeedToCut();
			prnt.Cut();
		}

		private static string FormatReceiptItemLine(OrderItem item)
		{
			var firstPart = String.Format("   {0} @ {1}", item.Quantity, item.UnitPrice);
			var total = (item.Quantity*item.UnitPrice).ToString();
			var toReturn = firstPart + new string(' ', 48 - firstPart.Length - total.Length) + total;
			return toReturn;
		}

		public bool AddDiscount(Discount discount)
		{
            if(discount.Amount > RemainingAmount)
            {
                MessageBox.Show("You cannot discount more than the amount due.", "Confirmation");
                return false;
            }

                Order.Discounts.Add(discount);

                DiscountAmount += discount.Amount;
                CalculateTotals();

                return true;
		}

        public void ClearItems()
        {
            //TODO better logic to remove items?
            Order.OrderItems.Clear();
            CalculateTotals();
        }
	}
}
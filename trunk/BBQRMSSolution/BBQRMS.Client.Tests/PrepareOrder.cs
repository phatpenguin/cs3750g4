using System;
using BBQRMSSolution.ServerProxy;

namespace BBQRMS.Client.Tests
{
	public static class PrepareOrder
	{
		public static Order For(DateTime date, int dinerType = DinerTypes.DineIn, int orderType = OrderTypes.WalkIn, int orderState = OrderStates.InProgress, int paymentState = PaymentStates.Unpaid, string memo = null)
		{
			var order =
				new Order
					{
						Date = date,
						DinerTypeId = dinerType,
						Memo = memo,
						OrderStateId = orderState,
						OrderTypeId = orderType,
						PaymentStateId = paymentState,
					};
			return order;
		}

		public static Order ForItem(this Order order, int quantity, MenuItem item)
		{
			var orderItem =
				new OrderItem
					{
						Quantity = quantity,
						MenuItem = item,
						MenuItemId = item.Id,
						Name = item.Name,
						UnitPrice = item.Price,
					};
			order.OrderItems.Add(orderItem);
			return order;
		}

		public static Order WithPayment(this Order order, decimal amount, int paymentType, string memo = null)
		{
			var payment =
				new Payment
					{
						Amount = amount,
						PaymentTypeId = paymentType,
						Memo = memo
					};
			order.Payments.Add(payment);
			return order;
		}
	}
}
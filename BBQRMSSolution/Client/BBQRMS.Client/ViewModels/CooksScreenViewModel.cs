using System;
using System.Collections.ObjectModel;
using System.Linq;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class CooksScreenViewModel : ViewModelBase
	{
        private ObservableCollection<Order> _pendingOrders;

		[Obsolete("Used for design-time only", true)]
		public CooksScreenViewModel()
		{
			PendingOrders =
				new ObservableCollection<Order>
					{
						new Order
							{
								Number = 1,
//								OrderItems = 
//								new DataServiceCollection<OrderItem>
//								             	{
//								             		new OrderItem { Name = "Sandwich", Quantity = 1}
//								             	}
							},
						new Order
							{
								Number = 2,
//								OrderItems = new DataServiceCollection<OrderItem>
//								             	{
//								             		new OrderItem {Name = "Soda", Quantity = 2}
//								             	}
							},
					};
		}

		public CooksScreenViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
		{
			DataService = dataService;
			MessageBus = messageBus;

            PendingOrders = new ObservableCollection<Order>(DataService.Orders.Expand("OrderItems").Where(x => x.OrderStateId == 1));

			CompleteOrderCommand = new DelegateCommand(HandleCompleteOrder);
		}

		public ObservableCollection<Order> PendingOrders
		{
			get { return _pendingOrders; }
            set { _pendingOrders = value; NotifyPropertyChanged("PendingOrders"); }
		}

		public DelegateCommand CompleteOrderCommand { get; private set; }

		private void HandleCompleteOrder(object parameter)
		{
			var order = (Order) parameter;
		    order.OrderStateId = 3;
            DataService.UpdateObject(order);
		    DataService.SaveChanges();

			PendingOrders.Remove(order);
		}
	}
}
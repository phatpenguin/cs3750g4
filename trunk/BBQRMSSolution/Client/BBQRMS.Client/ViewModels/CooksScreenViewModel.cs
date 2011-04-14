using System;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Linq;
using System.Windows.Threading;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class CooksScreenViewModel : ViewModelBase
	{
		private ObservableCollection<Order> _pendingOrders;
		private DispatcherTimer _timer;

		[Obsolete("Used for design-time only", true)]
		public CooksScreenViewModel()
		{
			PendingOrders =
				new ObservableCollection<Order>
					{
						new Order
							{
								Number = 1,
								OrderItems = 
								new DataServiceCollection<OrderItem>
								             	{
								             		new OrderItem { Name = "Sandwich", Quantity = 1}
								             	}
							},
						new Order
							{
								Number = 2,
								OrderItems = new DataServiceCollection<OrderItem>
								             	{
						             		new OrderItem {Name = "Soda", Quantity = 2}
							             	}
							},
					};
		}

		public CooksScreenViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
		{
			DataService = dataService;
			MessageBus = messageBus;
			var oldMergeOption = DataService.MergeOption;
			DataService.MergeOption = MergeOption.OverwriteChanges;
			PendingOrders =
				new ObservableCollection<Order>(DataService.Orders.Expand("OrderItems").Where(x => x.OrderStateId == OrderStates.Cooking));
			DataService.MergeOption = oldMergeOption;

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
			var order = (Order)parameter;
			//TODO: if the order is fully paid, we can close it, otherwise we should just mark it 'completed'
            order.OrderStateId = order.PaymentStateId == PaymentStates.Paid ? OrderStates.Closed : OrderStates.Completed;

			DataService.UpdateObject(order);
			DataService.SaveChanges();

			PendingOrders.Remove(order);
		}

		public override void Open()
		{
			base.Open();
			// start a dispatcher timer to periodically requery the Orders.
			_timer = new DispatcherTimer(TimeSpan.FromSeconds(2d), DispatcherPriority.Normal, ReQueryOrders,
			                            Dispatcher.CurrentDispatcher);
			_timer.Start();
		}

		private void ReQueryOrders(object sender, EventArgs e)
		{
			var oldMergeOption = DataService.MergeOption;
			DataService.MergeOption = MergeOption.OverwriteChanges;
			var orders = DataService.Orders.Expand("OrderItems").Where(x => x.OrderStateId == OrderStates.Cooking).ToList();
			foreach (var order in orders.Where(order => !PendingOrders.Contains(order)))
			{
			    PendingOrders.Add(order);
			}
			foreach (var pendingOrder in PendingOrders.Where(pendingOrder => !orders.Contains(pendingOrder)))
			{
			    PendingOrders.Remove(pendingOrder);
			}
			DataService.MergeOption = oldMergeOption;
		}

		public override void Close()
		{
			// stop the dispatcher timer
			_timer.Stop();
			base.Close();
		}
	}
}
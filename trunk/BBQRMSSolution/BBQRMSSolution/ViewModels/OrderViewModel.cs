using System;
using System.Collections.ObjectModel;
using System.Threading;
using BBQRMSSolution.Models;

namespace BBQRMSSolution.ViewModels
{
	public class OrderViewModel : ViewModelBase
	{
		private Timer t;

		private decimal st, tp, ta;
		public decimal subTotal { get { return st; } set { st = value; NotifyPropertyChanged("subTotal"); } }
		public decimal totalPrice { get { return tp; } set { tp = value; NotifyPropertyChanged("totalPrice"); } }
		public decimal taxAmount { get { return ta; } set { ta = value; NotifyPropertyChanged("taxAmount"); } }

		public OrderViewModel()
		{
			DateTime now = DateTime.Now;
			now = now.AddMilliseconds(-now.Millisecond);
			OrderSubmittedDate = now;
			t = new Timer(UpdateAge, null, 0, 1000 );

			totalPrice = 0.00m;
			subTotal = 0.00m;
			taxAmount = 0.00m;
		}

		private void UpdateAge(object state)
		{
			OrderAge = DateTime.Now - OrderSubmittedDate;
		}

		public DateTime OrderSubmittedDate { get; set; }

		private readonly ObservableCollection<OrderItem> mItems = new ObservableCollection<OrderItem>();
		public ObservableCollection<OrderItem> Items
		{
			get { return mItems; }
		}

		private int mOrderNumber;

		public int OrderNumber
		{
			get { return mOrderNumber; }
			set
			{
				if (value != mOrderNumber)
				{
					mOrderNumber = value;
					NotifyPropertyChanged("OrderNumber");
				}
			}
		}

		private TimeSpan mOrderAge;

		public TimeSpan OrderAge
		{
			get { return mOrderAge; }
			set
			{
				if (value != mOrderAge)
				{
					mOrderAge = value;
					NotifyPropertyChanged("OrderAge");
				}
			}
		}
	}
}
using System;
using System.Collections.ObjectModel;
using System.Threading;
using BBQRMSSolution.Models;

namespace BBQRMSSolution.ViewModels
{
	public class OrderViewModel : ViewModelBase
	{
		private Timer t;
		public OrderViewModel()
		{
			t = new Timer(UpdateAge, null, 0, 1000 );
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
using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.Models;

namespace BBQRMSSolution.ViewModels
{
	public class OrderViewModel : ViewModelBase
	{
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
using System;
using System.Collections.ObjectModel;
using System.Threading;
using BBQRMSSolution.Models;

namespace BBQRMSSolution.ViewModels
{
	public class OrderViewModel : ViewModelBase
	{
		private Timer _t;

		private decimal _st, _tp, _ta;
		public decimal SubTotal { get { return _st; } set { _st = value; NotifyPropertyChanged("subTotal"); } }
		public decimal TotalPrice { get { return _tp; } set { _tp = value; NotifyPropertyChanged("totalPrice"); } }
		public decimal TaxAmount { get { return _ta; } set { _ta = value; NotifyPropertyChanged("taxAmount"); } }

		public OrderViewModel()
		{
			DateTime now = DateTime.Now;
			now = now.AddMilliseconds(-now.Millisecond);
			OrderSubmittedDate = now;
			_t = new Timer(UpdateAge, null, 0, 1000 );

			TotalPrice = 0.00m;
			SubTotal = 0.00m;
			TaxAmount = 0.00m;
		}

		private void UpdateAge(object state)
		{
			OrderAge = DateTime.Now - OrderSubmittedDate;
		}

		public DateTime OrderSubmittedDate { get; set; }

		private readonly ObservableCollection<OrderItem> _mItems = new ObservableCollection<OrderItem>();
		public ObservableCollection<OrderItem> Items
		{
			get { return _mItems; }
		}

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
	}
}
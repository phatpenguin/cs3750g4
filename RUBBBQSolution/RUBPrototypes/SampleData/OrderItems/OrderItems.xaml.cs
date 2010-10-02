﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.
namespace Expression.Blend.SampleData.OrderItems
{
	using System; 

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
	internal class OrderItems { }
#else

	public class OrderItems : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		public OrderItems()
		{
			try
			{
				System.Uri resourceUri = new System.Uri("/RUBPrototypes;component/SampleData/OrderItems/OrderItems.xaml", System.UriKind.Relative);
				if (System.Windows.Application.GetResourceStream(resourceUri) != null)
				{
					System.Windows.Application.LoadComponent(this, resourceUri);
				}
			}
			catch (System.Exception)
			{
			}
		}

		private ItemCollection _Collection = new ItemCollection();

		public ItemCollection Collection
		{
			get
			{
				return this._Collection;
			}
		}

		private PendingOrders _PendingOrders = new PendingOrders();

		public PendingOrders PendingOrders
		{
			get
			{
				return this._PendingOrders;
			}
		}
	}

	public class ItemCollection : System.Collections.ObjectModel.ObservableCollection<Item>
	{ 
	}

	public class Item : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _ItemName = string.Empty;

		public string ItemName
		{
			get
			{
				return this._ItemName;
			}

			set
			{
				if (this._ItemName != value)
				{
					this._ItemName = value;
					this.OnPropertyChanged("ItemName");
				}
			}
		}

		private double _Price = 0;

		public double Price
		{
			get
			{
				return this._Price;
			}

			set
			{
				if (this._Price != value)
				{
					this._Price = value;
					this.OnPropertyChanged("Price");
				}
			}
		}
	}

	public class PendingOrdersItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _CountdownTime = string.Empty;

		public string CountdownTime
		{
			get
			{
				return this._CountdownTime;
			}

			set
			{
				if (this._CountdownTime != value)
				{
					this._CountdownTime = value;
					this.OnPropertyChanged("CountdownTime");
				}
			}
		}

		private Items _Items = new Items();

		public Items Items
		{
			get
			{
				return this._Items;
			}
		}
	}

	public class PendingOrders : System.Collections.ObjectModel.ObservableCollection<PendingOrdersItem>
	{ 
	}

	public class ItemsItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _ItemName = string.Empty;

		public string ItemName
		{
			get
			{
				return this._ItemName;
			}

			set
			{
				if (this._ItemName != value)
				{
					this._ItemName = value;
					this.OnPropertyChanged("ItemName");
				}
			}
		}

		private double _Price = 0;

		public double Price
		{
			get
			{
				return this._Price;
			}

			set
			{
				if (this._Price != value)
				{
					this._Price = value;
					this.OnPropertyChanged("Price");
				}
			}
		}

		private double _Qty = 0;

		public double Qty
		{
			get
			{
				return this._Qty;
			}

			set
			{
				if (this._Qty != value)
				{
					this._Qty = value;
					this.OnPropertyChanged("Qty");
				}
			}
		}
	}

	public class Items : System.Collections.ObjectModel.ObservableCollection<ItemsItem>
	{ 
	}
#endif
}
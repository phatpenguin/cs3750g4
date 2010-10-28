using System;

namespace RUBPrototypes.SampleData
{
	public class InventoryItemsItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private DateTime _Date;

		public DateTime Date
		{
			get
			{
				return this._Date;
			}

			set
			{
				if (this._Date != value)
				{
					this._Date = value;
					this.OnPropertyChanged("Date");
				}
			}
		}

		private string _InventoryItemName = string.Empty;

		public string InventoryItemName
		{
			get
			{
				return this._InventoryItemName;
			}

			set
			{
				if (this._InventoryItemName != value)
				{
					this._InventoryItemName = value;
					this.OnPropertyChanged("InventoryItemName");
				}
			}
		}

		private double _Quantity = 0;

		public double Quantity
		{
			get
			{
				return this._Quantity;
			}

			set
			{
				if (this._Quantity != value)
				{
					this._Quantity = value;
					this.OnPropertyChanged("Quantity");
				}
			}
		}
	}

	public class InventoryItems : System.Collections.ObjectModel.ObservableCollection<InventoryItemsItem>
	{
		public InventoryItems()
		{
			try
			{
				System.Uri resourceUri = new System.Uri("/RUBPrototypes;component/SampleData/InventoryItems.xaml", System.UriKind.Relative);
				if (System.Windows.Application.GetResourceStream(resourceUri) != null)
				{
					System.Windows.Application.LoadComponent(this, resourceUri);
				}
			}
			catch (System.Exception)
			{
			}
		}
	}
}
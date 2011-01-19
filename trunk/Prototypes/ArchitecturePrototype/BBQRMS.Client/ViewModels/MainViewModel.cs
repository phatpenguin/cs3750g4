using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Services.Client;
using BBQRMS.Client.Database;

namespace BBQRMS.Client.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private readonly DatabaseContainer mEntities;

		public MainViewModel()
		{
			// Insert code required on object creation below this point.
			mEntities = new DatabaseContainer(new Uri("http://localhost:11424/DataAccess.svc/"));

			LoadItems();
		}

		private void LoadItems()
		{
			mItems.Clear();
			foreach (var item in mEntities.MenuItems.Execute())
			{
				mItems.Add(item);
			}
		}

		public ObservableCollection<MenuItem> Items
		{
			get { return mItems; }
		}

		public void SaveData()
		{
			// Each of the objects that might have changed needs to be fed back into the data container so it can save those changes.
			foreach (var menuItem in Items)
			{
				mEntities.UpdateObject(menuItem);
			}
			mEntities.SaveChanges(SaveChangesOptions.Batch);
		}

		private string viewModelProperty = "Runtime Property Value";
		private readonly ObservableCollection<MenuItem> mItems = new ObservableCollection<MenuItem>();

		/// <summary>
		/// Sample ViewModel property; this property is used in the view to display its value using a Binding.
		/// </summary>
		/// <returns></returns>
		public string ViewModelProperty
		{ 
			get
			{
				return this.viewModelProperty;
			}
			set
			{
				this.viewModelProperty = value;
				this.NotifyPropertyChanged("ViewModelProperty");
			}
		}
		
		/// <summary>
		/// Sample ViewModel method; this method is invoked by a Behavior that is associated with it in the View.
		/// </summary>
		public void ViewModelMethod()
		{ 
			if(!this.ViewModelProperty.EndsWith("Updated Value", StringComparison.Ordinal)) 
			{ 
				this.ViewModelProperty = this.ViewModelProperty + " - Updated Value";
			}
		}
		


		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
		#endregion

	}
}
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class QuickInventoryViewModel : ViewModelBase
	{

        private ObservableCollection<InventoryLocationType> _locations;
        private InventoryLocationType _selectedLocation;

        
        
        [Obsolete("Used for design-time only", true)]
		public QuickInventoryViewModel()
		{

		}

		public QuickInventoryViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
		{
			DataService = dataService;
			MessageBus = messageBus;

            Locations = new ObservableCollection<InventoryLocationType>(DataService.InventoryLocationTypes.Expand("MasterInventories").Execute());
            
		}


	    public ObservableCollection<InventoryLocationType> Locations
	    {
	        get { return _locations; }
	        set { _locations = value; NotifyPropertyChanged("Locations");}
	    }

	    public InventoryLocationType SelectedLocation
	    {
	        get { return _selectedLocation; }
	        set { _selectedLocation = value; NotifyPropertyChanged("SelectedLocation");}
	    }


	    public DelegateCommand RemoveInventoryCommand { get; private set; }

		public void HandleRemovedInventoryCommand(object parameter)
		{
			var item = (MasterInventory) parameter;
			// Start animation that indicates the inventory is being updated
			IsWorking = true;
			DispatcherTimer timer = null;
			EventHandler callback =
				(sender, args) =>
					{
						IsWorking = false;
						timer.Stop();
						RemoveInventoryCommand.RaiseCanExecuteChanged();
					};
			timer = new DispatcherTimer(TimeSpan.FromSeconds(1.25d), DispatcherPriority.Normal, callback, Dispatcher.CurrentDispatcher);
			timer.Start();
			//item.Reset();
		}

		private bool CanRemoveInventory(object param)
		{
			return !IsWorking;
		}

		private bool _isWorking;

		public bool IsWorking
		{
			get { return _isWorking; }
			set
			{
				if (value != _isWorking)
				{
					_isWorking = value;
					NotifyPropertyChanged("IsWorking");
				}
			}
		}

	}


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
    public class ReconcileInventoryViewModel:ViewModelBase
    {
        private ObservableCollection<MasterInventory> _MasterInventory;
        private MasterInventory _SelectedMasterInventory;
        private Boolean isVisible = true;
        //private Item _selectedItem;


        [Obsolete("Used for design-time only", true)]
        public ReconcileInventoryViewModel()
    	{
    		//TODO: fill in the properties with some simulated data for the VS designer
    	}

        public ReconcileInventoryViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
        {
            DataService = dataService;
            MessageBus = messageBus;
            resetList();
        }
        private void resetList()
        {
            MasterInventories = new ObservableCollection<MasterInventory>(DataService.MasterInventories.Where(x => x.IsActive == true));
        }
        //property --kinda like a method but also an attribute
        public ObservableCollection<MasterInventory> MasterInventories
        {
            get
            {
                return _MasterInventory;
            }
            set
            {
                _MasterInventory = value; 
                NotifyPropertyChanged("MasterInventories");

            }

        }
        public MasterInventory SelectedMasterInventory
        {
            get
            {
                return _SelectedMasterInventory;
            }
            set
            {
                _SelectedMasterInventory = value;
                NotifyPropertyChanged("SelectedMasterInventory");
            }
        }
        public Boolean IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
                NotifyPropertyChanged("IsVisible");
            }
        }
        internal void SaveItem()
        {

            if (_SelectedMasterInventory.Id > 0)
            {
                DataService.UpdateObject(_SelectedMasterInventory);
          
            }
            else
            {
                DataService.AddToMasterInventories(_SelectedMasterInventory);
               
            }
            DataService.SaveChanges();
            resetList();
        }

        internal void DeleteItem()
        {
            // can't delete items from inventory...Must set inactive.
            // DataService.DeleteObject(_MasterInventory);
            // DataService.SaveChanges();
            if( _SelectedMasterInventory != null){
                _SelectedMasterInventory.IsActive = false;
                SaveItem();
            }
           
        }
    }
}

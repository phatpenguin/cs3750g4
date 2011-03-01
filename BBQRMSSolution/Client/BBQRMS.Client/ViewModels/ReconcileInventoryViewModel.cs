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
        private ObservableCollection<MasterInventory> _item;
        private MasterInventory _selectedItem;


        [Obsolete("Used for design-time only", true)]
        public ReconcileInventoryViewModel()
    	{
    		//TODO: fill in the properties with some simulated data for the VS designer
    	}

        public ReconcileInventoryViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
        {
                
        }
        //property --kinda like a method but also an attribute
        public ObservableCollection<MasterInventory> MasterInventory
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value; NotifyPropertyChanged("MasterInventory");
            }

        }

    }
}

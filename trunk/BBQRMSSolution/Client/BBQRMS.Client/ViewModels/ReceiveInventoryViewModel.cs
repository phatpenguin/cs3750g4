using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBQRMSSolution.ServerProxy;
using System.Collections.ObjectModel;

namespace BBQRMSSolution.ViewModels
{
    public class ReceiveInventoryViewModel : ViewModelBase
    {
        private ServerProxy.BBQRMSEntities DataService;
        private Controls.IMessageBus MessageBus;
        private ViewModelBase _content;
        private Boolean isVisible = true;


        private MasterInventory _MasterInventory;
        [Obsolete("Used for design-time only", true)]
  
         public ReceiveInventoryViewModel ()
	    {
                
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
        public ReceiveInventoryViewModel(ServerProxy.BBQRMSEntities DataService, Controls.IMessageBus MessageBus)
        {
            // TODO: Complete member initialization
            this.DataService = DataService;
            this.MessageBus = MessageBus;
            _MasterInventory = new MasterInventory();
            _MasterInventory.Name = "New Inventory";
            _MasterInventory.UnitQty = 0;
            _MasterInventory.ExpirationDate = DateTime.Now.Date;
            _MasterInventory.OrderLeadDays = 14;
            _MasterInventory.IsActive = true;
           
        }

        public MasterInventory Item
        {
            get
            {
                return _MasterInventory;
            }
            set
            {
                _MasterInventory = value;
                NotifyPropertyChanged("Item");
            }
        }



        internal void SaveInventory()
        {

            if (_MasterInventory.Id > 0)
            {
                DataService.UpdateObject(_MasterInventory);
                DataService.SaveChanges();
            }
            else
            {
                DataService.AddToMasterInventories(_MasterInventory);
                DataService.SaveChanges();
            }
            IsVisible = false;
    
        }
        internal void CancelAdd()
        {
            IsVisible = false;
        }

    }
}

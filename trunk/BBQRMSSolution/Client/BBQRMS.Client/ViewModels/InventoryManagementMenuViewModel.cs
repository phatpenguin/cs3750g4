using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controls;
using System.Collections.ObjectModel;
using BBQRMSSolution.ServerProxy;


namespace BBQRMSSolution.ViewModels
{   
    public class InventoryManagementMenuViewModel:ViewModelBase
    {
      
        private readonly IMessageBus _MessageBus;
        private BBQRMSEntities _DataService;
        private ViewModelBase _Content;
   

        public InventoryManagementMenuViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
        {
            _MessageBus = messageBus;
            _DataService = dataService;

        }

        
        public ViewModelBase Content{
            get { return _Content; }
            set
            {
                if (value != _Content)
                {
                    _Content = value;
                    NotifyPropertyChanged("Content");
                }
            }
        }


        public void HandleAddSupplier()
        {
            Content = new SupplierDetailViewModel(_DataService, _MessageBus);

        }
        public void HandleReceiveInventory()
        {


        }
        public void HandleReconcileInventory()
        {


        }
    }

   
}

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
      
        private readonly IMessageBus mMessageBus;
        private BBQRMSEntities mDataService;

        public InventoryManagementMenuViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
        {
            mMessageBus = messageBus;
            mDataService = dataService;

        }

        private ViewModelBase _Content;
        public ViewModelBase Content
        
        {
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
            //Content = new LoginView

        }
        public void HandleReceiveInventory()
        {


        }
        public void HandleReconcileInventory()
        {


        }
    }

   
}

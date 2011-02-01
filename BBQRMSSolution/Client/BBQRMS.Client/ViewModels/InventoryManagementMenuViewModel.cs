using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBQRMSSolution.ViewModels
{   
    public class InventoryManagementMenuViewModel:ViewModelBase
    {
        public InventoryManagementMenuViewModel()
        {

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBQRMSSolution.ServerProxy;
using BBQRMSSolution.ViewModels.Messages;
using Controls;

namespace BBQRMSSolution.ViewModels
{
    public class AdministrationViewModel : ViewModelBase {
        private ViewModelBase mContent;

        public AdministrationViewModel() {
        }

        public ViewModelBase Content
        {
            get { return mContent; }
            set
            {
                if (value != mContent)
                {
                    mContent = value;
                    NotifyPropertyChanged("Content");
                }
            }
        }

        public void HandleChangePIN() {
            GlobalApplicationState.MessageBus.Publish(new ShowScreen(new ChangePINViewModel()));
        }

        public void HandleManageEmployees()
        {
            GlobalApplicationState.MessageBus.Publish(new ShowScreen(new EmployeeManagementViewModel()));
        }

        public void HandleManageMenus()
        {
            GlobalApplicationState.MessageBus.Publish(new ShowScreen(new MenuManagementViewModel()));
        }
    }
}

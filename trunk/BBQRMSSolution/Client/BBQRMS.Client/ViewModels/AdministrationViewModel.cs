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
        private readonly BBQRMSEntities mDataService;
        private readonly IMessageBus mMessageBus;
        private ViewModelBase mContent;

        public AdministrationViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
   		{
   		    this.mMessageBus = messageBus;
   		    this.mDataService = dataService;
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
            mMessageBus.Publish(new ShowScreen(new ChangePINViewModel()));
        }

        public void HandleManageEmployees()
        {
            mMessageBus.Publish(new ShowScreen(new EmployeeManagementViewModel(mDataService, mMessageBus)));
        }
    }
}

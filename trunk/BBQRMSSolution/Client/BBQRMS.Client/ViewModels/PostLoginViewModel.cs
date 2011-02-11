using System;
using System.Data.Services.Client;
using System.Linq;
using BBQRMSSolution.ServerProxy;
using BBQRMSSolution.ViewModels.Messages;
using Controls;

namespace BBQRMSSolution.ViewModels
{
    public class PostLoginViewModel : ViewModelBase, IHandle<ShowScreen>
    {
        private readonly IMessageBus mMessageBus;
        private readonly BBQRMSEntities mDataService;
        private readonly SecurityContext mSecurityContext;

        private ViewModelBase mContent;
        private bool mClockOutVisible;

        [Obsolete("To be used only at design time")]
        public PostLoginViewModel()
        {
            var sc = new DesignTimeSecurityContext();
            sc.CurrentUser = new Employee { FirstName = "Fred", LastName = "Jones" };
            mSecurityContext = sc;
        }


        public PostLoginViewModel(BBQRMSEntities dataService, IMessageBus messageBus, SecurityContext securityContext)
        {
            mMessageBus = messageBus;
            mDataService = dataService;
            mSecurityContext = securityContext;
            messageBus.Subscribe(this);
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

        public SecurityContext SecurityContext
        {
            get
            {
                return mSecurityContext;
            }
        }


        public void HandleTakeOrders()
        {
            //TODO: Show a new or existing viewmodel for taking orders.
            mMessageBus.Publish(new ShowScreen(new CustomerOrderScreenViewModel(mDataService, mMessageBus)));
            //TODO:
            // Maybe each of these modules can be represented by an instance (mockable).
            // Then we each can just work with that instance to handle the button click by providing a method which might show an existing VM or create a new one.
            // Each module instance could hold onto the reusable viewmodels.
        }
        public void HandleCooksScreen()
        {
            //TODO: Show a new or existing viewmodel for the cooks screen.
            mMessageBus.Publish(new ShowScreen(new CooksScreenViewModel()));
        }

        public void HandleQuickInventoryScreen()
        {
            //TODO: Show a new or existing viewmodel for quick inventory.
            mMessageBus.Publish(new ShowScreen(new QuickInventoryViewModel()));
        }

        public void HandleReporting()
        {
            //TODO: Show a new or existing viewmodel for reporting.
            mMessageBus.Publish(new ShowScreen(new ChooseReportViewModel(mDataService, mMessageBus)));
        }

        public void HandleManageEmployees()
        {
            //TODO: Show a new or existing viewmodel for managing employees.
            //			mMessageBus.Publish(new ShowScreen(new EmployeeManagementViewModel()));
            mMessageBus.Publish(new ShowScreen(new EmployeeManagementViewModel(mDataService, mMessageBus)));
        }

        public void HandleManageInventory()
        {
            //TODO: Show a new or existing viewmodel for managing inventory.
            //This was changed just to get the solution to compile.
            mMessageBus.Publish(new ShowScreen(new InventoryManagementMenuViewModel(mDataService, mMessageBus)));
        }

        public void HandleManageMenus()
        {
            //TODO: Show a new or existing viewmodel for managing menus.
        }

        public void HandleAdminBtn()
        {
            mMessageBus.Publish(new ShowScreen(new AdministrationViewModel(mDataService, mMessageBus)));
        }

        public void HandleLogout()
        {
            mMessageBus.Publish(new UserLoggingOut());
        }


        void IHandle<ShowScreen>.Handle(ShowScreen message)
        {
            Content = message.ViewModelToShow;
        }

        public void HandleClockOut()
        {
            // show clock-out confirmation user interface
            ClockOutVisible = true;
        }

        public bool ClockOutVisible
        {
            get
            {
                return mClockOutVisible;
            }
            set
            {
                if (value != mClockOutVisible)
                {
                    mClockOutVisible = value;
                    mMessageBus.Publish(new ClockOutMode(mClockOutVisible));
                    NotifyPropertyChanged("ClockOutVisible");
                }
            }
        }

        public void ConfirmClockOut()
        {
            //TODO: actually clock out.
            var openClocks =
                mDataService.EmployeeTimeClocks.Where(
                    tc => tc.EmployeeId == mSecurityContext.CurrentUser.Id && tc.ClockOutTimeUTC == null).OrderByDescending(tc => tc.ClockInTimeUTC).ToList();
            if (openClocks.Count == 0)
            {
                //TODO: handle the exceptional case.. no open timeclock.
            }
            else
            {
                openClocks[0].ClockOutTimeUTC = TimeProvider.Current.UtcNow;
                mDataService.UpdateObject(openClocks[0]);
                mDataService.SaveChanges(SaveChangesOptions.Batch);
                HandleLogout();
                ClockOutVisible = false;
            }
        }

        public void CancelClockOut()
        {
            ClockOutVisible = false;
        }

        public class DesignTimeSecurityContext : SecurityContext
        {
            public new Employee CurrentUser { get; set; }
        }

        public void Handle(ShowScreen message)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.SampleData;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class PostLoginViewModel : ViewModelBase, IHandle<ShowScreen>
	{
		private readonly IMessageBus mMessageBus;
		private readonly BBQRMSEntities mDataService;
		private readonly SecurityContext mSecurityContext;

		private ViewModelBase mContent;

		private readonly ObservableCollection<OrderViewModel> mPendingOrders = SampleOrders.Sample;

		[Obsolete("To be used only at design time")]
		public PostLoginViewModel()
		{
			var sc = new DesignTimeSecurityContext();
			sc.CurrentUser = new Employee {firstName = "Fred", lastName = "Jones"};
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
			mMessageBus.Publish(new ShowScreen(new CustomerOrderScreenViewModel(mDataService, mPendingOrders, mMessageBus)));
		}

		public void HandleCashier()
		{
			//TODO: Show a new or existing viewmodel for cashiering.
			mMessageBus.Publish(new ShowScreen(new OrderCashierScreenViewModel(mPendingOrders[mPendingOrders.Count - 1], mMessageBus)));
		}

		public void HandleCooksScreen()
		{
			//TODO: Show a new or existing viewmodel for the cooks screen.
			mMessageBus.Publish(new ShowScreen(new CooksScreenViewModel(mPendingOrders)));
		}

		public void HandleQuickInventoryScreen()
		{
			//TODO: Show a new or existing viewmodel for quick inventory.
			mMessageBus.Publish(new ShowScreen(new QuickInventoryViewModel()));
		}

		public void HandleReporting()
		{
			//TODO: Show a new or existing viewmodel for reporting.
			mMessageBus.Publish(new ShowScreen(new ChooseReportViewModel(mMessageBus)));
		}

		public void HandleManageEmployees()
		{
			//TODO: Show a new or existing viewmodel for managing employees.
			mMessageBus.Publish(new ShowScreen(new EmployeeManagementViewModel()));
		}

		public void HandleManageInventory()
		{
			//TODO: Show a new or existing viewmodel for managing inventory.
			mMessageBus.Publish(new ShowScreen(new InventoryManagementViewModel()));
		}

		public void HandleManageMenus()
		{
			//TODO: Show a new or existing viewmodel for managing menus.
		}

		public void HandleLogout()
		{
			mMessageBus.Publish(new UserLoggingOut());
		}


		void IHandle<ShowScreen>.Handle(ShowScreen message)
		{
			Content = message.ViewModelToShow;
		}
	}

	public class DesignTimeSecurityContext : SecurityContext
	{
		public new Employee CurrentUser { get; set; }
	}

	public class UserLoggingOut
	{
	}
}
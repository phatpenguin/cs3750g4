using System;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Linq;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class PostLoginViewModel : ViewModelBase, IHandle<ShowScreen>, IHandle<UserLoggingOut>
	{
		private readonly IClientTimeProvider _timeProvider;
		private ViewModelBase _content;
		private bool _clockOutVisible;
		private readonly IPOSDeviceManager _posDeviceManager;

		[Obsolete("To be used only at design time", true)]
		public PostLoginViewModel()
		{
			var sc =
				new DesignTimeSecurityContext
					{
						CurrentUser = new Employee { FirstName = "Fred", LastName = "Jones" }
					};
			SecurityContext = sc;
		}


		public PostLoginViewModel(BBQRMSEntities dataService, IMessageBus messageBus, ISecurityContext securityContext, IClientTimeProvider timeProvider, IPOSDeviceManager posDeviceManager)
		{
			_timeProvider = timeProvider;
			MessageBus = messageBus;
			DataService = dataService;
			SecurityContext = securityContext;
			_posDeviceManager = posDeviceManager;
			
			messageBus.Subscribe(this);
		}

		public ViewModelBase Content
		{
			get { return _content; }
			set
			{
				if (value != _content)
				{
					_content = value;
					NotifyPropertyChanged("Content");
				}
			}
		}



		public void HandleTakeOrders()
		{
			// Show a new or existing viewmodel for taking orders.
			MessageBus.Publish(new ShowScreen(new CustomerOrderScreenViewModel(DataService, MessageBus, _posDeviceManager)));
			//TODO:
			// Maybe each of these modules can be represented by an instance (mockable).
			// Then we each can just work with that instance to handle the button click by providing a method which might show an existing VM or create a new one.
			// Each module instance could hold onto the reusable viewmodels.
		}

        public void HandleLoadOrder()
        {
            //TODO: Show a new or existing viewmodel for the load order screen.
            MessageBus.Publish(new ShowScreen(new LoadOrderScreenViewModel(DataService, MessageBus, _posDeviceManager)));
        }

		public void HandleCooksScreen()
		{
			//TODO: Show a new or existing viewmodel for the cooks screen.
			MessageBus.Publish(new ShowScreen(new CooksScreenViewModel(DataService, MessageBus)));
		}

		public void HandleQuickInventoryScreen()
		{
			//TODO: Show a new or existing viewmodel for quick inventory.
			MessageBus.Publish(new ShowScreen(new QuickInventoryViewModel(DataService, MessageBus, SecurityContext)));
		}

		public void HandleReporting()
		{
			//TODO: Show a new or existing viewmodel for reporting.
			MessageBus.Publish(new ShowScreen(new ChooseReportViewModel(DataService, MessageBus, _timeProvider)));
		}

		public void HandleAdminBtn()
		{
			MessageBus.Publish(new ShowScreen(new AdministrationViewModel(DataService, MessageBus, SecurityContext)));
		}

		public void HandleLogout()
		{
			MessageBus.Publish(new UserLoggingOut());
		}

		public void HandleClockOut()
		{
			// show clock-out confirmation user interface
			ClockOutVisible = true;
		}

		void IHandle<ShowScreen>.Handle(ShowScreen message)
		{
			if (Content != null)
				Content.Close();

			Content = message.ViewModelToShow;
			Content.Open();
		}

		public bool ClockOutVisible
		{
			get
			{
				return _clockOutVisible;
			}
			set
			{
				if (value != _clockOutVisible)
				{
					_clockOutVisible = value;
					MessageBus.Publish(new ClockOutMode(_clockOutVisible));
					NotifyPropertyChanged("ClockOutVisible");
				}
			}
		}

		public void ConfirmClockOut()
		{
			// actually clock out.
			var openClocks =
					DataService.EmployeeTimeClocks.Where(
							tc => tc.EmployeeId == SecurityContext.CurrentUser.Id && tc.ClockOutTimeUTC == null).OrderByDescending(tc => tc.ClockInTimeUTC).ToList();
			if (openClocks.Count == 0)
			{
				//TODO: handle the exceptional case.. no open timeclock.
			}
			else
			{
				openClocks[0].ClockOutTimeUTC = TimeProvider.Current.UtcNow;
				DataService.UpdateObject(openClocks[0]);
				DataService.SaveChanges(SaveChangesOptions.Batch);
				HandleLogout();
				ClockOutVisible = false;
			}
		}

		public void CancelClockOut()
		{
			ClockOutVisible = false;
		}

		void IHandle<UserLoggingOut>.Handle(UserLoggingOut message)
		{
			if (Content != null)
				Content.Close();
		}
	}


	public class DesignTimeSecurityContext : ISecurityContext
	{
		public Employee CurrentUser { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
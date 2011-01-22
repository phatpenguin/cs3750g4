using System;
using System.Linq;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		private readonly BBQRMSEntities mDataService;
		private readonly IMessageBus mEvents;

		public LoginViewModel(BBQRMSEntities dataService, IMessageBus events)
		{
			mDataService = dataService;
			mEvents = events;
		}

		public void HandleLogin(string pin)
		{
			// verify the format of the PIN
			var parts = pin.Split('0');
			if(parts.Length != 2)
			{
				mEvents.Publish(new InvalidPinEntered());
			}
			else
			{
				// check the validity of the PIN
				//TODO: maybe check a local cache of credentials like Windows does when we can't communicate with the server.
				IQueryable<ApplicationUser> results = mDataService.ApplicationUsers.Expand("Employee").Where(au => au.idPart == parts[0] && au.personalPart == parts[1]);
				//TODO: the following should be done on a background thread (with a shortish timeout) while a 'busy' animation is displayed.
				var applicationUser = results.FirstOrDefault();
				if(applicationUser == null)
				{
					mEvents.Publish(new InvalidPinEntered());
				}
				else
				{
					// store the current user's identity and roles so the rest of the application can access them.
					mEvents.Publish(new UserLoggedIn(applicationUser.Employee));
				}
			}
		}

		public void Exit()
		{
			mEvents.Publish(new ShutdownRequested());
		}
	}

	public class ShutdownRequested
	{
		private bool mCancelled;
		public bool Cancelled
		{
			get { return mCancelled; }
			set
			{
				if(value)
					mCancelled = true;
			}
		}
	}

	public class UserLoggedIn
	{
		public UserLoggedIn(Employee applicationUser)
		{
			Employee = applicationUser;
		}

		public Employee Employee { get; private set; }
	}

	public class InvalidPinEntered
	{
	}
}
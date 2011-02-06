using System;
using System.Data.Services.Client;
using System.Linq;
using BBQRMSSolution.ServerProxy;
using BBQRMSSolution.ViewModels.Messages;
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
				IQueryable<ApplicationUser> results = mDataService.ApplicationUsers.Expand("Employee/Roles/Privileges").Where(au => au.IdPart == parts[0] && au.PersonalPart == parts[1]);
				//TODO: the following should be done on a background thread (with a shortish timeout) while a 'busy' animation is displayed.
				var applicationUser = results.FirstOrDefault();
				if(applicationUser == null)
				{
					mEvents.Publish(new InvalidPinEntered());
				}
				else
				{
					PrepareTimeClock(applicationUser.Employee);
					//Let everyone who is listening set up for the new user.
					mEvents.Publish(new UserLoggedIn(applicationUser.Employee));
				}
			}
		}

		public void Exit()
		{
			mEvents.Publish(new ShutdownRequested());
		}

		public void PrepareTimeClock(Employee employee)
		{
			var openclocks =
				mDataService.EmployeeTimeClocks.Where(tc => tc.EmployeeId == employee.Id && tc.ClockOutTimeUTC == null).ToList();
			if (openclocks.Count == 0)
			{
				mDataService.AddToEmployeeTimeClocks(new EmployeeTimeClock {EmployeeId = employee.Id});
				//TODO: what if this fails?
				var resp = mDataService.SaveChanges(SaveChangesOptions.Batch);
			}
		}
	}
}
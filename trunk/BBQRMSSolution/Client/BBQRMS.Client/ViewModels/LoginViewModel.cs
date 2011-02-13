using System;
using System.Data.Services.Client;
using System.Linq;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		[Obsolete("Used for design-time only", true)]
		public LoginViewModel()
		{
			
		}
		public LoginViewModel(BBQRMSEntities dataService, IMessageBus events)
		{
			DataService = dataService;
			MessageBus = events;
		}

		public void HandleLogin(string pin)
		{
			// verify the format of the PIN
			//TODO: the following should be done on a background thread (with a shortish timeout) while a 'busy' animation is displayed.
			var applicationUser = CheckPIN(DataService, pin);
			if(applicationUser == null)
			{
				MessageBus.Publish(new InvalidPinEntered("Invalid Pin Entered"));
			}
			else
			{
				PrepareTimeClock(applicationUser.Employee);
				//Let everyone who is listening set up for the new user.
				MessageBus.Publish(new UserLoggedIn(applicationUser.Employee));
			}
		}

		public static ApplicationUser CheckPIN(BBQRMSEntities dataService, string pin)
		{
			var parts = pin.Split(new []{'0'}, 2);
			if (parts.Length != 2)
			{
				return null;
			}
			else
			{
				// check the validity of the PIN
				//TODO: maybe check a local cache of credentials like Windows does when we can't communicate with the server.
				IQueryable<ApplicationUser> results = dataService.ApplicationUsers.Expand("Employee/Roles/Privileges").Where(au => au.IdPart == parts[0] && au.PersonalPart == parts[1]);
				var applicationUser = results.FirstOrDefault();
				return applicationUser;
			}
		}

		public void Exit()
		{
			MessageBus.Publish(new ShutdownRequested());
		}

		public void PrepareTimeClock(Employee employee)
		{
			var openclocks =
				DataService.EmployeeTimeClocks.Where(tc => tc.EmployeeId == employee.Id && tc.ClockOutTimeUTC == null).ToList();
			if (openclocks.Count == 0)
			{
				DataService.AddToEmployeeTimeClocks(new EmployeeTimeClock { EmployeeId = employee.Id });
				//TODO: what if this fails?
				var resp = DataService.SaveChanges(SaveChangesOptions.Batch);
			}
		}
	}
}
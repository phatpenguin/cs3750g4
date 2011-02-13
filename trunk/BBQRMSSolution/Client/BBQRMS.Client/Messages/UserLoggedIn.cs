using BBQRMSSolution.ServerProxy;

namespace BBQRMSSolution.Messages
{
	public class UserLoggedIn
	{
		public UserLoggedIn(Employee applicationUser)
		{
			Employee = applicationUser;
		}

		public Employee Employee { get; private set; }
	}
}
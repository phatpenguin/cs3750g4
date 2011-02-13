using System.ComponentModel;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.BusinessLogic
{
	public class SecurityContext : IHandle<UserLoggedIn>, IHandle<UserLoggingOut>, ISecurityContext
	{
		private Employee mCurrentUser;
		public Employee CurrentUser
		{
			get { return mCurrentUser; }
			private set
			{
				if (value != mCurrentUser)
				{
					mCurrentUser = value;
					var propChange = PropertyChanged;
					if (propChange != null)
						propChange(this, new PropertyChangedEventArgs("CurrentUser"));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		void IHandle<UserLoggedIn>.Handle(UserLoggedIn message)
		{
			CurrentUser = message.Employee;
		}

		void IHandle<UserLoggingOut>.Handle(UserLoggingOut message)
		{
			CurrentUser = null;
		}
	}

	public interface ISecurityContext : INotifyPropertyChanged
	{
		Employee CurrentUser { get; }
	}
}
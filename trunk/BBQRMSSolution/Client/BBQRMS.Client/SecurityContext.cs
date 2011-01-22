using System;
using System.ComponentModel;
using BBQRMSSolution.ServerProxy;
using BBQRMSSolution.ViewModels;
using Controls;

namespace BBQRMSSolution
{
	public class SecurityContext : INotifyPropertyChanged, IHandle<UserLoggedIn>, IHandle<UserLoggingOut>
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

		public void Handle(UserLoggingOut message)
		{
			CurrentUser = null;
		}
	}
}
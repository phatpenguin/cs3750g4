using System;
using BBQRMSSolution.Properties;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class MainWindowViewModel : ViewModelBase, IHandle<UserLoggedIn>, IHandle<UserLoggingOut>, IHandle<ShutdownRequested>
	{
		private readonly IMessageBus mMessageBus;
		private readonly SecurityContext mSecurityContext;

		/// <summary>
		/// This is called because an instance is instantiated in App.xaml's Resources.
		/// MainWindow.xaml then uses it as its DataContext.
		/// </summary>
		public MainWindowViewModel()
			: this(new Uri(Settings.Default.dataServiceBaseUri), GlobalApplicationState.MessageBus, GlobalApplicationState.SecurityContext)
		{
		}

		public MainWindowViewModel(Uri serverAddress, IMessageBus messageBus, SecurityContext securityContext)
		{
			mMessageBus = messageBus;
			mSecurityContext = securityContext;
			mDataService = new BBQRMSEntities(serverAddress);
			messageBus.Subscribe(this);
			ShowLoginScreen();
		}

		private readonly BBQRMSEntities mDataService;

		public void ShowLoginScreen()
		{
			FullScreenContent = new LoginViewModel(mDataService, mMessageBus);
		}

		private void ShowPostLoginScreen()
		{
			FullScreenContent = new PostLoginViewModel(mDataService, mMessageBus, mSecurityContext);
		}

		private ViewModelBase mFullScreenContent;

		public ViewModelBase FullScreenContent
		{
			get { return mFullScreenContent; }
			private set
			{
				if (value != mFullScreenContent)
				{
					mFullScreenContent = value;
					NotifyPropertyChanged("FullScreenContent");
				}
			}
		}


		public void Handle(UserLoggedIn message)
		{
			ShowPostLoginScreen();
		}

		public void Handle(UserLoggingOut message)
		{
			ShowLoginScreen();
		}

		public void Handle(ShutdownRequested message)
		{
			//TODO: ask all the loaded modules if they have unsaved information and prompt the user for confirmation or cancelling if they do.
			if(false)
				message.Cancelled = true;
			else
				mMessageBus.Publish(new Shutdown());
		}
	}

	public class Shutdown
	{
	}
}
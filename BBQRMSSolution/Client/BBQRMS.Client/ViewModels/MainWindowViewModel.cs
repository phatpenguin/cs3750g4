using System;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class MainWindowViewModel : ViewModelBase, IHandle<UserLoggedIn>, IHandle<UserLoggingOut>, IHandle<ShutdownRequested>
	{

		[Obsolete("Called only at design time.", true)]
		public MainWindowViewModel()
		{
			MessageBus = new MessageBus();
			SecurityContext = new DesignTimeSecurityContext();
			FullScreenContent = new LoginViewModel(DataService, MessageBus);
		}

		public MainWindowViewModel(Uri serverAddress, IMessageBus messageBus, ISecurityContext securityContext)
		{
			DataService = new BBQRMSEntities(serverAddress);
			MessageBus = messageBus;
			SecurityContext = securityContext;

			messageBus.Subscribe(this);
			ShowLoginScreen();
		}

		public void ShowLoginScreen()
		{
			FullScreenContent = new LoginViewModel(DataService, MessageBus);
		}

		private void ShowPostLoginScreen()
		{
			FullScreenContent = new PostLoginViewModel(DataService, MessageBus, SecurityContext);
		}

		private ViewModelBase _fullScreenContent;

		public ViewModelBase FullScreenContent
		{
			get { return _fullScreenContent; }
			private set
			{
				if (value != _fullScreenContent)
				{
					_fullScreenContent = value;
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
				MessageBus.Publish(new Shutdown());
		}
	}
}
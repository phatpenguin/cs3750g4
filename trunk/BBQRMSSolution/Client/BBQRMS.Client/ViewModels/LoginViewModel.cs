using System;

namespace BBQRMSSolution.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		private readonly MainWindowViewModel mMainWindowViewModel;

		public LoginViewModel(MainWindowViewModel mainWindowViewModel)
		{
			mMainWindowViewModel = mainWindowViewModel;
			LoginCommand = new DelegateCommand(HandleLoginCommand);
		}

		public DelegateCommand LoginCommand { get; private set; }

		private void HandleLoginCommand()
		{
			mMainWindowViewModel.ShowPostLoginScreen();
		}
	}
}
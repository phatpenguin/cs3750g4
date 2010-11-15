namespace BBQRMSSolution.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			ShowLoginScreen();
		}

		public void ShowLoginScreen()
		{
			FullScreenContent = new LoginViewModel(this);
		}

		public void ShowPostLoginScreen()
		{
			FullScreenContent = new PostLoginViewModel(this);
		}

		private ViewModelBase _mFullScreenContent;

		public ViewModelBase FullScreenContent
		{
			get { return _mFullScreenContent; }
			private set
			{
				if (value != _mFullScreenContent)
				{
					_mFullScreenContent = value;
					NotifyPropertyChanged("FullScreenContent");
				}
			}
		}
	}
}
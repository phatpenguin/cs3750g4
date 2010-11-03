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
	}
}
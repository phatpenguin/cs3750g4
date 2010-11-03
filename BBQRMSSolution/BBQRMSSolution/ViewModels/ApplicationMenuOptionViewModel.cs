using System;

namespace BBQRMSSolution.ViewModels
{
	public class ApplicationMenuOptionViewModel : ViewModelBase
	{
		private readonly INavigationService mNavigationService;

		public ApplicationMenuOptionViewModel(INavigationService navigationService)
		{
			mNavigationService = navigationService;
			GoToContentCommand = new DelegateCommand(HandleGoToContent);
		}

		public DelegateCommand GoToContentCommand { get; private set; }

		private void HandleGoToContent()
		{
			if(ViewModelFactory != null)
				mNavigationService.Content = ViewModelFactory.Invoke();
		}

		private string mName;

		public string Name
		{
			get { return mName; }
			set
			{
				if (value != mName)
				{
					mName = value;
					NotifyPropertyChanged("Name");
				}
			}
		}

		public Func<ViewModelBase> ViewModelFactory { get; set; }

		private string mImageSource;

		public string ImageSource
		{
			get { return mImageSource; }
			set
			{
				if (value != mImageSource)
				{
					mImageSource = value;
					NotifyPropertyChanged("ImageSource");
				}
			}
		}
	}
}
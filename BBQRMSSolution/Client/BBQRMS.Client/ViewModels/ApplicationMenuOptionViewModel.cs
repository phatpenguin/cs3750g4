using System;

namespace BBQRMSSolution.ViewModels
{
	public class ApplicationMenuOptionViewModel : ViewModelBase
	{
		private readonly INavigationService _mNavigationService;

		public ApplicationMenuOptionViewModel(INavigationService navigationService)
		{
			_mNavigationService = navigationService;
			GoToContentCommand = new DelegateCommand(HandleGoToContent);
		}

		public DelegateCommand GoToContentCommand { get; private set; }

		private void HandleGoToContent()
		{
			if(ViewModelFactory != null)
				_mNavigationService.Content = ViewModelFactory.Invoke();
		}

		private string _mName;

		public string Name
		{
			get { return _mName; }
			set
			{
				if (value != _mName)
				{
					_mName = value;
					NotifyPropertyChanged("Name");
				}
			}
		}

		public Func<ViewModelBase> ViewModelFactory { get; set; }

		private string _mImageSource;

		public string ImageSource
		{
			get { return _mImageSource; }
			set
			{
				if (value != _mImageSource)
				{
					_mImageSource = value;
					NotifyPropertyChanged("ImageSource");
				}
			}
		}
	}
}
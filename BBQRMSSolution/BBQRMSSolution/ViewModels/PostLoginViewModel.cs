using System;
using System.Collections.ObjectModel;

namespace BBQRMSSolution.ViewModels
{
	public class PostLoginViewModel : ViewModelBase, INavigationService
	{
		private readonly MainWindowViewModel mMainWindow;

		[Obsolete("To be used only at design time")]
		public PostLoginViewModel()
		{
			ApplicationMenuItems =
				new ObservableCollection<ApplicationMenuOptionViewModel>
					{
						new ApplicationMenuOptionViewModel(this)
							{
								Name = "Take Orders",
								ViewModelFactory = () => new CustomerOrderScreenViewModel(),
								ImageSource = "/Graphics/accessories-text-editor.png"
							},
						new ApplicationMenuOptionViewModel(this)
							{
								Name = "Cooks Screen",
								//ViewModelFactory = () => new ...
								ImageSource = "/Graphics/Anonymous_Chef.png"
							},
						new ApplicationMenuOptionViewModel(this)
							{
								Name = "Quick Inventory Screen",
								//ViewModelFactory = () => new ...
								ImageSource = "/Graphics/to_do_list_cheked_1.png"
							},
						new ApplicationMenuOptionViewModel(this)
							{
								Name = "Reporting",
								ViewModelFactory = () => new ChooseReportViewModel(this),
								ImageSource = "/Graphics/x-office-spreadsheet.png"
							},
						new ApplicationMenuOptionViewModel(this)
							{
								Name = "Manage Employees",
								ViewModelFactory = () => new EmployeeManagementViewModel(),
								ImageSource = "/Graphics/system-users.png"
							},
						new ApplicationMenuOptionViewModel(this)
							{
								Name = "Manage Inventory",
								ViewModelFactory = () => new InventoryManagementViewModel(),
								ImageSource = "/Graphics/file_manager.png"
							},
						new ApplicationMenuOptionViewModel(this)
							{
								Name = "Manage Menus",
								//ViewModelFactory = () => new ...
								ImageSource = "/Graphics/menu.png"
							}
					};

			LogOutCommand = new DelegateCommand(HandleLogout);
		}

#pragma warning disable 612,618
		public PostLoginViewModel(MainWindowViewModel mainWindow) : this()
#pragma warning restore 612,618
		{
			mMainWindow = mainWindow;
		}

		public DelegateCommand LogOutCommand { get; private set; }

		private void HandleLogout()
		{
			mMainWindow.ShowLoginScreen();
		}

		public ObservableCollection<ApplicationMenuOptionViewModel> ApplicationMenuItems { get; private set; }


		private object mContent;
		public object Content
		{
			get { return mContent; }
			set
			{
				if (value != mContent)
				{
					mContent = value;
					NotifyPropertyChanged("Content");
				}
			}
		}


		ViewModelBase INavigationService.Content
		{
			get { return Content as ViewModelBase; }
			set { Content = value; }
		}
	}
}
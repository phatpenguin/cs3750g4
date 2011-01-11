using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.SampleData;

namespace BBQRMSSolution.ViewModels
{
	public class PostLoginViewModel : ViewModelBase, INavigationService
	{
		private readonly MainWindowViewModel _mMainWindow;
		private readonly ObservableCollection<OrderViewModel> _mPendingOrders = SampleOrders.Sample;

		[Obsolete("To be used only at design time")]
		public PostLoginViewModel()
		{
					
			ApplicationMenuItems =
				new ObservableCollection<ApplicationMenuOptionViewModel>
					{
						new ApplicationMenuOptionViewModel(this)
							{
								Name = "Take Orders",
								ViewModelFactory = () => new CustomerOrderScreenViewModel(_mPendingOrders,this),
								ImageSource = "/Graphics/accessories-text-editor.png"
							},
							new ApplicationMenuOptionViewModel(this)
								{
									Name = "Cashier Order",
									ViewModelFactory = () => new OrderCashierScreenViewModel(_mPendingOrders[_mPendingOrders.Count - 1],this),
									ImageSource = "/Graphics/cash.png"
								},
						new ApplicationMenuOptionViewModel(this)
							{
								Name = "Cooks Screen",
								ViewModelFactory = () => new CooksScreenViewModel(_mPendingOrders),
								ImageSource = "/Graphics/Anonymous_Chef.png"
							},
						new ApplicationMenuOptionViewModel(this)
							{
								Name = "Quick Inventory Screen",
								ViewModelFactory = () => new QuickInventoryViewModel(),
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
			_mMainWindow = mainWindow;
		}

		public DelegateCommand LogOutCommand { get; private set; }

		private void HandleLogout()
		{
			_mMainWindow.ShowLoginScreen();
		}

		public ObservableCollection<ApplicationMenuOptionViewModel> ApplicationMenuItems { get; private set; }


		private object _mContent;
		public object Content
		{
			get { return _mContent; }
			set
			{
				if (value != _mContent)
				{
					_mContent = value;
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
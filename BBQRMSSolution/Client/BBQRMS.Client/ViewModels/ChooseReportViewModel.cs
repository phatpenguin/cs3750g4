using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class ChooseReportViewModel : ViewModelBase
	{
		public ChooseReportViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
		{
			mMessageBus = messageBus;

			Reports =
				new ObservableCollection<ReportViewModel>
					{
						new ReportViewModel(dataService)
							{
								ReportName = "Most Popular Items",
								Group = "Sales",
								HasChart = false,
								Parameters =
									{
										new ReportDateParameterViewModel {Prompt = "Start Date"},
										new ReportDateParameterViewModel {Prompt = "End Date"},
									}
							},
						new ReportViewModel(dataService)
							{
								ReportName = "Daily Sales graph",
								Group = "Sales",
								HasChart = true,
								Parameters =
									{
										new ReportDateParameterViewModel {Prompt = "Start Date"},
										new ReportDateParameterViewModel {Prompt = "End Date"},
									}
							},
						new ReportViewModel(dataService)
							{
								ReportName = "Weekly Sales graph",
								Group = "Sales",
								HasChart = true,
								Parameters =
									{
										new ReportDateParameterViewModel {Prompt = "Start Date"},
										new ReportDateParameterViewModel {Prompt = "End Date"},
									}
							},
						new ReportViewModel(dataService)
							{
								ReportName = "Sales by type pie chart",
								Group = "Sales",
								HasChart = true,
								Parameters =
									{
										new ReportDateParameterViewModel {Prompt = "Start Date"},
										new ReportDateParameterViewModel {Prompt = "End Date"},
									}
							},
						new ReportViewModel(dataService)
							{
								ReportName = "Monthly Sales by type bar chart",
								Group = "Sales",
								HasChart = true,
								Parameters =
									{
										new ReportDateParameterViewModel {Prompt = "Start Date"},
										new ReportDateParameterViewModel {Prompt = "End Date"},
									}
							},
						new ReportViewModel(dataService)
							{
								ReportName = "Daily Inventory Levels",
								Group = "Inventory",
								HasChart = true,
								Parameters =
									{
										new ReportDateParameterViewModel {Prompt = "Start Date", Value = new DateTime(2010, 10, 1)},
										new ReportDateParameterViewModel {Prompt = "End Date", Value = new DateTime(2010, 10, 31)},
										new ReportOptionParameterViewModel
											{Prompt = "Choose Item", Options = {"Ribs", "Chicken", "Brisket"}, SelectedOption = "Ribs"},
									}
							},
						new ReportViewModel(dataService)
							{
								ReportName = "Daily Inventory used",
								Group = "Inventory",
								HasChart = false,
								Parameters =
									{
										new ReportDateParameterViewModel {Prompt = "Start Date"},
										new ReportDateParameterViewModel {Prompt = "End Date"},
										new ReportOptionParameterViewModel
											{Prompt = "Choose Item", Options = {"Ribs", "Chicken", "Brisket"}, SelectedOption = "Ribs"},
									}
							},
						new ReportViewModel(dataService)
							{
								ReportName = "Weekly Cost of inventory",
								Group = "Inventory",
								HasChart = false,
								Parameters =
									{
										new ReportDateParameterViewModel {Prompt = "Start Date"},
										new ReportDateParameterViewModel {Prompt = "End Date"},
									}
							},
						new ReportViewModel(dataService)
							{
								ReportName = "Monthly cost of inventory",
								Group = "Inventory",
								HasChart = false,
								Parameters =
									{
										new ReportDateParameterViewModel {Prompt = "Start Date"},
										new ReportDateParameterViewModel {Prompt = "End Date"},
									}
							},
						new ReportViewModel(dataService)
							{
								ReportName = "Weekly Hours worked",
								Group = "Employees",
								HasChart = false,
								Parameters =
									{
										new ReportDateParameterViewModel {Prompt = "Start Date"},
										new ReportDateParameterViewModel {Prompt = "End Date"},
									}
							},
						new ReportViewModel(dataService)
							{
								ReportName = "Individual employee schedule",
								Group = "Employees",
								HasChart = false,
								Parameters =
									{
										new ReportDateParameterViewModel {Prompt = "Start Date"},
										new ReportOptionParameterViewModel
											{Prompt = "Selecte employee", Options = {"Jane Doe", "John Doe", "Bob", "Alice"}}
									}
							},
						new ReportViewModel(dataService)
							{
								ReportName = "Staff schedule",
								Group = "Employees",
								HasChart = false,
								Parameters =
									{
										new ReportDateParameterViewModel {Prompt = "Start Date"},
									}
							},
					};

			RunReportCommand = new DelegateCommand(HandleRunReport, CanRunReport);
		}

		public DelegateCommand RunReportCommand { get; private set; }

		public void HandleRunReport()
		{
			mMessageBus.Publish(new ShowScreen(SelectedReport));
		}

		private bool CanRunReport()
		{
			return SelectedReport != null;
		}

		private ReportViewModel mSelectedReport;

		public ReportViewModel SelectedReport
		{
			get { return mSelectedReport; }
			set
			{
				if (value != mSelectedReport)
				{
					mSelectedReport = value;
					NotifyPropertyChanged("SelectedReport");
				}
			}
		}

		private ObservableCollection<ReportViewModel> mReports;
		private readonly IMessageBus mMessageBus;

		public ObservableCollection<ReportViewModel> Reports
		{
			get { return mReports; }
			set
			{
				if (value != mReports)
				{
					mReports = value;
					NotifyPropertyChanged("Reports");
				}
			}
		}
	}

	public class ShowScreen
	{
		public ShowScreen(ViewModelBase viewModel)
		{
			ViewModelToShow = viewModel;
		}

		public ViewModelBase ViewModelToShow { get; private set; }
	}
}
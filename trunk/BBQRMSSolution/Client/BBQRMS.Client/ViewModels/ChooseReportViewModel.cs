using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ServerProxy;
using BBQRMSSolution.ViewModels.Reports;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class ChooseReportViewModel : ViewModelBase
	{
		[Obsolete("Used for design-time only", true)]
		public ChooseReportViewModel() : this(null, null)
		{
			
		}

		public ChooseReportViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
		{
			DataService = dataService;
			MessageBus = messageBus;

			Reports =
				new ObservableCollection<ReportViewModel>
					{
						new DailySalesReport(dataService),
						/*new ReportViewModel(dataService)
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
							},*/
					};

			RunReportCommand = new DelegateCommand(HandleRunReport, CanRunReport);
		}

		public DelegateCommand RunReportCommand { get; private set; }

		public void HandleRunReport()
		{
			MessageBus.Publish(new ShowScreen(SelectedReport));
		}

		private bool CanRunReport()
		{
			return SelectedReport != null;
		}

		private ReportViewModel _selectedReport;

		public ReportViewModel SelectedReport
		{
			get { return _selectedReport; }
			set
			{
				if (value != _selectedReport)
				{
					_selectedReport = value;
					NotifyPropertyChanged("SelectedReport");
				}
			}
		}

		private ObservableCollection<ReportViewModel> _reports;

		public ObservableCollection<ReportViewModel> Reports
		{
			get { return _reports; }
			set
			{
				if (value != _reports)
				{
					_reports = value;
					NotifyPropertyChanged("Reports");
				}
			}
		}
	}
}
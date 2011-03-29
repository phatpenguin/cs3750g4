using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ServerProxy;
using BBQRMSSolution.ViewModels.Reports;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class ChooseReportViewModel : ViewModelBase
	{
		private readonly IClientTimeProvider _timeProvider;

		[Obsolete("Used for design-time only", true)]
		public ChooseReportViewModel() : this(null, null, TimeProvider.Current)
		{
			
		}

		public ChooseReportViewModel(BBQRMSEntities dataService, IMessageBus messageBus, IClientTimeProvider timeProvider)
		{
			_timeProvider = timeProvider;
			DataService = dataService;
			MessageBus = messageBus;

			Reports =
				new ObservableCollection<ReportViewModel>
					{
						new DailySalesReport(dataService, _timeProvider),
						new DailyShoppingReport(dataService, _timeProvider),
					};

			RunReportCommand = new DelegateCommand(HandleRunReport, CanRunReport);
		}

		public DelegateCommand RunReportCommand { get; private set; }

		public void HandleRunReport()
		{
			SelectedReport.SetParameterDefaults();
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
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using BBQRMSSolution.ServerProxy;

namespace BBQRMSSolution.ViewModels
{
	public class ReportViewModel : ViewModelBase
	{
		private readonly BBQRMSEntities mDataService;

		[Obsolete("Used only by the designer")]
		protected ReportViewModel()
		{
			
		}

		public ReportViewModel(BBQRMSEntities dataService)
		{
			mDataService = dataService;
			mParameters = new ObservableCollection<ReportParameterViewModel>();
		}

		private readonly ObservableCollection<ReportParameterViewModel> mParameters;

		public ObservableCollection<ReportParameterViewModel> Parameters
		{
			get { return mParameters; }
		}

		private string mReportName;

		public string ReportName
		{
			get { return mReportName; }
			set
			{
				if (value != mReportName)
				{
					mReportName = value;
					NotifyPropertyChanged("ReportName");
				}
			}
		}

		private string mGroup;

		public string Group
		{
			get { return mGroup; }
			set
			{
				if (value != mGroup)
				{
					mGroup = value;
					NotifyPropertyChanged("Group");
				}
			}
		}

		private bool mHasChart;

		public bool HasChart
		{
			get {
				return mHasChart;
			}
			set {
				if (value != mHasChart)
				{
					mHasChart = value;
					NotifyPropertyChanged("HasChart");
				}
			}
		}

		private static Stream GetReportDefinitions()
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream("BBQRMSSolution.Reports.Export_Menus.rdlc");
		}

		private IEnumerable GetData()
		{
			//If the refresh button on report viewer is going to function, the IEnumerable needs to support more than one enumeration.
			return mDataService.Menus.Execute().ToList();
		}

		public void RunReport(IReportViewer reportViewer)
		{
			//TODO:
			// 1) verify input parameters
			// 2) load report definition (what if it's already loaded and the user just wants to change parameter values?)
			reportViewer.LoadReportDefinition(GetReportDefinitions());
			// 3) retrieve report data
			IEnumerable data = GetData();
			// 4) assign the data to the report's data source(s)
			IList<string> names = reportViewer.GetDataSourceNames();
			reportViewer.AddDataSource(names[0], data);
			// 5) _run_ the report.
			reportViewer.RefreshReport();
		}
	}

	public abstract class ReportParameterViewModel : ViewModelBase
	{
		private string mPrompt;

		public string Prompt
		{
			get { return mPrompt; }
			set
			{
				if (value != mPrompt)
				{
					mPrompt = value;
					NotifyPropertyChanged("Prompt");
				}
			}
		}
	}

	public class ReportDateParameterViewModel : ReportParameterViewModel
	{
		private DateTime mValue;

		public DateTime Value
		{
			get { return mValue; }
			set
			{
				if (value != mValue)
				{
					mValue = value;
					NotifyPropertyChanged("Value");
				}
			}
		}
	}

	public class ReportBoolParameterViewModel : ReportParameterViewModel
	{
		private bool mValue;

		public bool Value
		{
			get { return mValue; }
			set
			{
				if (value != mValue)
				{
					mValue = value;
					NotifyPropertyChanged("Value");
				}
			}
		}
	}

	public class ReportOptionParameterViewModel : ReportParameterViewModel
	{
		public ReportOptionParameterViewModel()
		{
			Options = new ObservableCollection<string>();
		}

		private string mSelectedOption;

		public string SelectedOption
		{
			get { return mSelectedOption; }
			set
			{
				if (value != mSelectedOption)
				{
					mSelectedOption = value;
					NotifyPropertyChanged("SelectedOption");
				}
			}
		}

		public ObservableCollection<string> Options { get; private set; }
	}

	public class DesignTimeReportViewModel : ReportViewModel
	{
#pragma warning disable 612,618
		public DesignTimeReportViewModel()
#pragma warning restore 612,618
		{
			Parameters.Add(new ReportDateParameterViewModel {Prompt = "Start Date", Value = DateTime.Now.Date.AddMonths(-1)});
			Parameters.Add(new ReportDateParameterViewModel {Prompt = "End Date", Value = DateTime.Now.Date});
			Parameters.Add(new ReportOptionParameterViewModel {Prompt = "Choose", Options = {"First", "Second"}});
			Parameters.Add(new ReportBoolParameterViewModel {Prompt = "Choose", Value = true});
			ReportName = "Sample Report";
			Group = "Sample Group";
			HasChart = true;
		}
	}
}
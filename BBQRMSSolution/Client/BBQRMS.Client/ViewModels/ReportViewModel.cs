using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.ServerProxy;

namespace BBQRMSSolution.ViewModels
{
	public abstract class ReportViewModel : ViewModelBase
	{
		protected readonly IClientTimeProvider TheClock;

		[Obsolete("Used only by the designer")]
		protected ReportViewModel()
		{
			_parameters = new ObservableCollection<ReportParameterViewModel>();
		}

		protected ReportViewModel(BBQRMSEntities dataService, IClientTimeProvider theClock)
		{
			TheClock = theClock;
			DataService = dataService;
			_parameters = new ObservableCollection<ReportParameterViewModel>();
		}

		private readonly ObservableCollection<ReportParameterViewModel> _parameters;

		public ObservableCollection<ReportParameterViewModel> Parameters
		{
			get { return _parameters; }
		}

		private string _reportName;

		public string ReportName
		{
			get { return _reportName; }
			set
			{
				if (value != _reportName)
				{
					_reportName = value;
					NotifyPropertyChanged("ReportName");
				}
			}
		}

		private string _group;

		public string Group
		{
			get { return _group; }
			set
			{
				if (value != _group)
				{
					_group = value;
					NotifyPropertyChanged("Group");
				}
			}
		}

		private bool _hasChart;

		public bool HasChart
		{
			get {
				return _hasChart;
			}
			set {
				if (value != _hasChart)
				{
					_hasChart = value;
					NotifyPropertyChanged("HasChart");
				}
			}
		}

		protected abstract Stream GetReportDefinition();
/*
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream("BBQRMSSolution.Reports.Export_Menus.rdlc");
		}
*/

		protected abstract IDictionary<string, IEnumerable> GetDataSets();
/*
		{
			//If the refresh button on report viewer is going to function, the IEnumerable needs to support more than one enumeration.
			return DataService.Menus.Execute().ToList();
		}
*/

		public void RunReport(IReportViewer reportViewer)
		{
			//TODO:
			// 1) verify input parameters
			// 2) load report definition (what if it's already loaded and the user just wants to change parameter values?)
			reportViewer.LoadReportDefinition(GetReportDefinition());
			// 3) assign the parameters to the report
			foreach (ReportParameterViewModel reportParameterViewModel in Parameters)
			{
				if (!string.IsNullOrEmpty(reportParameterViewModel.Name))
					reportViewer.AddParameter(reportParameterViewModel.Name, reportParameterViewModel.StringValue);
			}
			// 4) retrieve report data
			var data = GetDataSets();
			// 5) assign the data to the report's data source(s)
			foreach (KeyValuePair<string, IEnumerable> keyAndDataPair in data)
			{
				reportViewer.AddDataSource(keyAndDataPair.Key, keyAndDataPair.Value);
			}
			// 6) _run_ the report.
			reportViewer.RefreshReport();
		}

		public virtual void SetParameterDefaults()
		{
			
		}
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

		protected override Stream GetReportDefinition()
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream("BBQRMSSolution.Reports.Export_Menus.rdlc");
		}

		protected override IDictionary<string, IEnumerable> GetDataSets()
		{
			return new Dictionary<string, IEnumerable>();
		}
	}
}
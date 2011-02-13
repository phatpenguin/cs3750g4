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
		[Obsolete("Used only by the designer")]
		protected ReportViewModel()
		{
		}

		public ReportViewModel(BBQRMSEntities dataService)
		{
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

		private static Stream GetReportDefinition()
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream("BBQRMSSolution.Reports.Export_Menus.rdlc");
		}

		private IEnumerable GetData()
		{
			//If the refresh button on report viewer is going to function, the IEnumerable needs to support more than one enumeration.
			return DataService.Menus.Execute().ToList();
		}

		public void RunReport(IReportViewer reportViewer)
		{
			//TODO:
			// 1) verify input parameters
			// 2) load report definition (what if it's already loaded and the user just wants to change parameter values?)
			reportViewer.LoadReportDefinition(GetReportDefinition());
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
		private string _prompt;

		public string Prompt
		{
			get { return _prompt; }
			set
			{
				if (value != _prompt)
				{
					_prompt = value;
					NotifyPropertyChanged("Prompt");
				}
			}
		}
	}

	public class ReportDateParameterViewModel : ReportParameterViewModel
	{
		private DateTime _value;

		public DateTime Value
		{
			get { return _value; }
			set
			{
				if (value != _value)
				{
					_value = value;
					NotifyPropertyChanged("Value");
				}
			}
		}
	}

	public class ReportBoolParameterViewModel : ReportParameterViewModel
	{
		private bool _value;

		public bool Value
		{
			get { return _value; }
			set
			{
				if (value != _value)
				{
					_value = value;
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

		private string _selectedOption;

		public string SelectedOption
		{
			get { return _selectedOption; }
			set
			{
				if (value != _selectedOption)
				{
					_selectedOption = value;
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
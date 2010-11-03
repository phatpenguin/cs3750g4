using System;
using System.Collections.ObjectModel;

namespace BBQRMSSolution.ViewModels
{
	public class ReportViewModel : ViewModelBase
	{
		public ReportViewModel()
		{
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
}
using System;
using System.Collections.ObjectModel;

namespace BBQRMSSolution.ViewModels
{
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


}
using System;
using System.Windows.Controls;
using System.Windows.Input;
using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution.Views
{
	public partial class ChooseReport : UserControl
	{
		public ChooseReport()
		{
			// Required to initialize variables
			InitializeComponent();
		}

		private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
		{
			ViewModel.RunReportCommand.Execute(null);
		}

		private ChooseReportViewModel ViewModel { get { return (ChooseReportViewModel) DataContext; } }

	}
}
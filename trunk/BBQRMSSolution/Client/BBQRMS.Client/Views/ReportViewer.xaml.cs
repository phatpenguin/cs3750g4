using System.Windows;
using System.Windows.Controls;
using BBQRMSSolution.ViewModels;
using BBQRMSSolution.ViewModels.Messages;
using Controls;

namespace BBQRMSSolution.Views
{
	public partial class ReportViewer : UserControl, IHandle<ClockOutMode>
	{
		public ReportViewer()
		{
			InitializeComponent();
			GlobalApplicationState.MessageBus.Subscribe(this);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.RunReport(new ReportViewerWrapper(reportViewer));
		}

		private ReportViewModel ViewModel
		{
			get { return (ReportViewModel) DataContext; }
		}

		public void Handle(ClockOutMode message)
		{
			winFormsHost.Visibility = message.ClockOutVisible ? Visibility.Hidden : Visibility.Visible;
		}
	}
}

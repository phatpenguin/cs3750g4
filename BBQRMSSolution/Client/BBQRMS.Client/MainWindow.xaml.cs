using System.ComponentModel;
using System.Windows;
using BBQRMSSolution.ViewModels.Messages;

namespace BBQRMSSolution
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			// Insert code required on object creation below this point.
			Closing += MainWindow_Closing;
		}

		private void MainWindow_Closing(object sender, CancelEventArgs e)
		{
			//If this was the result of the user requesting the close, we'll publish the message and see if anyone cancelled it.
			if (!GlobalApplicationState.ShuttingDown)
			{
				ShutdownRequested shutdownRequested = new ShutdownRequested();
				GlobalApplicationState.MessageBus.Publish(shutdownRequested);
				if (shutdownRequested.Cancelled)
					e.Cancel = true;
			}
		}
	}
}
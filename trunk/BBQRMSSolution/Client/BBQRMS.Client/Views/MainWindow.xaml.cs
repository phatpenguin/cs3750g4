using System.ComponentModel;
using System.Windows;
using BBQRMSSolution.Messages;

namespace BBQRMSSolution.Views
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

			Loaded += MainWindow_Loaded;
			Closing += MainWindow_Closing;
		}

		void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			DataContext = ((App)Application.Current).GetMainWindowViewModel();
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
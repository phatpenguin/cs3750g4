using System;
using System.Windows;
using System.Windows.Controls;

namespace BBQRMSSolution.Views.ViewParts
{
	/// <summary>
	/// Interaction logic for ClockOutView.xaml
	/// </summary>
	public partial class ClockOut : UserControl
	{
		public ClockOut()
		{
			InitializeComponent();
		}

		private void NoButton_Click(object sender, RoutedEventArgs e)
		{
			InvokeNo();
		}

		private void YesButton_Click(object sender, RoutedEventArgs e)
		{
			InvokeYes();
		}

		public event EventHandler No;

		public void InvokeNo()
		{
			EventHandler handler = No;
			if (handler != null) handler(this, EventArgs.Empty);
		}

		public event EventHandler Yes;

		public void InvokeYes()
		{
			EventHandler handler = Yes;
			if (handler != null) handler(this, EventArgs.Empty);
		}
	}
}

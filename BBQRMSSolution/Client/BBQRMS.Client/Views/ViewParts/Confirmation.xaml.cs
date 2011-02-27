using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BBQRMSSolution.Views.ViewParts
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Confirmation : UserControl
    {
		public Confirmation()
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

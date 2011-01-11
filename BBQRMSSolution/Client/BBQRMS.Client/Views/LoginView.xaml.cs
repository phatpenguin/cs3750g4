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

namespace BBQRMSSolution.Views
{
	/// <summary>
	/// Interaction logic for LoginView.xaml
	/// </summary>
	public partial class LoginView : UserControl
	{
		public LoginView()
		{
			InitializeComponent();
			Loaded += new RoutedEventHandler(LoginView_Loaded);
		}

		void LoginView_Loaded(object sender, RoutedEventArgs e)
		{
			passwordBox.Focus();
		}

		private void button1_Click_1(object sender, RoutedEventArgs e)
		{
			App.Current.Shutdown();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Button pressed = (Button) sender;
			passwordBox.Password += pressed.CommandParameter;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			passwordBox.Password = "";
		}

	}
}

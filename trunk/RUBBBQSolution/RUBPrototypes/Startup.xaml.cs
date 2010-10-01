using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RUBPrototypes
{
	/// <summary>
	/// Interaction logic for Startup.xaml
	/// </summary>
	public partial class Startup : Window
	{
		public Startup()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			new MainWindow().Show();
		}

		private void Cooks_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			new CooksScreen().Show();
		}

		private void Login_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			new Login().Show();
		}

		private void Inventory_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			new Inventory().Show();
		}
	}
}
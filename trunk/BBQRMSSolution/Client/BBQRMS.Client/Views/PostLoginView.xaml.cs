using System;
using System.Windows;
using System.Windows.Controls;
using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution.Views
{
	/// <summary>
	/// Interaction logic for PostLoginView.xaml
	/// </summary>
	public partial class PostLoginView : UserControl
	{
		public PostLoginView()
		{
			InitializeComponent();
		}

		PostLoginViewModel ViewModel { get { return (PostLoginViewModel) DataContext; }}

		private void takeOrders_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ViewModel.HandleTakeOrders();
		}

		private void cashier_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ViewModel.HandleCashier();
		}

		private void cook_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ViewModel.HandleCooksScreen();
		}

		private void quickInventory_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ViewModel.HandleQuickInventoryScreen();
		}

		private void reporting_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ViewModel.HandleReporting();
		}

		private void manageEmployees_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ViewModel.HandleManageEmployees();
		}

		private void manageInventory_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ViewModel.HandleManageInventory();
		}

		private void manageMenus_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ViewModel.HandleManageMenus();
		}

		private void logout_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ViewModel.HandleLogout();
		}

		private void changePin_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.HandleChangePIN();
		}
	}
}

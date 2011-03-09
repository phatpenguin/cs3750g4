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
using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution.Views
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
	public partial class Admin : UserControlBase<AdministrationViewModel>
	{

		public Admin()
		{
			InitializeComponent();
		}

		private void manageEmployees_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.HandleManageEmployees();
		}

		private void changePin_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.HandleChangePIN();
		}

		private void manageMenus_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.HandleManageMenus();
		}

		private void manageInventory_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.HandleManageInventory();
		}

        private void manageRoles_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.HandleManageRoles();
        }

        private void manageMenuItems_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.HandleManageMenuItems();
        }
	}
}

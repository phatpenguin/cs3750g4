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
    /// Interaction logic for MenuItemManagementView.xaml
    /// </summary>
    public partial class MenuItemManagementView : UserControlBase<MenuItemManagementViewModel>
    {
        public MenuItemManagementView()
        {
            InitializeComponent();
        }
        private void SaveCommand(object sender, RoutedEventArgs e)
        {

            ViewModel.HandleSaveClick();
        }

        private void AddCommand(object sender, RoutedEventArgs e)
        {
            ViewModel.HandleCreateItem();
        }

        private void DeleteCommand(object sender, RoutedEventArgs e)
        {
            ViewModel.HandleDeleteMenuItem();
        }
    }
}

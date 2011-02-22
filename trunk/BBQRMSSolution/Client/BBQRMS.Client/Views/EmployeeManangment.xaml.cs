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
using BBQRMSSolution.ServerProxy;
using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution.Views
{
    /// <summary>
    /// Interaction logic for EmployeeManangment.xaml
    /// </summary>
    public partial class EmployeeManangment : UserControlBase<EmployeeManagementViewModel>
    {

        public EmployeeManangment()
        {
            InitializeComponent();
        }

        private void SaveCommand(object sender, RoutedEventArgs e)
        {
            
            ViewModel.HandleSaveClick();
        }

        private void AddEmployeeCommand(object sender, RoutedEventArgs e)
        {
            ViewModel.HandleCreateEmployee();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel != null) {
         //       ViewModel.HandlePayTypeSelectionChanged(payTypesCB.SelectedItem);
            }
        }

        private void DeleteCommand(object sender, RoutedEventArgs e)
        {
            ViewModel.HandleDeleteEmployee();
        }
    }
}

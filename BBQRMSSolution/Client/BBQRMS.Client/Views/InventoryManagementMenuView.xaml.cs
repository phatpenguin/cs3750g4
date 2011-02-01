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
    /// Interaction logic for InventoryManagementMenuView.xaml
    /// </summary>
    public partial class InventoryManagementMenuView : UserControl
    {
        InventoryManagementMenuViewModel ViewModel { get { return (InventoryManagementMenuViewModel)DataContext; } }
        public InventoryManagementMenuView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.HandleAddSupplier();
        }
   
    }
}

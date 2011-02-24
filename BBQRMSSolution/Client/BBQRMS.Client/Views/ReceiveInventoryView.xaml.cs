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
    /// Interaction logic for ReceiveInventory.xaml
    /// </summary>
    public partial class ReceiveInventoryView : UserControl
    {
        ReceiveInventoryViewModel ViewModel { get { return (ReceiveInventoryViewModel)DataContext; } }
        public ReceiveInventoryView()
        {
            InitializeComponent();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            ViewModel.CancelAdd();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveInventory();
        }
    }
}

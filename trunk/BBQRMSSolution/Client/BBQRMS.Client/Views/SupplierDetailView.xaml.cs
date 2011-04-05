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
    /// Interaction logic for SupplierDetailView.xaml
    /// </summary>
    public partial class SupplierDetailView : UserControl
    {
        SupplierDetailViewModel ViewModel { get { return (SupplierDetailViewModel)DataContext; } }
        public SupplierDetailView()
        {
            InitializeComponent();
        }

        private void SupplierNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            //capture value and insert into an objects attribute.
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveSupplier();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteItem();
        }

        private void Button_AddSupplier(object sender, RoutedEventArgs e)
        {
            ViewModel.AddSupplier();
        }

       
    }
}

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
    /// Interaction logic for InventoryManagement.xaml
    /// </summary>
    public partial class InventoryManagement : UserControl
    {
        public InventoryManagement()
        {
            InitializeComponent();
            
        }

       
        private void addNewItem_Click(object sender, RoutedEventArgs e)
        {
            inventoryItemDetailCP.Content = new InventoryItemDetail();
        }
    }
}

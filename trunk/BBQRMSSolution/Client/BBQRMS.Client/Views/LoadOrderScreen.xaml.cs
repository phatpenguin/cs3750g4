using System.Windows.Controls;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution.Views
{
    public partial class LoadOrderScreen : UserControlBase<LoadOrderScreenViewModel>
    {
        public LoadOrderScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            OrderListView.SelectedIndex = -1;
        }
    }
}

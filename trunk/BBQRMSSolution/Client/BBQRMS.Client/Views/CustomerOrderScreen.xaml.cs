using System.Windows;
using System.Windows.Controls;
using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution.Views
{
	public partial class CustomerOrderScreen : UserControlBase<CustomerOrderScreenViewModel>
	{
		public CustomerOrderScreen()
		{
			InitializeComponent();
		}

        private void Payment_Pad_Button_Click(object sender, RoutedEventArgs e)
        {
            var pressed = (Button)sender;

            if (pressed.CommandParameter.ToString() == "." && PaymentAmountTextBox.Text.Contains(".")) return;
            if (PaymentAmountTextBox.Text == "0") PaymentAmountTextBox.Text = pressed.CommandParameter.ToString();
            else PaymentAmountTextBox.Text += pressed.CommandParameter;
        }

        private void Discount_Pad_Button_Click(object sender, RoutedEventArgs e)
        {
            var pressed = (Button)sender;

            if (pressed.CommandParameter.ToString() == "." && DiscountAmountTextBox.Text.Contains(".")) return;
            if (DiscountAmountTextBox.Text == "0") DiscountAmountTextBox.Text = pressed.CommandParameter.ToString();
            else DiscountAmountTextBox.Text += pressed.CommandParameter;
        }
	}
} 

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

        private void Payment_Pad_BackSpace_Click(object sender, RoutedEventArgs e)
        {
            PaymentAmountTextBox.Text = PaymentAmountTextBox.Text.Length == 1 ? "0" : PaymentAmountTextBox.Text.Remove(PaymentAmountTextBox.Text.Length - 1);
        }

	    private void Discount_Pad_BackSpace_Click(object sender, RoutedEventArgs e)
        {
            DiscountAmountTextBox.Text = DiscountAmountTextBox.Text.Length == 1 ? "0" : DiscountAmountTextBox.Text.Remove(DiscountAmountTextBox.Text.Length - 1);
        }

        private void Payment_Pad_Clear_Click(object sender, RoutedEventArgs e)
        {
            PaymentAmountTextBox.Text = "0";
        }

        private void Discount_Pad_Clear_Click(object sender, RoutedEventArgs e)
        {
            DiscountAmountTextBox.Text = "0";
        }
	}
} 

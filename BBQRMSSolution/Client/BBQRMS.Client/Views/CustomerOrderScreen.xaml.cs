﻿using System;
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
	/// Interaction logic for CustomerOrderScreen.xaml
	/// </summary>
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

            PaymentAmountTextBox.Text += pressed.CommandParameter;
        }

        private void Discount_Pad_Button_Click(object sender, RoutedEventArgs e)
        {
            var pressed = (Button)sender;


            if (pressed.CommandParameter.ToString() == "." && DiscountAmountTextBox.Text.Contains(".")) return;

            DiscountAmountTextBox.Text += pressed.CommandParameter;
        }
	}
} 

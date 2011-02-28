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

namespace BBQRMSSolution.Views.ViewParts
{
    /// <summary>
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : UserControl
    {
        public OrderViewModel Order { get; set; }

        public Payment()
        {
            InitializeComponent();
        }
    }
}

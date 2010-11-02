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
    /// Interaction logic for EmployeeManangment.xaml
    /// </summary>
    public partial class EmployeeManangment : UserControl
    {
        public EmployeeManangment()
        {
            InitializeComponent();
            EmployeeList.SelectionChanged += new SelectionChangedEventHandler(EmployeeList_SelectionChanged);
            RoleList.SelectionChanged += new SelectionChangedEventHandler(RoleList_SelectionChanged);
        }

        private void RoleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employeeManagmentContent.Content = new RoleManagement();
        }

        private void EmployeeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employeeManagmentContent.Content = new EmployeeInfo();
        }
    }
}

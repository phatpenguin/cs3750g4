using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BBQRMSSolution.ServerProxy;

namespace BBQRMSSolution.Views
{
	/// <summary>
	/// Interaction logic for EmployeeInfo.xaml
	/// </summary>
	public partial class EmployeeInfo : UserControlBase<Employee>
	{
		public EmployeeInfo()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty AllRolesProperty =
			DependencyProperty.Register("AllRoles", typeof(IEnumerable<Role>), typeof(EmployeeInfo), new UIPropertyMetadata(null));

		public static readonly DependencyProperty PayTypesProperty =
			DependencyProperty.Register("PayTypes", typeof(IEnumerable<EmployeePayType>), typeof(EmployeeInfo), new UIPropertyMetadata(null));

		public static readonly DependencyProperty SaveCommandProperty =
				DependencyProperty.Register("SaveCommand", typeof(ICommand), typeof(EmployeeInfo), new UIPropertyMetadata(null));


		public IEnumerable<Role> AllRoles
		{
			get { return (IEnumerable<Role>)GetValue(AllRolesProperty); }
			set { SetValue(AllRolesProperty, value); }
		}

		public IEnumerable<EmployeePayType> PayTypes
		{
			get { return (IEnumerable<EmployeePayType>)GetValue(PayTypesProperty); }
			set { SetValue(PayTypesProperty, value); }
		}

		public ICommand SaveCommand
		{
			get { return (ICommand)GetValue(SaveCommandProperty); }
			set { SetValue(SaveCommandProperty, value); }
		}

	}
}

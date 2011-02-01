﻿using System;
using System.Linq;
using System.Reflection;
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
using RUBPrototypes.SampleData;

namespace BBQRMSSolution.Views
{
	/// <summary>
	/// Interaction logic for ReportViewer.xaml
	/// </summary>
	public partial class ReportViewer : UserControl
	{
		public ReportViewer()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.RunReport(new ReportViewerWrapper(reportViewer));
		}

		private ReportViewModel ViewModel
		{
			get { return (ReportViewModel) DataContext; }
		}
	}
}
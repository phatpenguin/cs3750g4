using System;
using System.Collections.Generic;
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
using Microsoft.Reporting.WinForms;
using RUBPrototypes.SampleData;

namespace BBQRMSSolution.Views
{
	/// <summary>
	/// Interaction logic for ReportViewer.xaml
	/// </summary>
	public partial class ReportViewer : UserControl
	{
		private readonly InventoryItems mItemsData;

		public ReportViewer()
		{
			InitializeComponent();
			mItemsData = new InventoryItems();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			reportViewer.LocalReport.LoadReportDefinition(Assembly.GetExecutingAssembly().GetManifestResourceStream("BBQRMSSolution.Reports.InventoryLevelsGraph.rdlc"));

			//var inventoryItemsObj = FindResource("InventoryItems");
			reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", mItemsData));
			reportViewer.RefreshReport();
		}
	}
}

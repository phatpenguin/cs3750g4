using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Expression.Blend.SampleData.OrderItems;
using Microsoft.Reporting.WinForms;
using RUBPrototypes.Properties;
using RUBPrototypes.SampleData;

namespace RUBPrototypes
{
	/// <summary>
	/// Interaction logic for Inventory.xaml
	/// </summary>
	public partial class Reporting2 : Window
	{
		public Reporting2()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.

			var inventoryItemsObj = FindResource("InventoryItems");
			var inventoryItems = inventoryItemsObj as InventoryItems;

			reportViewer.LocalReport.LoadReportDefinition(Assembly.GetExecutingAssembly().GetManifestResourceStream("RUBPrototypes.InventoryLevelsGraph.rdlc"));

			reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", inventoryItems));
			reportViewer.RefreshReport();
		}

	}
}
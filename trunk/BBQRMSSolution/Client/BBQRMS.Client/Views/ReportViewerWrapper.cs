﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using BBQRMSSolution.ViewModels;
using Microsoft.Reporting.WinForms;

namespace BBQRMSSolution.Views
{
	public class ReportViewerWrapper : IReportViewer
	{
		private readonly Microsoft.Reporting.WinForms.ReportViewer mRealViewer;

		public ReportViewerWrapper(Microsoft.Reporting.WinForms.ReportViewer realViewer)
		{
			mRealViewer = realViewer;
		}

		public void LoadReportDefinition(Stream rdlcDefinition)
		{
			mRealViewer.LocalReport.LoadReportDefinition(rdlcDefinition);
		}

		public IList<string> GetDataSourceNames()
		{
			return mRealViewer.LocalReport.GetDataSourceNames();
		}

		public void AddDataSource(string dataSourceName, IEnumerable data)
		{
			mRealViewer.LocalReport.DataSources.Add(new ReportDataSource(dataSourceName, data));
		}

		public void RefreshReport()
		{
			mRealViewer.RefreshReport();
		}
	}
}
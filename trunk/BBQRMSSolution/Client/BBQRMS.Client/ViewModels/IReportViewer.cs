using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BBQRMSSolution.ViewModels
{
	public interface IReportViewer
	{
		void LoadReportDefinition(Stream rdlcDefinition);
		IList<string> GetDataSourceNames();
		void AddDataSource(string dataSourceName, IEnumerable data);
		void RefreshReport();
		void AddParameter(string parameterName, string parameterValue);
	}
}
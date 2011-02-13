using System.Collections.Generic;
using BBQRMSSolution.ServerProxy;

namespace RUBPrototypes.SampleData
{
	public static class DesignTimePayTypes
	{
		private static IEnumerable<EmployeePayType> mAll;
		static DesignTimePayTypes()
		{
			mAll = new[] {new EmployeePayType {Descr = "Salary"}, new EmployeePayType {Descr = "Hourly"},};
		}
		public static IEnumerable<EmployeePayType> All
		{
			get { return mAll; }
		}
	}
}
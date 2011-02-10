using System;
using System.Data.Services;

namespace BBQRMS.WCFServices
{
	public class BBQRMSDataService : DataService<BBQRMSEntities>
	{
		public static void InitializeService(IDataServiceConfiguration config)
		{
			config.SetEntitySetAccessRule("*", EntitySetRights.All);
			config.UseVerboseErrors = true;
		}

		[ChangeInterceptor("EmployeeTimeClocks")]
		public void OnChangeEmployeeTimeClocks(EmployeeTimeClock tc, UpdateOperations operations)
		{
			if(operations == UpdateOperations.Add)
			{
				tc.ClockInTimeUTC = TimeProvider.Current.UtcNow;
			}
		}
	}
}
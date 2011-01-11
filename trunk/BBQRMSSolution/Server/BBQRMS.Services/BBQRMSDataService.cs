using System.Data.Services;

namespace BBQRMS.WCFServices
{
	public class BBQRMSDataService : DataService<BBQRMSEntities>
	{
		public static void InitializeService(IDataServiceConfiguration config)
		{
			config.SetEntitySetAccessRule("*", EntitySetRights.All);
		}
	}
}
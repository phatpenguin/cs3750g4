using System;
using System.Data.Services;

namespace BBQRMS.WCFServices
{
	public static class Host
	{
		private static DataServiceHost host;
		public static void Open()
		{
			Type serviceType = typeof (BBQRMSDataService);
			Uri baseAddress = new Uri("http://localhost:80/Temporary_Listen_Addresses/BBQRMS/");
			Uri[] baseAddresses = new [] {baseAddress};

			host = new DataServiceHost(serviceType, baseAddresses);
			host.Open();
		}

		public static void Close()
		{
			if (host != null)
				host.Close();
		}

	}
}
using System;
using System.Data.Services;

namespace BBQRMS.WCFServices
{
	public static class Host
	{
		private static DataServiceHost host;
		public static void Open(Uri address)
		{
			Type serviceType = typeof (BBQRMSDataService);
			Uri baseAddress = address;
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
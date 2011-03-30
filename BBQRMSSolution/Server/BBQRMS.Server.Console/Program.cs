using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBQRMS.WCFServices;

namespace BBQRMS.ServerConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Uri address = new Uri("http://0.0.0.0:80/Temporary_Listen_Addresses/BBQRMS/");
			Host.Open(address);
			Console.WriteLine("Service listening at {0}", address);
			Console.WriteLine("Press any key to terminate.");
			Console.ReadKey();
			Host.Close();
		}
	}
}

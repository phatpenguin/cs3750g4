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
			Host.Open();
			Console.WriteLine("Service listening.");
			Console.WriteLine("Press any key to terminate.");
			Console.ReadKey();
			Host.Close();
		}
	}
}

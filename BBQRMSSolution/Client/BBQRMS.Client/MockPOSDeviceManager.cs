using System;
using System.Drawing;
using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution
{
	public class MockPOSDeviceManager : IPOSDeviceManager
	{
		private readonly ICashDrawer _drawer = new MockCashDrawer();
		private readonly IReceiptPrinter _printer = new MockReceiptPrinter();

		public ICashDrawer GetCashDrawer()
		{
			return _drawer;
		}

		public IReceiptPrinter GetReceiptPrinter()
		{
			return _printer;
		}
	}

	public class MockReceiptPrinter : IReceiptPrinter
	{
		public void PrintLine(string line)
		{
			//TODO: implement
		}

		public void Cut()
		{
			//TODO: implement
		}

		public void Claim()
		{
			//TODO: implement
		}

		public void Enable()
		{
			//TODO: implement
		}

		public void Release()
		{
			//TODO: implement
		}

		public void FeedToCut()
		{
			//TODO: implement
		}

		public void PrintStoredLogo(int logoNumber)
		{
			//TODO: implement
		}
	}

	public class MockCashDrawer : ICashDrawer
	{
		public void OpenDrawer()
		{
			IsDrawerOpen = true;
		}

		public bool IsDrawerOpen { get; private set; }

		public event EventHandler Closed;

		public void Claim()
		{
			//TODO: implement
		}

		public void Enable()
		{
			//TODO: implement
		}

		public void Release()
		{
			//TODO: implement
		}
	}

}
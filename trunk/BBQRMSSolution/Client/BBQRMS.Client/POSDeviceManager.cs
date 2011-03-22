using System;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ViewModels;
using Controls;
using Microsoft.PointOfService;

namespace BBQRMSSolution
{
	public class POSDeviceManager : IPOSDeviceManager
	{
		private readonly IMessageBus _messageBus;
		private ICashDrawer _drawer;
		private IReceiptPrinter _printer;
		private readonly PosExplorer _exp;

		public POSDeviceManager(IMessageBus messageBus)
		{
			_messageBus = messageBus;
			//TODO: provide an ISynchronizeInvoke to the PosExplorer.
			_exp = new PosExplorer();
		}

		public ICashDrawer GetCashDrawer()
		{
			if(_drawer == null)
			{
				DeviceInfo drawerDeviceInfo = _exp.GetDevice("CashDrawer");
				CashDrawer drawer = (CashDrawer)_exp.CreateInstance(drawerDeviceInfo);
				_drawer = new DrawerWrapper(drawer, _messageBus);
				drawer.Open();
			}
			return _drawer;
		}

		public IReceiptPrinter GetReceiptPrinter()
		{
			if(_printer == null)
			{
				DeviceInfo printerDeviceInfo = _exp.GetDevice("PosPrinter");
				PosPrinter printer = (PosPrinter)_exp.CreateInstance(printerDeviceInfo);
				_printer = new ReceiptPrinterWrapper(printer);
				printer.Open();
			}
			return _printer;
		}
	}

	public class ReceiptPrinterWrapper : IReceiptPrinter
	{
		private readonly PosPrinter _printer;

		public ReceiptPrinterWrapper(PosPrinter printer)
		{
			_printer = printer;
		}

		public void PrintStoredLogo(int logoNumber)
		{
			try
			{
				_printer.PrintNormal(PrinterStation.Receipt, "\x1b|cA\x1b|" + logoNumber + "B");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public void PrintLine(string line)
		{
			try
			{
				_printer.PrintNormal(PrinterStation.Receipt, line + "\n");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public void Cut()
		{
			_printer.CutPaper(95);
		}

		public void Claim()
		{
			_printer.Claim(1000);
		}

		public void Enable()
		{
			_printer.DeviceEnabled = true;
		}

		public void Release()
		{
			if (_printer.Claimed)
			{
				if (_printer.DeviceEnabled)
				{
					_printer.DeviceEnabled = false;
				}
				_printer.Release();
			}
		}

		public void FeedToCut()
		{
			_printer.PrintNormal(PrinterStation.Receipt, "\x1B|7lF");
		}
	}

	public class DrawerWrapper : ICashDrawer
	{
		private readonly CashDrawer _drawer;
		private readonly IMessageBus _messageBus;

		public DrawerWrapper(CashDrawer drawer, IMessageBus messageBus)
		{
			_drawer = drawer;
			_messageBus = messageBus;
			_drawer.StatusUpdateEvent += HandleStatusUpdateEvent;
		}

		void HandleStatusUpdateEvent(object sender, StatusUpdateEventArgs e)
		{
			//if ISynchronizeInvoke was not supplied, then we may have to marshal this back to the UI thread before publishing.
			if (e.Status == 0)
				_messageBus.Publish(new CashDrawerClosed());
			if(e.Status == 1)
				_messageBus.Publish(new CashDrawerOpened());
		}

		public void OpenDrawer()
		{
			_drawer.OpenDrawer();
		}

		public bool IsDrawerOpen
		{
			get { return _drawer.DrawerOpened; }
		}

		public void Claim()
		{
			_drawer.Claim(1000);
		}

		public void Enable()
		{
			_drawer.DeviceEnabled = true;
		}

		public void Release()
		{
			if (_drawer.Claimed)
			{
				if (_drawer.DeviceEnabled)
				{
					_drawer.DeviceEnabled = false;
				}
				_drawer.Release();
			}
		}
	}
}
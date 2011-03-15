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
				//TODO: fill in the rest of this appropriately.
				_printer = new MockReceiptPrinter();
			}
			return _printer;
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
namespace BBQRMSSolution.ViewModels
{
	public interface IPOSDeviceManager
	{
		ICashDrawer GetCashDrawer();
		IReceiptPrinter GetReceiptPrinter();
	}
}
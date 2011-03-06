namespace BBQRMSSolution.ViewModels
{
	public interface IReceiptPrinter
	{
		void PrintLine(string line);
		void Cut();
		void Claim();
		void Enable();
		void Release();
	}
}
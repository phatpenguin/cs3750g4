namespace BBQRMSSolution.Messages
{
	public class InvalidPinEntered
	{
		public InvalidPinEntered(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		public string ErrorMessage { get; set; }
	}
}
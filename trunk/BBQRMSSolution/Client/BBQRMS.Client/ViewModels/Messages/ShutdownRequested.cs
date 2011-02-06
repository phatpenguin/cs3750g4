namespace BBQRMSSolution.ViewModels.Messages
{
	public class ShutdownRequested
	{
		private bool mCancelled;
		public bool Cancelled
		{
			get { return mCancelled; }
			set
			{
				if(value)
					mCancelled = true;
			}
		}
	}
}
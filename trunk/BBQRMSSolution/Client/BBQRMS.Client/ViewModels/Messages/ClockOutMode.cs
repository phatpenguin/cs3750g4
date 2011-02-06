namespace BBQRMSSolution.ViewModels.Messages
{
	public class ClockOutMode
	{
		public bool ClockOutVisible { get; private set; }

		public ClockOutMode(bool clockOutVisible)
		{
			ClockOutVisible = clockOutVisible;
		}
	}
}
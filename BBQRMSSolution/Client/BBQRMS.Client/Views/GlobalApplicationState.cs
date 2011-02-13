using Controls;

namespace BBQRMSSolution.Views
{
	public static class GlobalApplicationState
	{
		static GlobalApplicationState()
		{
			MessageBus = new MessageBus();
		}
		public static IMessageBus MessageBus { get; private set; }

		/// <summary>
		/// This property is True while the application is doing an unconditional shutdown.
		/// </summary>
		public static bool ShuttingDown { get; set; }
	}
}
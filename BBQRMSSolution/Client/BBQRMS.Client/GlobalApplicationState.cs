using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution
{
	public static class GlobalApplicationState
	{
		static GlobalApplicationState()
		{
			MessageBus = new MessageBus();

			SecurityContext = new SecurityContext();

			MessageBus.Subscribe(SecurityContext);
		}
		public static IMessageBus MessageBus { get; private set; }

		public static SecurityContext SecurityContext { get; private set; }

        public static BBQRMSEntities Entities { get; set; }

		/// <summary>
		/// This property is True while the application is doing an unconditional shutdown.
		/// </summary>
		public static bool ShuttingDown { get; set; }
	}
}
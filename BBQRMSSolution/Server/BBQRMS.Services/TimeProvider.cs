using System;

namespace BBQRMS.WCFServices
{
	public class TimeProvider
	{
		private static IServerTimeProvider sCurrent = new DefaultTimeProvider();

		public static IServerTimeProvider Current
		{
			get { return sCurrent; }
			set {
				if (value == null)
					throw new ArgumentNullException("value");
				sCurrent = value;
			}
		}


		public static void ResetToDefault()
		{
			sCurrent = new DefaultTimeProvider();
		}
	}

	public interface IServerTimeProvider
	{
		DateTime UtcNow { get; }
		DateTime Now { get; }
	}

	public class DefaultTimeProvider : IServerTimeProvider
	{
		public DateTime UtcNow
		{
			get { return DateTime.UtcNow; }
		}

		public DateTime Now
		{
			get { return DateTime.Now; }
		}
	}

}
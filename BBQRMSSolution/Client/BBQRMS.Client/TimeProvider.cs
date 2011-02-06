using System;

namespace BBQRMSSolution
{
	public interface IClientTimeProvider
	{
		DateTime UtcNow { get; }
		DateTime Now { get; }
	}

	public abstract class TimeProvider
	{
		private static IClientTimeProvider sCurrent = new DefaultTimeProvider();

		public static IClientTimeProvider Current
		{
			get { return sCurrent; }
			set
			{
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

	public class DefaultTimeProvider : IClientTimeProvider
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
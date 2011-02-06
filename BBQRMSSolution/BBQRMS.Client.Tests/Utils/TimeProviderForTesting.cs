using System;
using BBQRMS.WCFServices;
using BBQRMSSolution;

namespace BBQRMS.Client.Tests
{
	public class TimeProviderForTesting : IClientTimeProvider, IServerTimeProvider
	{
		private TimeSpan mAdvance = TimeSpan.Zero;
		public void SkipForwardBy(TimeSpan toSkip)
		{
			mAdvance += toSkip;
		}

		public DateTime UtcNow
		{
			get { return DateTime.UtcNow + mAdvance; }
		}

		public DateTime Now
		{
			get { return DateTime.Now + mAdvance; }
		}
	}
}
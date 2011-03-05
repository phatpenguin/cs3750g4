using BBQRMSSolution.ServerProxy;

namespace BBQRMS.Client.Tests
{
	public static class PrepareMenuItem
	{
		public static MenuItem Of(string name, decimal price, string description)
		{
			var menuItem =
				new MenuItem
					{
						Name = name,
						Price = price,
						Description = description
					};
			return menuItem;
		}
	}
}
using BBQRMSSolution.ServerProxy;

namespace BBQRMS.Client.Tests
{
	public static class PrepareRole
	{
		public static Role Named(string roleName)
		{
			var role = new Role { Description = roleName };
			return role;
		}

		public static Role WithPrivelege(this Role role, int privelegeId)
		{
			var priv = new Privilege { Id = privelegeId };
			role.Privileges.Add(priv);
			return role;
		}
	}
}
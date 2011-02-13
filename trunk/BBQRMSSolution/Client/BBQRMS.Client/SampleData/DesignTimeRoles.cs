using System.Collections.Generic;
using BBQRMSSolution.ServerProxy;

namespace RUBPrototypes.SampleData
{
	public class DesignTimeRoles
	{
		private static IEnumerable<Role> mAll;
		static DesignTimeRoles()
		{
			mAll = new[]
			       	{
			       		new Role {Description = "Role 1"}, 
								new Role {Description = "Role 2"},
								new Role {Description = "Role 3"},
								new Role {Description = "Role 4"},
								new Role {Description = "Role 5"},
			       	};
		}
		public static IEnumerable<Role> All
		{
			get { return mAll; }
		}
	}
}
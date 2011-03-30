using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BBQRMS.Client.Tests
{
	[TestClass]
	public sealed class BeforeAllTests
	{
		[AssemblyInitialize]
		public static void DoOnce(TestContext context)
		{
			using (var eConn = (EntityConnection)new WCFServices.BBQRMSEntities().Connection)
			{
				using (var conn = (SqlConnection)eConn.StoreConnection)
				{
					conn.Open();
					SqlCommand cmd = conn.CreateCommand();
					cmd.CommandType = CommandType.Text;
					cmd.CommandText =
						@"
DELETE FROM Payment;
DELETE FROM OrderItem;
DELETE FROM MenuItemMap;
DELETE FROM MenuItem;
DELETE FROM Menu;
DELETE FROM [Order];
DELETE FROM ApplicationUser;
DELETE FROM EmployeeRoleMap;
DELETE FROM EmployeeTimeClock;
DELETE FROM ConsumedInventory;
DELETE FROM Employee;
DELETE FROM RolePrivilegeMap;
DELETE FROM Role;
";
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
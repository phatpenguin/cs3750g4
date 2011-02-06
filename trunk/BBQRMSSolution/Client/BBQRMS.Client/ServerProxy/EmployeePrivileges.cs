﻿/**************************************************************
 * This file is generated by a T4 template. regenerate it by 
 * right-clicking on the .tt file in solution explorer
 * and clicking "Run Custom Tool"
 **************************************************************/
using System.Linq;

namespace BBQRMSSolution.ServerProxy
{
	public partial class Employee
	{
		public bool CanTakeOrders { get { return Roles.Any(r => r.Privileges.Any(p => p.Id == 1)); } }
		public bool CanCashier { get { return Roles.Any(r => r.Privileges.Any(p => p.Id == 2)); } }
		public bool CanUseCooksScreen { get { return Roles.Any(r => r.Privileges.Any(p => p.Id == 3)); } }
		public bool CanUseQuickInventory { get { return Roles.Any(r => r.Privileges.Any(p => p.Id == 4)); } }
		public bool CanRunReports { get { return Roles.Any(r => r.Privileges.Any(p => p.Id == 5)); } }
		public bool CanManageEmployees { get { return Roles.Any(r => r.Privileges.Any(p => p.Id == 6)); } }
		public bool CanManageInventory { get { return Roles.Any(r => r.Privileges.Any(p => p.Id == 7)); } }
		public bool CanManageMenus { get { return Roles.Any(r => r.Privileges.Any(p => p.Id == 8)); } }
	}
}
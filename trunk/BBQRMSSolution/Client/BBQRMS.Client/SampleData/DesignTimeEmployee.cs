using System;
using System.Collections.Generic;
using System.Linq;
using BBQRMSSolution.ServerProxy;

namespace RUBPrototypes.SampleData
{
	public class DesignTimeEmployee : Employee
	{
		private readonly IEnumerable<Role> mRoles;
		private readonly IEnumerable<EmployeeTimeClock> mTimeClocks;

		public DesignTimeEmployee()
		{
			this.FirstName = "Mickey";
			this.LastName = "Mouse";
			mRoles = new List<Role> {DesignTimeRoles.All.First(), DesignTimeRoles.All.Skip(2).First() };
			this.HireDate = new DateTime(2007,10,2);
			this.EmployeePayType = DesignTimePayTypes.All.First();
			this.Id = 12;
			this.PayAmount = 7.45m;
			this.Phone1 = "111-555-1212";
			this.Phone2 = "(phone 2)";
			this.Phone3 = "(phone 3)";
			this.Address1 = "1234 Main";
			this.Address2 = "Anytown, USA 12345";
			this.Email1 = "noemail@noemail.net";
			this.Email2 = "(email 2)";
			mTimeClocks = new[] { new EmployeeTimeClock { ClockInTimeUTC = new DateTime(2010, 2, 12, 14, 35, 15), }, new EmployeeTimeClock { ClockInTimeUTC = new DateTime(2010, 1, 10, 9, 20, 00) }, };
		}

		public new IEnumerable<Role> Roles
		{
			get { return mRoles; }
		}

		public new IEnumerable<EmployeeTimeClock> EmployeeTimeClocks
		{
			get { return mTimeClocks; }
		}
	}
}
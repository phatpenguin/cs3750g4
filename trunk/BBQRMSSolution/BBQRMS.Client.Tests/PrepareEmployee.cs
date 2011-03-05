using System;
using BBQRMSSolution.ServerProxy;

namespace BBQRMS.Client.Tests
{
	public static class PrepareEmployee
	{
		public static Employee With(string firstName = "A", string lastName = "B", int payTypeId = 1, decimal payAmount = 9.75m )
		{
			return
				new Employee
					{
						HireDate = DateTime.Today,
						FirstName = firstName,
						LastName = lastName,
						PayTypeId = payTypeId,
						PayAmount = payAmount
					};
		}

		public static Employee HiredOn(this Employee emp, DateTime hireDate)
		{
			emp.HireDate = hireDate;
			return emp;
		}

		public static Employee AndRole(this Employee emp, Role role)
		{
			emp.Roles.Add(role);
			return emp;
		}
	}
}
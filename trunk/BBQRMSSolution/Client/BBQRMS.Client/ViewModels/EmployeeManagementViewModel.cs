using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class EmployeeManagementViewModel : ViewModelBase
	{
		private ObservableCollection<Employee> _employees;
		private ObservableCollection<EmployeePayType> _payTypes;
		private ObservableCollection<Role> _roles;
		private Employee _selectedEmployee;
		private EmployeePayType _selectedPayType;

		[Obsolete("Used for design-time.")]
		public EmployeeManagementViewModel()
		{
			_roles = new ObservableCollection<Role>();
			_payTypes = new ObservableCollection<EmployeePayType>();
			_employees =
				new ObservableCollection<Employee>
					{
						new Employee
							{
								FirstName = "First Name",
								LastName = "Last Name",
								ApplicationUser = new ApplicationUser {IdPart = "1", PersonalPart = "345"}
							}
					};
			_selectedEmployee = _employees[0];
		}

		public EmployeeManagementViewModel(BBQRMSEntities dataService)
		{
			DataService = dataService;

			ResetEmployeesList();

			ResetListSelectableLists();
		}

		private void ResetEmployeesList()
		{
			DataService.MergeOption = MergeOption.PreserveChanges;
			Employees = new ObservableCollection<Employee>
				(
				DataService.Employees.Expand("EmployeePayType").Expand("Roles").Expand("ApplicationUser")
					.Where(x => x.IsActive)
				);
			SelectedEmployee = Employees[0];
		}

		private void ResetListSelectableLists()
		{
			PayTypes = new ObservableCollection<EmployeePayType>(DataService.EmployeePayTypes.ToList());
			Roles = new ObservableCollection<Role>(DataService.Roles.ToList());
		}

		public ObservableCollection<Role> Roles
		{
			get { return _roles; }
			set { _roles = value; NotifyPropertyChanged("Roles"); }
		}

		public ObservableCollection<EmployeePayType> PayTypes
		{
			get { return _payTypes; }
			set { _payTypes = value; NotifyPropertyChanged("PayTypes"); }
		}


		public ObservableCollection<Employee> Employees
		{
			get { return _employees; }
			set { _employees = value; NotifyPropertyChanged("Employees"); }
		}

		public Employee SelectedEmployee
		{
			get { return _selectedEmployee; }
			set
			{
				_selectedEmployee = value;
				ResetListSelectableLists();

				if (_selectedEmployee != null && _selectedEmployee.Id > 0)
				{
					DataService.LoadProperty(_selectedEmployee, "Roles");
					DataService.LoadProperty(_selectedEmployee, "EmployeePayType");
					SelectedPayType = PayTypes.Where(x => x.Id == _selectedEmployee.EmployeePayType.Id).FirstOrDefault();
				}
				NotifyPropertyChanged("SelectedEmployee");
			}
		}

		public EmployeePayType SelectedPayType
		{
			get { return _selectedPayType; }
			set
			{
				_selectedPayType = value;
				NotifyPropertyChanged("SelectedPayType");
			}
		}

		public DelegateCommand SaveEmployee { get { return new DelegateCommand(HandleSaveClick); } }
		public DelegateCommand AddEmployee { get { return new DelegateCommand(HandleCreateEmployee); } }

		public void HandleSaveClick()
		{
			if (SelectedEmployee.Id > 0)
			{
				DataService.UpdateObject(SelectedEmployee);
				DataService.UpdateObject(SelectedEmployee.ApplicationUser);
				//TODO: handle all changes of roles.

				// Delete applicationuser record of deleted (inactivated) employees.
				if (!SelectedEmployee.IsActive && SelectedEmployee.ApplicationUser != null)
				{
					DataService.DeleteObject(SelectedEmployee.ApplicationUser);
				}
			}
			else
			{
				DataService.AddToEmployees(SelectedEmployee);
				DataService.AddToApplicationUsers(SelectedEmployee.ApplicationUser);
				DataService.SetLink(SelectedEmployee, "ApplicationUser", SelectedEmployee.ApplicationUser);
				foreach (Role role in SelectedEmployee.Roles)
				{
					DataService.AddLink(SelectedEmployee, "Roles", role);
					DataService.AddLink(role, "Employees", SelectedEmployee);
				}
			}
			DataService.SaveChanges(SaveChangesOptions.Batch);
			SelectedEmployee = null;
		}

		public void HandleCreateEmployee()
		{
			var newEmp = Employee.CreateEmployee(0, "New Employee", "", DateTime.Now.Date, 1, (decimal)4.45, true);
			newEmp.ApplicationUser = new ApplicationUser {IdPart = GenerateApplicationUserId(), PersonalPart = GeneratePersonalPart()};
			newEmp.ApplicationUser.Employee = newEmp;
			SelectedEmployee = newEmp;

			Employees.Add(SelectedEmployee);
		}

		private string GeneratePersonalPart()
		{
			var chars = "0123456789".ToArray();
			Random rnd = new Random();
			var toReturn = new string(new char[] {chars[rnd.Next(0, 10)], chars[rnd.Next(0, 10)], chars[rnd.Next(0, 10)]});
			return toReturn;
		}

		private string GenerateApplicationUserId()
		{
			var maxExistingNumericId =
				_employees
					.Where(e => e.ApplicationUser != null)
					.Select(e => e.ApplicationUser.IdPart)
					.Where(str => str.All(c => c >= '1' && c <= '9'))
					.Max(str => uint.Parse(str));

			// We won't see any ids that have zeroes in them.
			//  The only way we'd end up with zeroes after incrementing 
			//  is if we end up with a multiple of 10.
			var newId = maxExistingNumericId+1;
			if (newId % 10 == 0)
			{
				newId = uint.Parse(newId.ToString().Replace('0', '1'));
			}

			return newId.ToString();
		}

		public void HandleDeleteEmployee()
		{
			SelectedEmployee.IsActive = false;

			if (SelectedEmployee.Id != 0)
			{
				HandleSaveClick();
			}
			ResetEmployeesList();
		}

		public void HandlePayTypeSelectionChanged(Object employeePayType)
		{
			SelectedEmployee.EmployeePayType = (EmployeePayType)employeePayType;
			SelectedEmployee.PayTypeId = SelectedEmployee.EmployeePayType.Id;
		}
	}
}
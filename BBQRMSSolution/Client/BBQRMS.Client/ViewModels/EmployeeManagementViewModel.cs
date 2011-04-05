using System;
using System.Data.Services.Client;
using System.Linq;
using System.Collections.ObjectModel;
using BBQRMSSolution.ServerProxy;
using Controls;
using RUBPrototypes.SampleData;

namespace BBQRMSSolution.ViewModels
{
	public class EmployeeManagementViewModel : ViewModelBase
	{
        ObservableCollection<Employee> employees;
        private ObservableCollection<EmployeePayType> payTypes;
        private ObservableCollection<Role> roles;
        private Employee selectedEmployee;
        private EmployeePayType selectedPayType;

	    public EmployeeManagementViewModel(BBQRMSEntities dataService)
	    {
	    	DataService = dataService;

			ResetEmployeesList();

	        ResetListSelectableLists();
	    }

	    private void ResetEmployeesList()
	    {
	        DataService.MergeOption = MergeOption.PreserveChanges;
	        Employees = new ObservableCollection<Employee>(DataService.Employees.Expand("EmployeePayType").Expand("Roles")
                .Where(x=> x.IsActive)
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
	        get { return roles; }
	        set { roles = value; NotifyPropertyChanged("Roles");}
	    }

	    public ObservableCollection<EmployeePayType> PayTypes
	    {
	        get { return payTypes; }
	        set { payTypes = value;  NotifyPropertyChanged("PayTypes");}
	    }


	    public ObservableCollection<Employee> Employees
	    {
	        get { return employees; }
	        set { employees = value; NotifyPropertyChanged("Employees");}
	    }

	    public Employee SelectedEmployee
	    {
	        get { return selectedEmployee; }
	        set
	        {
	            selectedEmployee = value;
                ResetListSelectableLists();

                if (selectedEmployee != null && selectedEmployee.Id > 0)
                {
                    DataService.LoadProperty(selectedEmployee, "Roles");
                    DataService.LoadProperty(selectedEmployee, "EmployeePayType");
                    SelectedPayType = PayTypes.Where(x=> x.Id == selectedEmployee.EmployeePayType.Id).FirstOrDefault();
                }
                NotifyPropertyChanged("SelectedEmployee");
            }
	    }

	    public EmployeePayType SelectedPayType
	    {
	        get { return selectedPayType; }
	        set { 
                selectedPayType = value;
	            NotifyPropertyChanged("SelectedPayType");
            }
	    }

	    public DelegateCommand SaveEmployee { get { return new DelegateCommand(HandleSaveClick); } }
        public DelegateCommand AddEmployee { get { return new DelegateCommand(HandleCreateEmployee); } }

	    public void HandleSaveClick() {
            if (SelectedEmployee.Id > 0) {
                DataService.UpdateObject(SelectedEmployee);
            } else {
                DataService.AddToEmployees(SelectedEmployee);
            }
	        DataService.SaveChanges();
	        SelectedEmployee = null;
	    }

        public void HandleCreateEmployee()
        {
            SelectedEmployee = Employee.CreateEmployee(0, "New Employee", "", DateTime.Now.Date, 1, (decimal)4.45, true);
            Employees.Add(SelectedEmployee);
        }

        public void HandleDeleteEmployee()
        {
            SelectedEmployee.IsActive = false;
            if (SelectedEmployee.Id != 0) {
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
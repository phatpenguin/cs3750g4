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

		[Obsolete("Used for design-time only", true)]
		public EmployeeManagementViewModel()
		{
			Employees = new ObservableCollection<Employee> {new DesignTimeEmployee(), new DesignTimeEmployee()};
			SelectedEmployee = Employees[1];
		}
	    public EmployeeManagementViewModel(BBQRMSEntities dataService)
	    {
	    	DataService = dataService;

				DataService.MergeOption = MergeOption.PreserveChanges;
	        Employees = new ObservableCollection<Employee>(DataService.Employees.Expand("EmployeePayType").Expand("Roles"));
/*
	        foreach (Employee employee in employees)
	        {
                mDataService.LoadProperty(employee, "EmployeePayType");
                mDataService.LoadProperty(employee, "Roles");
            }
*/
	        PayTypes = new ObservableCollection<EmployeePayType>(DataService.EmployeePayTypes);
	        Roles = new ObservableCollection<Role>(DataService.Roles);
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
	        set { selectedEmployee = value; NotifyPropertyChanged("SelectedEmployee");
	            //HandleDisplayEmployeeInfoView(selectedEmployee);
	        }
	    }

        public DelegateCommand SaveEmployee { get { return new DelegateCommand(HandleSaveClick); } }
        public DelegateCommand AddEmployee { get { return new DelegateCommand(HandleCreateEmployee); } }

	    public void HandleSaveClick()
	    {
           
            if (SelectedEmployee.Id > 0) {
                DataService.UpdateObject(SelectedEmployee);
            } else {
                DataService.AddToEmployees(SelectedEmployee);
            }
	        DataService.SaveChanges();
        }

        public void HandleCreateEmployee()
        {
            SelectedEmployee = new Employee();
            Employees.Add(SelectedEmployee);
            //Employees = new ObservableCollection<Employee>(mDataService.Employees);
        }
	}
}
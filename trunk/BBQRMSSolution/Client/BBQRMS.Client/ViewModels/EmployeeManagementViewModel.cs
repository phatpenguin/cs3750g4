using System;
using System.Linq;
using System.Collections.ObjectModel;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class EmployeeManagementViewModel : ViewModelBase
	{
        private readonly BBQRMSEntities mDataService;
        ObservableCollection<Employee> employees;
        private ObservableCollection<EmployeePayType> payTypes;
        private ObservableCollection<Role> roles;
        private Employee selectedEmployee;
        private IMessageBus mMessageBus;

	    public EmployeeManagementViewModel()
	    {
	        mDataService = GlobalApplicationState.Entities;
	        this.mMessageBus = GlobalApplicationState.MessageBus;

	        Employees = new ObservableCollection<Employee>(mDataService.Employees.Expand("EmployeePayType").Expand("Roles"));
/*
	        foreach (Employee employee in employees)
	        {
                mDataService.LoadProperty(employee, "EmployeePayType");
                mDataService.LoadProperty(employee, "Roles");
            }
*/
	        PayTypes = new ObservableCollection<EmployeePayType>(mDataService.EmployeePayTypes);
	        Roles = new ObservableCollection<Role>(mDataService.Roles);
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
                mDataService.UpdateObject(SelectedEmployee);
            } else {
                mDataService.AddToEmployees(SelectedEmployee);
            }
	        mDataService.SaveChanges();
        }

        public void HandleCreateEmployee()
        {
            SelectedEmployee = new Employee();
            Employees.Add(SelectedEmployee);
            //Employees = new ObservableCollection<Employee>(mDataService.Employees);
        }
	}
}
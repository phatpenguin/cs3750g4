using System;
using System.Collections.ObjectModel;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class EmployeeManagementViewModel : ViewModelBase
	{
        private readonly BBQRMSEntities mDataService;
        ObservableCollection<Employee> employees;

		[Obsolete("This constructor to be used only by the VS designer")]
		public EmployeeManagementViewModel()
		{
			
		}

	    public EmployeeManagementViewModel(BBQRMSEntities bbqrmsEntities, IMessageBus mMessageBus)
	    {
	        mDataService = bbqrmsEntities;
	        this.mMessageBus = mMessageBus;

	        Employees = new ObservableCollection<Employee>(mDataService.Employees);
	    }

	    
	    public ObservableCollection<Employee> Employees
	    {
	        get { return employees; }
	        set { employees = value; NotifyPropertyChanged("Employees");}
	    }

	    private Employee selectedEmployee;
	    private IMessageBus mMessageBus;

	    public Employee SelectedEmployee
	    {
	        get { return selectedEmployee; }
	        set { selectedEmployee = value; NotifyPropertyChanged("SelectedEmployee");
	            //HandleDisplayEmployeeInfoView(selectedEmployee);
	        }
	    }

	    public void HandleSaveClick()
	    {
            if (SelectedEmployee.Id > 0)
            {
                mDataService.UpdateObject(selectedEmployee);
            }
            else
            {
                mDataService.AddToEmployees(SelectedEmployee);
            }
	    }

        public void HandleCreateEmployee()
        {
            SelectedEmployee = new Employee();
            Employees.Add(SelectedEmployee);
            //Employees = new ObservableCollection<Employee>(mDataService.Employees);
        }
	}
}
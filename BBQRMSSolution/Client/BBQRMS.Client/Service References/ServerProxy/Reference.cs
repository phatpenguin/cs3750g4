//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Original file name:
// Generation date: 1/28/2011 6:07:06 AM
using System.Collections.ObjectModel;
using System.Data.Services.Client;

namespace BBQRMSSolution.ServerProxy
{
    
    /// <summary>
    /// There are no comments for BBQRMSEntities in the schema.
    /// </summary>
    public partial class BBQRMSEntities : global::System.Data.Services.Client.DataServiceContext
    {
        /// <summary>
        /// Initialize a new BBQRMSEntities object.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public BBQRMSEntities(global::System.Uri serviceRoot) : 
                base(serviceRoot)
        {
            this.ResolveName = new global::System.Func<global::System.Type, string>(this.ResolveNameFromType);
            this.ResolveType = new global::System.Func<string, global::System.Type>(this.ResolveTypeFromName);
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// Since the namespace configured for this service reference
        /// in Visual Studio is different from the one indicated in the
        /// server schema, use type-mappers to map between the two.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected global::System.Type ResolveTypeFromName(string typeName)
        {
            if (typeName.StartsWith("BBQRMSModel", global::System.StringComparison.Ordinal))
            {
                return this.GetType().Assembly.GetType(string.Concat("BBQRMSSolution.ServerProxy", typeName.Substring(11)), false);
            }
            return null;
        }
        /// <summary>
        /// Since the namespace configured for this service reference
        /// in Visual Studio is different from the one indicated in the
        /// server schema, use type-mappers to map between the two.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected string ResolveNameFromType(global::System.Type clientType)
        {
            if (clientType.Namespace.Equals("BBQRMSSolution.ServerProxy", global::System.StringComparison.Ordinal))
            {
                return string.Concat("BBQRMSModel.", clientType.Name);
            }
            return null;
        }
        /// <summary>
        /// There are no comments for ApplicationUsers in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<ApplicationUser> ApplicationUsers
        {
            get
            {
                if ((this._ApplicationUsers == null))
                {
                    this._ApplicationUsers = base.CreateQuery<ApplicationUser>("ApplicationUsers");
                }
                return this._ApplicationUsers;
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<ApplicationUser> _ApplicationUsers;
        /// <summary>
        /// There are no comments for Employees in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<Employee> Employees
        {
            get
            {
                if ((this._Employees == null))
                {
                    this._Employees = base.CreateQuery<Employee>("Employees");
                }
                return this._Employees;
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<Employee> _Employees;
        /// <summary>
        /// There are no comments for Menus in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<Menu> Menus
        {
            get
            {
                if ((this._Menus == null))
                {
                    this._Menus = base.CreateQuery<Menu>("Menus");
                }
                return this._Menus;
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<Menu> _Menus;
        /// <summary>
        /// There are no comments for MenuItems in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<MenuItem> MenuItems
        {
            get
            {
                if ((this._MenuItems == null))
                {
                    this._MenuItems = base.CreateQuery<MenuItem>("MenuItems");
                }
                return this._MenuItems;
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<MenuItem> _MenuItems;
        /// <summary>
        /// There are no comments for ApplicationUsers in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToApplicationUsers(ApplicationUser applicationUser)
        {
            base.AddObject("ApplicationUsers", applicationUser);
        }
        /// <summary>
        /// There are no comments for Employees in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToEmployees(Employee employee)
        {
            base.AddObject("Employees", employee);
        }
        /// <summary>
        /// There are no comments for Menus in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToMenus(Menu menu)
        {
            base.AddObject("Menus", menu);
        }
        /// <summary>
        /// There are no comments for MenuItems in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToMenuItems(MenuItem menuItem)
        {
            base.AddObject("MenuItems", menuItem);
        }
    }
    /// <summary>
    /// There are no comments for BBQRMSModel.ApplicationUser in the schema.
    /// </summary>
    /// <KeyProperties>
    /// idPart
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("ApplicationUsers")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("idPart")]
    public partial class ApplicationUser : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new ApplicationUser object.
        /// </summary>
        /// <param name="idPart">Initial value of idPart.</param>
        /// <param name="personalPart">Initial value of personalPart.</param>
        /// <param name="employeeId">Initial value of employeeId.</param>
        /// <param name="displayName">Initial value of displayName.</param>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static ApplicationUser CreateApplicationUser(string idPart, string personalPart, int employeeId, string displayName)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.idPart = idPart;
            applicationUser.personalPart = personalPart;
            applicationUser.employeeId = employeeId;
            applicationUser.displayName = displayName;
            return applicationUser;
        }
        /// <summary>
        /// There are no comments for Property idPart in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string idPart
        {
            get
            {
                return this._idPart;
            }
            set
            {
                this.OnidPartChanging(value);
                this._idPart = value;
                this.OnidPartChanged();
                this.OnPropertyChanged("idPart");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _idPart;
        partial void OnidPartChanging(string value);
        partial void OnidPartChanged();
        /// <summary>
        /// There are no comments for Property personalPart in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string personalPart
        {
            get
            {
                return this._personalPart;
            }
            set
            {
                this.OnpersonalPartChanging(value);
                this._personalPart = value;
                this.OnpersonalPartChanged();
                this.OnPropertyChanged("personalPart");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _personalPart;
        partial void OnpersonalPartChanging(string value);
        partial void OnpersonalPartChanged();
        /// <summary>
        /// There are no comments for Property employeeId in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int employeeId
        {
            get
            {
                return this._employeeId;
            }
            set
            {
                this.OnemployeeIdChanging(value);
                this._employeeId = value;
                this.OnemployeeIdChanged();
                this.OnPropertyChanged("employeeId");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _employeeId;
        partial void OnemployeeIdChanging(int value);
        partial void OnemployeeIdChanged();
        /// <summary>
        /// There are no comments for Property displayName in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string displayName
        {
            get
            {
                return this._displayName;
            }
            set
            {
                this.OndisplayNameChanging(value);
                this._displayName = value;
                this.OndisplayNameChanged();
                this.OnPropertyChanged("displayName");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _displayName;
        partial void OndisplayNameChanging(string value);
        partial void OndisplayNameChanged();
        /// <summary>
        /// There are no comments for Employee in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public Employee Employee
        {
            get
            {
                return this._Employee;
            }
            set
            {
                this._Employee = value;
                this.OnPropertyChanged("Employee");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private Employee _Employee;
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// There are no comments for BBQRMSModel.Employee in the schema.
    /// </summary>
    /// <KeyProperties>
    /// id
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("Employees")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("id")]
    public partial class Employee : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new Employee object.
        /// </summary>
        /// <param name="ID">Initial value of id.</param>
        /// <param name="firstName">Initial value of firstName.</param>
        /// <param name="lastName">Initial value of lastName.</param>
        /// <param name="hireDate">Initial value of hireDate.</param>
        /// <param name="payTypeId">Initial value of payTypeId.</param>
        /// <param name="payAmount">Initial value of payAmount.</param>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static Employee CreateEmployee(int ID, string firstName, string lastName, global::System.DateTime hireDate, int payTypeId, decimal payAmount)
        {
            Employee employee = new Employee();
            employee.id = ID;
            employee.firstName = firstName;
            employee.lastName = lastName;
            employee.hireDate = hireDate;
            employee.payTypeId = payTypeId;
            employee.payAmount = payAmount;
            return employee;
        }
        /// <summary>
        /// There are no comments for Property id in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                this.OnidChanging(value);
                this._id = value;
                this.OnidChanged();
                this.OnPropertyChanged("id");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _id;
        partial void OnidChanging(int value);
        partial void OnidChanged();
        /// <summary>
        /// There are no comments for Property firstName in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string firstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                this.OnfirstNameChanging(value);
                this._firstName = value;
                this.OnfirstNameChanged();
                this.OnPropertyChanged("firstName");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _firstName;
        partial void OnfirstNameChanging(string value);
        partial void OnfirstNameChanged();
        /// <summary>
        /// There are no comments for Property lastName in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string lastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                this.OnlastNameChanging(value);
                this._lastName = value;
                this.OnlastNameChanged();
                this.OnPropertyChanged("lastName");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _lastName;
        partial void OnlastNameChanging(string value);
        partial void OnlastNameChanged();
        /// <summary>
        /// There are no comments for Property hireDate in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.DateTime hireDate
        {
            get
            {
                return this._hireDate;
            }
            set
            {
                this.OnhireDateChanging(value);
                this._hireDate = value;
                this.OnhireDateChanged();
                this.OnPropertyChanged("hireDate");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.DateTime _hireDate;
        partial void OnhireDateChanging(global::System.DateTime value);
        partial void OnhireDateChanged();
        /// <summary>
        /// There are no comments for Property phone1 in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string phone1
        {
            get
            {
                return this._phone1;
            }
            set
            {
                this.Onphone1Changing(value);
                this._phone1 = value;
                this.Onphone1Changed();
                this.OnPropertyChanged("phone1");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _phone1;
        partial void Onphone1Changing(string value);
        partial void Onphone1Changed();
        /// <summary>
        /// There are no comments for Property phone2 in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string phone2
        {
            get
            {
                return this._phone2;
            }
            set
            {
                this.Onphone2Changing(value);
                this._phone2 = value;
                this.Onphone2Changed();
                this.OnPropertyChanged("phone2");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _phone2;
        partial void Onphone2Changing(string value);
        partial void Onphone2Changed();
        /// <summary>
        /// There are no comments for Property phone3 in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string phone3
        {
            get
            {
                return this._phone3;
            }
            set
            {
                this.Onphone3Changing(value);
                this._phone3 = value;
                this.Onphone3Changed();
                this.OnPropertyChanged("phone3");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _phone3;
        partial void Onphone3Changing(string value);
        partial void Onphone3Changed();
        /// <summary>
        /// There are no comments for Property address1 in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string address1
        {
            get
            {
                return this._address1;
            }
            set
            {
                this.Onaddress1Changing(value);
                this._address1 = value;
                this.Onaddress1Changed();
                this.OnPropertyChanged("address1");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _address1;
        partial void Onaddress1Changing(string value);
        partial void Onaddress1Changed();
        /// <summary>
        /// There are no comments for Property address2 in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string address2
        {
            get
            {
                return this._address2;
            }
            set
            {
                this.Onaddress2Changing(value);
                this._address2 = value;
                this.Onaddress2Changed();
                this.OnPropertyChanged("address2");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _address2;
        partial void Onaddress2Changing(string value);
        partial void Onaddress2Changed();
        /// <summary>
        /// There are no comments for Property email1 in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string email1
        {
            get
            {
                return this._email1;
            }
            set
            {
                this.Onemail1Changing(value);
                this._email1 = value;
                this.Onemail1Changed();
                this.OnPropertyChanged("email1");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _email1;
        partial void Onemail1Changing(string value);
        partial void Onemail1Changed();
        /// <summary>
        /// There are no comments for Property email2 in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string email2
        {
            get
            {
                return this._email2;
            }
            set
            {
                this.Onemail2Changing(value);
                this._email2 = value;
                this.Onemail2Changed();
                this.OnPropertyChanged("email2");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _email2;
        partial void Onemail2Changing(string value);
        partial void Onemail2Changed();
        /// <summary>
        /// There are no comments for Property payTypeId in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int payTypeId
        {
            get
            {
                return this._payTypeId;
            }
            set
            {
                this.OnpayTypeIdChanging(value);
                this._payTypeId = value;
                this.OnpayTypeIdChanged();
                this.OnPropertyChanged("payTypeId");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _payTypeId;
        partial void OnpayTypeIdChanging(int value);
        partial void OnpayTypeIdChanged();
        /// <summary>
        /// There are no comments for Property payAmount in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public decimal payAmount
        {
            get
            {
                return this._payAmount;
            }
            set
            {
                this.OnpayAmountChanging(value);
                this._payAmount = value;
                this.OnpayAmountChanged();
                this.OnPropertyChanged("payAmount");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private decimal _payAmount;
        partial void OnpayAmountChanging(decimal value);
        partial void OnpayAmountChanged();
        /// <summary>
        /// There are no comments for ApplicationUsers in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceCollection<ApplicationUser> ApplicationUsers
        {
            get
            {
                return this._ApplicationUsers;
            }
            set
            {
                this._ApplicationUsers = value;
                this.OnPropertyChanged("ApplicationUsers");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceCollection<ApplicationUser> _ApplicationUsers = new global::System.Data.Services.Client.DataServiceCollection<ApplicationUser>(null, System.Data.Services.Client.TrackingMode.None);
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// There are no comments for BBQRMSModel.Menu in the schema.
    /// </summary>
    /// <KeyProperties>
    /// ID
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("Menus")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("ID")]
    public partial class Menu : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new Menu object.
        /// </summary>
        /// <param name="ID">Initial value of ID.</param>
        /// <param name="name">Initial value of Name.</param>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static Menu CreateMenu(int ID, string name)
        {
            Menu menu = new Menu();
            menu.ID = ID;
            menu.Name = name;
            return menu;
        }
        /// <summary>
        /// There are no comments for Property ID in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this.OnIDChanging(value);
                this._ID = value;
                this.OnIDChanged();
                this.OnPropertyChanged("ID");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _ID;
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
        /// <summary>
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for MenuItems in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceCollection<MenuItem> MenuItems
        {
            get
            {
                return this._MenuItems;
            }
            set
            {
                this._MenuItems = value;
                this.OnPropertyChanged("MenuItems");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceCollection<MenuItem> _MenuItems = new global::System.Data.Services.Client.DataServiceCollection<MenuItem>(null, System.Data.Services.Client.TrackingMode.None);
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// There are no comments for BBQRMSModel.MenuItem in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("MenuItems")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Id")]
    public partial class MenuItem : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new MenuItem object.
        /// </summary>
        /// <param name="ID">Initial value of Id.</param>
        /// <param name="name">Initial value of Name.</param>
        /// <param name="description">Initial value of Description.</param>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static MenuItem CreateMenuItem(int ID, string name, string description)
        {
            MenuItem menuItem = new MenuItem();
            menuItem.Id = ID;
            menuItem.Name = name;
            menuItem.Description = description;
            return menuItem;
        }
        /// <summary>
        /// There are no comments for Property Id in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _Id;
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        /// <summary>
        /// There are no comments for Property Price in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<decimal> Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this.OnPriceChanging(value);
                this._Price = value;
                this.OnPriceChanged();
                this.OnPropertyChanged("Price");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<decimal> _Price;
        partial void OnPriceChanging(global::System.Nullable<decimal> value);
        partial void OnPriceChanged();
        /// <summary>
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for Property Description in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this.OnDescriptionChanging(value);
                this._Description = value;
                this.OnDescriptionChanged();
                this.OnPropertyChanged("Description");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Description;
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        /// <summary>
        /// There are no comments for Menus in the schema.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceCollection<Menu> Menus
        {
            get
            {
                return this._Menus;
            }
            set
            {
                this._Menus = value;
                this.OnPropertyChanged("Menus");
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceCollection<Menu> _Menus = new global::System.Data.Services.Client.DataServiceCollection<Menu>(null, System.Data.Services.Client.TrackingMode.None);
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using BBQRMSSolution.ServerProxy;

namespace BBQRMSSolution.ViewModels
{
    public class RoleManagementViewModel : ViewModelBase
    {
        private ObservableCollection<Role> roles;
        private Role selectedRole;
        private ObservableCollection<Privilege> _privileges;
        private ObservableCollection<Privilege> _selectedPrivileges;

        public RoleManagementViewModel(BBQRMSEntities dataService)
	    {
	    	DataService = dataService;
            ResetSelectablePrivileges();
            ResetRoleList();
        }

        private void ResetRoleList() {
            Roles = new ObservableCollection<Role>(DataService.Roles.ToList());
            SelectedRole = Roles[0];
        }

        private void ResetSelectablePrivileges()
        {
            Privileges = new ObservableCollection<Privilege>(DataService.Privileges.ToList());
            //Privileges_CollectionChanged(null, null);
            Privileges.CollectionChanged += Privileges_CollectionChanged;
        }

        public ObservableCollection<Privilege> Privileges { 
            get { return _privileges; }
            set {
                _privileges = value;
                NotifyPropertyChanged("Privileges");
            }
        }

        public ObservableCollection<Privilege> SelectedPrivileges
        {
            get { return _selectedPrivileges; }
            set
            {
                _selectedPrivileges = value;
                NotifyPropertyChanged("SelectedPrivileges");
            }
        }

        private void Privileges_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Privileges.CollectionChanged -= Privileges_CollectionChanged;
            SelectedPrivileges.CollectionChanged -= SelectedPrivileges_CollectionChanged;
            SelectedPrivileges = new ObservableCollection<Privilege>(SelectedRole.Privileges.Where(x=>!Privileges.Contains(x)));
            SelectedPrivileges.CollectionChanged += SelectedPrivileges_CollectionChanged;
            Privileges.CollectionChanged += Privileges_CollectionChanged;
        }


        public void SelectedPrivileges_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SelectedPrivileges.CollectionChanged -= SelectedPrivileges_CollectionChanged;
            Privileges.CollectionChanged -= Privileges_CollectionChanged;
            Privileges = new ObservableCollection<Privilege>(DataService.Privileges.ToList().Where(x => !SelectedPrivileges.Contains(x)));
            Privileges.CollectionChanged += Privileges_CollectionChanged;
            SelectedPrivileges.CollectionChanged += SelectedPrivileges_CollectionChanged;
        }

        public ObservableCollection<Role> Roles
        {
            get { return roles; }
            set { roles = value; NotifyPropertyChanged("Roles"); }
        }

        public Role SelectedRole {
            get { return selectedRole; }
            set { 
                selectedRole = value;
                if (selectedRole != null) {
                    if (selectedRole.Id > 0) {
                        DataService.LoadProperty(selectedRole, "Privileges");
                    }
                    SelectedPrivileges = new ObservableCollection<Privilege>(selectedRole.Privileges.ToList());
                    SelectedPrivileges_CollectionChanged(null, null);
                    SelectedPrivileges.CollectionChanged += SelectedPrivileges_CollectionChanged;
                }
                NotifyPropertyChanged("SelectedRole"); 
            }
        }

        public void HandleSaveRole()
        {

            if (SelectedRole.Id > 0) {
                DataService.UpdateObject(SelectedRole);
            } else {
                DataService.AddToRoles(SelectedRole);
            }

            var list = SelectedRole.Privileges.Except(SelectedPrivileges).ToList();
            foreach (Privilege privilege in list)
            {
                DataService.DeleteLink(SelectedRole, "Privileges", privilege);
                SelectedRole.Privileges.Remove(privilege);
            }

            list = SelectedPrivileges.Except(SelectedRole.Privileges).ToList();
            foreach (Privilege privilege in list)
            {
                DataService.AddLink(SelectedRole, "Privileges", privilege);
                SelectedRole.Privileges.Add(privilege);
            }

            DataService.SaveChanges();
            SelectedRole = null;
            ResetRoleList();
        }

        public void HandleCreateRole()
        {
            SelectedRole = Role.CreateRole(0, "New Role");
            Roles.Add(SelectedRole);
        }

        public void HandleDeleteRole() { 
            if (SelectedRole.Id > 0) {
                //SelectedRole.Active = false;
                HandleSaveRole();
            }
            SelectedRole = null;
        }
    }
}

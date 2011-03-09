using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using BBQRMSSolution.ServerProxy;

namespace BBQRMSSolution.ViewModels
{
    public class MenuItemManagementViewModel : ViewModelBase
    {

        private ObservableCollection<MenuItem> menuItems;
        private MenuItem selectedMenuItem;

        public MenuItemManagementViewModel(BBQRMSEntities dataService)
	    {
	    	DataService = dataService;

			ResetList();

	    }

	    private void ResetList()
	    {
	        DataService.MergeOption = MergeOption.PreserveChanges;
            MenuItems = new ObservableCollection<MenuItem>(DataService.MenuItems);
            SelectedMenuItem = MenuItems[0];
	    }

        public ObservableCollection<MenuItem> MenuItems
	    {
	        get { return menuItems; }
	        set { menuItems = value; NotifyPropertyChanged("MenuItems");}
	    }

        public MenuItem SelectedMenuItem
	    {
            get { return selectedMenuItem; }
	        set
	        {
                selectedMenuItem = value;

                NotifyPropertyChanged("SelectedMenuItem");
            }
	    }

	    public void HandleSaveClick() {
            if (SelectedMenuItem.Id > 0) {
                DataService.UpdateObject(SelectedMenuItem);
            } else {
                DataService.AddToMenuItems(SelectedMenuItem);      
            }
	        DataService.SaveChanges();
	        SelectedMenuItem = null;
	    }

        public void HandleCreateItem()
        {
            SelectedMenuItem = new MenuItem();
            SelectedMenuItem.Name = "New Menu Item";
            MenuItems.Add(SelectedMenuItem);
        }

        public void HandleDeleteMenuItem()
        {
            HandleSaveClick();
            ResetList();
        }

    }
}

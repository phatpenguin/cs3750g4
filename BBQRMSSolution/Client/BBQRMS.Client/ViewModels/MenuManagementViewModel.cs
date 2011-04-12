using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BBQRMSSolution.ServerProxy;

namespace BBQRMSSolution.ViewModels
{
	public class MenuManagementViewModel : ViewModelBase
	{

		public MenuManagementViewModel(BBQRMSEntities dataService)
		{
			DataService = dataService;
			Reset();
		}

		private void Reset()
		{
			Menus = new ObservableCollection<Menu>(DataService.Menus.Expand("MenuItems").Where(x => x.IsActive));
			MenuItems = new ObservableCollection<MenuItem>(DataService.MenuItems);
			SelectedMenu = Menus[0];
			SelectedMenuItem = MenuItems[0];
		}

		private MenuItem _selectedMenuItem;
		public MenuItem SelectedMenuItem
		{
			get { return _selectedMenuItem; }
			set { _selectedMenuItem = value; NotifyPropertyChanged("SelectedMenuItem"); }
		}

		private Menu _selectedMenu;
		public Menu SelectedMenu
		{
			get { return _selectedMenu; }
			set { _selectedMenu = value; NotifyPropertyChanged("SelectedMenu"); }
		}

		private ObservableCollection<MenuItem> _menuItems;
		public ObservableCollection<MenuItem> MenuItems
		{
			get { return _menuItems; }
			set { _menuItems = value; NotifyPropertyChanged("MenuItems"); }
		}

		private ObservableCollection<Menu> _menus;
		public ObservableCollection<Menu> Menus
		{
			get { return _menus; }
			set { _menus = value; NotifyPropertyChanged("Menus"); }
		}

		public void HandleSaveMenuItem()
		{
		}

		public void HandleSaveMenu()
		{
			if (SelectedMenu.Id > 0)
			{
				DataService.UpdateObject(SelectedMenu);
				var oldMenuItems =
					DataService
						.Links
						.Where(ld => ld.Source == SelectedMenu && ld.SourceProperty == "MenuItems")
						.Select(ld => ld.Target)
						.OfType<MenuItem>();

				var newMenuItems = SelectedMenu.MenuItems;

				var toAdd = newMenuItems.Except(oldMenuItems);
				var toRemove = oldMenuItems.Except(newMenuItems);
				foreach (MenuItem menuItem in toRemove)
				{
					DataService.DeleteLink(SelectedMenu, "MenuItems", menuItem);
				}
				foreach (MenuItem menuItem in toAdd)
				{
					DataService.AddLink(SelectedMenu, "MenuItems", menuItem);
				}
			}
			else
			{
				DataService.AddToMenus(SelectedMenu);
				var toAdd = SelectedMenu.MenuItems;
				foreach (MenuItem menuItem in toAdd)
				{
					DataService.AddLink(SelectedMenu, "MenuItems", menuItem);
				}
			}
			DataService.SaveChanges();
			SelectedMenu = null;
		}

		public void HandleAddMenu()
		{
			Menu menu = Menu.CreateMenu(0, "New Menu", true);
			Menus.Add(menu);
			SelectedMenu = menu;
		}

		public void HandleDelete()
		{
			SelectedMenu.IsActive = false;
			HandleSaveMenu();
			Reset();
		}
	}
}

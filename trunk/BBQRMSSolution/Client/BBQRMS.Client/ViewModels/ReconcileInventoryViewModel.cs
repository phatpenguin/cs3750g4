using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class ReconcileInventoryViewModel : ViewModelBase
	{
		private ObservableCollection<MasterInventory> _MasterInventory;
		private MasterInventory _SelectedMasterInventory;
		private Boolean isVisible = true;
		private IEnumerable<InventoryLocationType> _inventoryLocations;


		[Obsolete("Used for design-time only", true)]
		public ReconcileInventoryViewModel()
		{
			// fill in the properties with some simulated data for the VS designer
			InventoryLocations =
				new[]
					{
						new InventoryLocationType {Id = 1, Description = "Fridge"},
						new InventoryLocationType {Id = 2, Description = "Pantry"},
						new InventoryLocationType {Id = 3, Description = "Freezer"},
					};
			MasterInventories =
				new ObservableCollection<MasterInventory>
							{
								new MasterInventory {Id = 1, Name = "Chicken", UnitOfMeasure = "lbs", IsActive = true, UnitQty = 10, MinQuantity = 5, MaxQuantity = 30, LocationId = 1},
								new MasterInventory {Id = 1, Name = "Ribs", UnitOfMeasure = "lbs", IsActive = true, UnitQty = 10, },
								new MasterInventory {Id = 1, Name = "Sauce", UnitOfMeasure = "lbs", IsActive = true, UnitQty = 10, },
							};
			SelectedMasterInventory = MasterInventories[0];
		}

		public ReconcileInventoryViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
		{
			DataService = dataService;
			MessageBus = messageBus;
			InventoryLocations = DataService.InventoryLocationTypes.Execute();
			resetList();
		}
		
		private void resetList()
		{
			MasterInventories = new ObservableCollection<MasterInventory>(DataService.MasterInventories.Where(x => x.IsActive == true));
		}
		
		//property --kinda like a method but also an attribute
		public IEnumerable<InventoryLocationType> InventoryLocations
		{
			get { return _inventoryLocations; }
			set
			{
				if (value != _inventoryLocations)
				{
					_inventoryLocations = value;
					NotifyPropertyChanged("InventoryLocations");
				}
			}
		}

		public ObservableCollection<MasterInventory> MasterInventories
		{
			get
			{
				return _MasterInventory;
			}
			set
			{
				_MasterInventory = value;
				NotifyPropertyChanged("MasterInventories");

			}

		}

		public MasterInventory SelectedMasterInventory
		{
			get
			{
				return _SelectedMasterInventory;
			}
			set
			{
				_SelectedMasterInventory = value;
				NotifyPropertyChanged("SelectedMasterInventory");
			}
		}
	
		public Boolean IsVisible
		{
			get
			{
				return isVisible;
			}
			set
			{
				isVisible = value;
				NotifyPropertyChanged("IsVisible");
			}
		}
		
		internal void SaveItem()
		{

			if (_SelectedMasterInventory.Id > 0)
			{
				DataService.UpdateObject(_SelectedMasterInventory);

			}
			else
			{
				DataService.AddToMasterInventories(_SelectedMasterInventory);

			}
			DataService.SaveChanges();
			resetList();
		}

		internal void DeleteItem()
		{
			// can't delete items from inventory...Must set inactive.
			// DataService.DeleteObject(_MasterInventory);
			// DataService.SaveChanges();
			if (_SelectedMasterInventory != null)
			{
				_SelectedMasterInventory.IsActive = false;
				SaveItem();
			}

		}
	}
}

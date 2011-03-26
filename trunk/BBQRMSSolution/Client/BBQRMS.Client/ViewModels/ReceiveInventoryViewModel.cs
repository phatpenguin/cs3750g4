using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBQRMSSolution.ServerProxy;
using System.Collections.ObjectModel;

namespace BBQRMSSolution.ViewModels
{
	public class ReceiveInventoryViewModel : ViewModelBase
	{
		private Boolean _isVisible = true;
		private MasterInventory _masterInventory;
		private IEnumerable<InventoryLocationType> _inventoryLocations;

		[Obsolete("Used for design-time only", true)]
		public ReceiveInventoryViewModel()
		{
			InventoryLocations =
				new[]
					{
						new InventoryLocationType {Id = 1, Description = "Fridge"},
						new InventoryLocationType {Id = 2, Description = "Pantry"},
						new InventoryLocationType {Id = 3, Description = "Freezer"},
					};
			Item =
				new MasterInventory
					{
						Id = 99,
						Name = "Some fancy new thing",
						LocationId = 2,
						UnitOfMeasure = "boxes",
						UnitQty = 1,
						MinQuantity = 0,
						MaxQuantity = 5,
						IsActive = true,
					};
		}

		public ReceiveInventoryViewModel(BBQRMSEntities dataService, Controls.IMessageBus messageBus)
		{
			// TODO: Complete member initialization
			DataService = dataService;
			MessageBus = messageBus;
			InventoryLocations = DataService.InventoryLocationTypes.Execute();
			_masterInventory = MasterInventory.CreateMasterInventory(0, "New Inventory", 0, 0, null, 0, 0, true);
		}

		public Boolean IsVisible
		{
			get
			{
				return _isVisible;
			}
			set
			{
				_isVisible = value;
				NotifyPropertyChanged("IsVisible");
			}
		}

		public MasterInventory Item
		{
			get
			{
				return _masterInventory;
			}
			set
			{
				_masterInventory = value;
				NotifyPropertyChanged("Item");
			}
		}

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

		internal void SaveInventory()
		{

			if (_masterInventory.Id > 0)
			{
				DataService.UpdateObject(_masterInventory);
				DataService.SaveChanges();
			}
			else
			{
				DataService.AddToMasterInventories(_masterInventory);
				DataService.SaveChanges();
			}
			IsVisible = false;

		}

		internal void CancelAdd()
		{
			IsVisible = false;
		}
	}
}

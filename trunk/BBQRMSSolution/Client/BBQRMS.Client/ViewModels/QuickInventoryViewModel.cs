using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Windows.Threading;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class QuickInventoryViewModel : ViewModelBase
	{
		private IEnumerable<QuickInventoryLocationViewModel> _locations;
		private QuickInventoryLocationViewModel _selectedLocation;
		private bool _isWorking;
		private DelegateCommand _removeInventoryCommand;


		[Obsolete("Used for design-time only", true)]
		public QuickInventoryViewModel()
		{
			Locations =
				new List<QuickInventoryLocationViewModel>
					{
						new QuickInventoryLocationViewModel
							{
								Description = "Fridge",
								Items =
									new List<QuickInventoryItemViewModel>
										{
											new QuickInventoryItemViewModel
												{
													ItemName = "Ribs",
													UnitOfMeasure = "lbs.",
													Number = 1
												},
											new QuickInventoryItemViewModel
												{
													ItemName = "Chicken",
													UnitOfMeasure = "lbs.",
													Number = 1,
													ExcludeFromDailyShopping = true
												},
											new QuickInventoryItemViewModel
												{
													ItemName = "Sauce",
													UnitOfMeasure = "bottles",
													Number = 1
												},
										}
							},
						new QuickInventoryLocationViewModel
							{
								Description = "Freezer"
							},
						new QuickInventoryLocationViewModel
							{
								Description = "Pantry"
							},
					};
			SelectedLocation = Locations.First();
		}

		public QuickInventoryViewModel(BBQRMSEntities dataService, IMessageBus messageBus, ISecurityContext context)
		{
			DataService = dataService;
			MessageBus = messageBus;
			SecurityContext = context;

			var oldOption = DataService.MergeOption;
			DataService.MergeOption = MergeOption.NoTracking;
			var locationRecords = DataService.InventoryLocationTypes.Expand("MasterInventories").Execute();
			DataService.MergeOption = oldOption;

			Locations =
				(
					from rec in locationRecords
					select
						new QuickInventoryLocationViewModel
							{
								Description = rec.Description,
								Items =
									(
										from mi in rec.MasterInventories
										select
											new QuickInventoryItemViewModel
												{
													ItemName = mi.Name,
													Number = 1,
													UnitOfMeasure = mi.UnitOfMeasure,
													MasterInventoryId = mi.Id
												}
									).ToList()
							}
				).ToList();
			SelectedLocation = Locations.FirstOrDefault();
		}


		public IEnumerable<QuickInventoryLocationViewModel> Locations
		{
			get { return _locations; }
			set { _locations = value; NotifyPropertyChanged("Locations"); }
		}

		public QuickInventoryLocationViewModel SelectedLocation
		{
			get { return _selectedLocation; }
			set { _selectedLocation = value; NotifyPropertyChanged("SelectedLocation"); }
		}


		public DelegateCommand RemoveInventoryCommand
		{
			get { return _removeInventoryCommand ?? (_removeInventoryCommand = new DelegateCommand(RemoveInventory, CanRemoveInventory)); }
		}

		public void RemoveInventory(object parameter)
		{
			var item = (QuickInventoryItemViewModel)parameter;
			// Start animation that indicates the inventory is being updated
			IsWorking = true;
			DataService.AddToConsumedInventories(
				new ConsumedInventory
					{
						ConsumptionTypeId = ConsumptionTypes.Used,
						DateConsumed = DateTime.Now,
						EmployeeId = SecurityContext.CurrentUser.Id,
						ExcludeFromDailyShopping = item.ExcludeFromDailyShopping,
						Quantity = item.Number,
						MasterInventoryId = item.MasterInventoryId
					}
				);
			var masterInventory = DataService.MasterInventories.Where(mi => mi.Id == item.MasterInventoryId).ToList().First();
			masterInventory.UnitQty -= item.Number;
			DataService.UpdateObject(masterInventory);

			DispatcherTimer timer = null;
			EventHandler callback =
				(sender, args) =>
				{
					IsWorking = false;
					timer.Stop();
					RemoveInventoryCommand.RaiseCanExecuteChanged();
				};
			timer = new DispatcherTimer(TimeSpan.FromSeconds(.75d), DispatcherPriority.Normal, callback, Dispatcher.CurrentDispatcher);
			timer.Start();
			item.Reset();
			//TODO: run this on a background thread while the 'working' animation displays
			//TODO: catch exceptions.
			DataService.SaveChanges(SaveChangesOptions.Batch);
		}
		private bool CanRemoveInventory(object param)
		{
			return !IsWorking;
		}

		public bool IsWorking
		{
			get { return _isWorking; }
			set
			{
				if (value != _isWorking)
				{
					_isWorking = value;
					NotifyPropertyChanged("IsWorking");
				}
			}
		}

	}

	public class QuickInventoryLocationViewModel : ViewModelBase
	{
		private string _description;
		private IEnumerable<QuickInventoryItemViewModel> _items;

		public string Description
		{
			get { return _description; }
			set
			{
				if(value != _description)
				{
					_description = value;
					NotifyPropertyChanged("Description");
				}
			}
		}

		public IEnumerable<QuickInventoryItemViewModel> Items
		{
			get { return _items; }
			set
			{
				if(value != _items)
				{
					_items = value;
					NotifyPropertyChanged("Items");
				}
			}
		}


	}

	public class QuickInventoryItemViewModel : ViewModelBase
	{
		private DelegateCommand _decrementCommand;
		private DelegateCommand _incrementCommand;
		private string _itemName;
		private int _number;
		private string _unitOfMeasure;
		private bool _excludeFromDailyShopping;

		public int Number
		{
			get { return _number; }
			set
			{
				if(value != _number)
				{
					_number = value;
					NotifyPropertyChanged("Number");
				}
			}
		}

		public string ItemName
		{
			get { return _itemName; }
			set
			{
				if(value != _itemName)
				{
					_itemName = value;
					NotifyPropertyChanged("ItemName");
				}
			}
		}

		public string UnitOfMeasure
		{
			get { return _unitOfMeasure; }
			set
			{
				if(value != _unitOfMeasure)
				{
					_unitOfMeasure = value;
					NotifyPropertyChanged("UnitOfMeasure");
				}
			}
		}

		public bool ExcludeFromDailyShopping
		{
			get { return _excludeFromDailyShopping; }
			set
			{
				if(value != _excludeFromDailyShopping)
				{
					_excludeFromDailyShopping = value;
					NotifyPropertyChanged("ExcludeFromDailyShopping");
				}
			}
		}

		public int MasterInventoryId { get; set; }
		
		public DelegateCommand DecrementCommand
		{
			get { return _decrementCommand ?? (_decrementCommand = new DelegateCommand(Decrement)); }
		}
		public void Decrement()
		{
			if(Number > 1)
				Number -= 1;
		}

		public DelegateCommand IncrementCommand
		{
			get { return _incrementCommand ?? (_incrementCommand = new DelegateCommand(Increment)); }
		}
		public void Increment()
		{
			Number += 1;
		}

		public void Reset()
		{
			Number = 1;
			ExcludeFromDailyShopping = false;
		}
	}
}
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace BBQRMSSolution.ViewModels
{
	public class QuickInventoryViewModel : ViewModelBase
	{
		public QuickInventoryViewModel()
		{
			Locations =
				new ObservableCollection<InventoryLocation>
					{
						new InventoryLocation
							{
								Name = "Pantry",
								Items =
									{
										new QuickInventoryItem {Name = "Beans (#10 can)"},
										new QuickInventoryItem {Name = "Molasses (16 oz. bottle)"},
										new QuickInventoryItem {Name = "Catsup (bottle)"},
									}
							},
						new InventoryLocation
							{
								Name = "Fridge",
								Items =
									{
										new QuickInventoryItem {Name = "Beer (can)"},
										new QuickInventoryItem {Name = "Cabbage (head)"},
										new QuickInventoryItem {Name = "Carrots (5# bag)"},
										new QuickInventoryItem {Name = "Cob Corn (dozen)"},
									}
							},
						new InventoryLocation
							{
								Name = "Freezer",
								Items =
									{
										new QuickInventoryItem {Name = "Ribs (pounds)"},
										new QuickInventoryItem {Name = "Chicken (pounds)"},
										new QuickInventoryItem {Name = "Brisket (pounds)"},
										new QuickInventoryItem {Name = "Pork (pounds)"},
									}
							},
					};

			RemoveInventoryCommand = new DelegateCommand(HandleRemovedInventoryCommand, CanRemoveInventory);
		}


		public ObservableCollection<InventoryLocation> Locations { get; private set; }

		public DelegateCommand RemoveInventoryCommand { get; private set; }

		public void HandleRemovedInventoryCommand(object parameter)
		{
			var item = (QuickInventoryItem) parameter;
			// Start animation that indicates the inventory is being updated
			IsWorking = true;
			DispatcherTimer timer = null;
			EventHandler callback =
				(sender, args) =>
					{
						IsWorking = false;
						timer.Stop();
						RemoveInventoryCommand.RaiseCanExecuteChanged();
					};
			timer = new DispatcherTimer(TimeSpan.FromSeconds(1.25d), DispatcherPriority.Normal, callback, Dispatcher.CurrentDispatcher);
			timer.Start();
			item.Reset();
		}

		private bool CanRemoveInventory(object param)
		{
			return !IsWorking;
		}

		private bool mIsWorking;

		public bool IsWorking
		{
			get { return mIsWorking; }
			set
			{
				if (value != mIsWorking)
				{
					mIsWorking = value;
					NotifyPropertyChanged("IsWorking");
				}
			}
		}

	}


	public class InventoryLocation : ViewModelBase
	{
		public InventoryLocation()
		{
			Items = new ObservableCollection<QuickInventoryItem>();
		}

		public ObservableCollection<QuickInventoryItem> Items { get; private set; }

		public string Name { get; set; }
	}

	public class QuickInventoryItem : ViewModelBase
	{
		public QuickInventoryItem()
		{
			IncrementCommand = new DelegateCommand(HandleIncrement);
			DecrementCommand = new DelegateCommand(HandleDecrement, CanDecrement);
			Number = 1;
		}

		public void Reset()
		{
			Number = 1;
		}

		public string Name { get; set; }

		public DelegateCommand IncrementCommand { get; private set; }
		public DelegateCommand DecrementCommand { get; private set; }

		private void HandleIncrement()
		{
			Number += 1;
		}
		private void HandleDecrement()
		{
			Number -= 1;
		}
		private bool CanDecrement()
		{
			return Number > 1;
		}


		private int mNumber;
		public int Number
		{
			get { return mNumber; }
			private set
			{
				if (value != mNumber)
				{
					mNumber = value;
					NotifyPropertyChanged("Number");
				}
			}
		}
	}
}
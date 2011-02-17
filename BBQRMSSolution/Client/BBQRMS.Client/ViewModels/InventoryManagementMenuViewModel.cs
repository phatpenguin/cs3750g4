using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controls;
using System.Collections.ObjectModel;
using BBQRMSSolution.ServerProxy;


namespace BBQRMSSolution.ViewModels
{
	public class InventoryManagementMenuViewModel : ViewModelBase
	{

		private ViewModelBase _content;

		[Obsolete("Used for design-time only", true)]
		public InventoryManagementMenuViewModel()
		{
			
		}

		public InventoryManagementMenuViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
		{
			DataService = dataService;
			MessageBus = messageBus;
		}


		public ViewModelBase Content
		{
			get { return _content; }
			set
			{
				if (value != _content)
				{
					_content = value;
					NotifyPropertyChanged("Content");
				}
			}
		}


		public void HandleAddSupplier()
		{
			Content = new SupplierDetailViewModel(DataService, MessageBus);
		}
		public void HandleReceiveInventory()
		{
            Content = new ReceiveInventoryViewModel(DataService, MessageBus);

		}
		public void HandleReconcileInventory()
		{


		}
	}


}

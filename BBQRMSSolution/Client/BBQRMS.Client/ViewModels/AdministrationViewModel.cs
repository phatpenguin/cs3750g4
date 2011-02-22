using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class AdministrationViewModel : ViewModelBase, IHandle<ShowAdminScreen>
	{
		private ViewModelBase _content;

		[Obsolete("Used only for design-time only", true)]
		public AdministrationViewModel()
		{
			
		}

		public AdministrationViewModel(BBQRMSEntities dataservice, IMessageBus messageBus, ISecurityContext securityContext)
		{
			DataService = dataservice;
			MessageBus = messageBus;
			SecurityContext = securityContext;
            MessageBus.Subscribe(this);
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

		public void HandleChangePIN()
		{
            MessageBus.Publish(new ShowAdminScreen(new ChangePINViewModel(DataService, MessageBus, SecurityContext)));
		}

        public void HandleManageEmployees()
        {
            MessageBus.Publish(new ShowAdminScreen(new EmployeeManagementViewModel(DataService)));
        }

        public void HandleManageRoles()
        {
            MessageBus.Publish(new ShowAdminScreen(new RoleManagementViewModel(DataService)));
        }

        public void HandleManageMenus()
		{
            MessageBus.Publish(new ShowAdminScreen(new MenuManagementViewModel(DataService)));
		}
		public void HandleManageInventory()
		{
			//TODO: Show a new or existing viewmodel for managing inventory.
            MessageBus.Publish(new ShowAdminScreen(new InventoryManagementMenuViewModel(DataService, MessageBus)));
		}

        void IHandle<ShowAdminScreen>.Handle(ShowAdminScreen message)
        {
            Content = message.ViewModelToShow;
        }


	}
}

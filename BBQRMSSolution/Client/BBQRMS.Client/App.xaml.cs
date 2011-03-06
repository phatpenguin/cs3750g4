using System;
using System.Windows;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.Messages;
using BBQRMSSolution.Properties;
using BBQRMSSolution.ViewModels;
using BBQRMSSolution.Views;
using Controls;

namespace BBQRMSSolution
{
	public partial class App : Application, IHandle<Shutdown>
	{

		void IHandle<Shutdown>.Handle(Shutdown message)
		{
			GlobalApplicationState.ShuttingDown = true;
			Shutdown(0);
		}

		public ViewModelBase GetMainWindowViewModel()
		{
			var securityContext = new SecurityContext();

			IMessageBus messageBus = GlobalApplicationState.MessageBus;
			messageBus.Subscribe(this);
			messageBus.Subscribe(securityContext);
			return
				new MainWindowViewModel(
					new Uri(Settings.Default.dataServiceBaseUri),
					messageBus,
					securityContext,
					TimeProvider.Current,
					new POSDeviceManager(messageBus)
					);
		}
	}
}
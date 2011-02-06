using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;
using BBQRMSSolution.ViewModels.Messages;
using Controls;

namespace BBQRMSSolution
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application, IHandle<Shutdown>
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			GlobalApplicationState.MessageBus.Subscribe(this);
		}

		public void Handle(Shutdown message)
		{
			GlobalApplicationState.ShuttingDown = true;
			Shutdown(0);
		}
	}
}
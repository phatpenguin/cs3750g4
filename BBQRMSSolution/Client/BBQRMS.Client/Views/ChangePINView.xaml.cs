using System;
using System.Windows;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ViewModels;
using Controls;

namespace BBQRMSSolution.Views
{
	public partial class ChangePINView : UserControlBase<ChangePINViewModel>, IHandle<InvalidPinEntered>
	{
		public ChangePINView()
		{
			InitializeComponent();
			GlobalApplicationState.MessageBus.Subscribe(this);
			Loaded += ChangePINView_Loaded;
		}

		void ChangePINView_Loaded(object sender, RoutedEventArgs e)
		{
			pinEntry.Focus();
		}

		private void pinEntry_PinEntered(object sender, EventArgs args)
		{
			ViewModel.AcceptPin();
		}

		void IHandle<InvalidPinEntered>.Handle(InvalidPinEntered message)
		{
				pinEntry.ShowError(message.ErrorMessage);
		}
	}
}

using System;
using System.Windows;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ViewModels;
using Controls;

namespace BBQRMSSolution.Views
{
	/// <summary>
	/// Interaction logic for LoginView.xaml
	/// </summary>
	public partial class LoginView : UserControlBase<LoginViewModel>, IHandle<InvalidPinEntered>
	{

		public LoginView()
		{
			InitializeComponent();
			Loaded += LoginView_Loaded;
			GlobalApplicationState.MessageBus.Subscribe(this);
		}

		void LoginView_Loaded(object sender, RoutedEventArgs e)
		{
			pinEntry.Focus();
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.Exit();
		}

		void IHandle<InvalidPinEntered>.Handle(InvalidPinEntered message)
		{
			pinEntry.ShowError(message.ErrorMessage);
			pinEntry.Cancel();
		}

		private void PINEntry_OnPinEntered(object sender, EventArgs args)
		{
			ViewModel.HandleLogin(pinEntry.PIN);
		}
	}
}

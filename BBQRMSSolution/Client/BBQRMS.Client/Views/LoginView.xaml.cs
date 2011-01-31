using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using BBQRMSSolution.ViewModels;
using Controls;

namespace BBQRMSSolution.Views
{
	/// <summary>
	/// Interaction logic for LoginView.xaml
	/// </summary>
	public partial class LoginView : UserControl, IHandle<InvalidPinEntered>
	{
		private Storyboard mErrorFadeInAnimation;

		public LoginView()
		{
			InitializeComponent();
			Loaded += new RoutedEventHandler(LoginView_Loaded);
			GlobalApplicationState.MessageBus.Subscribe(this);
			mErrorFadeInAnimation = (Storyboard)this.FindResource("fadeIn");

		}

		void LoginView_Loaded(object sender, RoutedEventArgs e)
		{
			passwordBox.Focus();
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			LoginViewModel.Exit();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Button pressed = (Button) sender;
			passwordBox.Password += pressed.CommandParameter;
		}

		private void cancelButton_Click(object sender, RoutedEventArgs e)
		{
			passwordBox.Password = "";
			passwordBox.Focus();
		}

		private void enterButton_Click(object sender, RoutedEventArgs e)
		{
			LoginViewModel.HandleLogin(passwordBox.Password);
		}

		private LoginViewModel LoginViewModel
		{
			get { return ((LoginViewModel) DataContext); }
		}

		public void Handle(InvalidPinEntered message)
		{
			//clear their old entry
			passwordBox.Password = "";
			//set focus back into the password box (it's probably on the enter button now)
			passwordBox.Focus();
			//display an error message for a short time.
			errorText.BeginStoryboard(mErrorFadeInAnimation);
		}
	}
}

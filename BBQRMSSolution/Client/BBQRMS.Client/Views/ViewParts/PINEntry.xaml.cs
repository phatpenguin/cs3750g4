using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace BBQRMSSolution.Views.ViewParts
{
	public partial class PINEntry : UserControl
	{
		private readonly Storyboard mErrorFadeInAnimation;

		public PINEntry()
		{
			InitializeComponent();
			mErrorFadeInAnimation = (Storyboard)FindResource("fadeIn");
		}

		public static readonly DependencyProperty PINProperty =
				DependencyProperty.Register("PIN", typeof(string), typeof(PINEntry), new UIPropertyMetadata(null, HandlePINChanged));
		public static readonly DependencyProperty PromptProperty =
				DependencyProperty.Register("Prompt", typeof(string), typeof(PINEntry), new UIPropertyMetadata("Enter your PIN."));

		public string PIN
		{
			get { return (string)GetValue(PINProperty); }
			set { SetValue(PINProperty, value); }
		}

		private static void HandlePINChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			var me = (PINEntry)dependencyObject;
			if (dependencyPropertyChangedEventArgs.NewValue is string)
				me.passwordBox.Password = (string)dependencyPropertyChangedEventArgs.NewValue;
		}

		public string Prompt
		{
			get { return (string)GetValue(PromptProperty); }
			set { SetValue(PromptProperty, value); }
		}


		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var pressed = (Button)sender;
			passwordBox.Password += pressed.CommandParameter;
		}

		private void cancelButton_Click(object sender, RoutedEventArgs e)
		{
			Cancel();
		}

		private void enterButton_Click(object sender, RoutedEventArgs e)
		{
			PIN = passwordBox.Password;
			OnPinEntered();
		}

		public new void Focus()
		{
			passwordBox.Focus();
		}

		public event EventHandler PinEntered;

		private void OnPinEntered()
		{
			if(PinEntered != null)
				PinEntered(this, EventArgs.Empty);
		}

		public void ShowError(string errorMessage)
		{
			errorText.Text = errorMessage;
			errorText.BeginStoryboard(mErrorFadeInAnimation);
		}

		public void Cancel()
		{
			passwordBox.Password = "";
			passwordBox.Focus();
		}
	}
}

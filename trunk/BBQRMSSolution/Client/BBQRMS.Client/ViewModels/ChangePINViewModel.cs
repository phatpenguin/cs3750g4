using System;
using System.Data.Services.Client;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.Messages;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class ChangePINViewModel : ViewModelBase
	{
		[Obsolete("Used for design-time only", true)]
		public ChangePINViewModel()
		{
		}

		private string _newPin;
		private string _oldIdPart;
		private string _enteredPin;
		private bool _success;
		private string _prompt = "Enter your old PIN.";
		private int _currentStep = 1;
		private ApplicationUser _applicationUser;

		public ChangePINViewModel(BBQRMSEntities dataService, IMessageBus messageBus, ISecurityContext securityContext)
		{
			DataService = dataService;
			MessageBus = messageBus;
			SecurityContext = securityContext;
		}

		public string Prompt
		{
			get { return _prompt; }
			set
			{
				if(value != _prompt)
				{
					_prompt = value;
					NotifyPropertyChanged("Prompt");
				}
			}
		}

		public string EnteredPIN
		{
			get { return _enteredPin; }
			set
			{
				if (value != _enteredPin)
				{
					_enteredPin = value;
					NotifyPropertyChanged("EnteredPIN");
				}
			}
		}

		public void AcceptPin()
		{
			switch(_currentStep)
			{
				case 1:
					CheckOldPIN();
					break;
				case 2:
					CheckNewPIN();
					break;
				case 3:
					ConfirmNewPIN();
					break;
			}
		}

		private void CheckOldPIN()
		{
			//TODO: the call to CheckPIN should happen on a background thread with a short-ish timeout so we don't block the UI.
			_applicationUser = LoginViewModel.CheckPIN(DataService, EnteredPIN);
			if (_applicationUser == null || _applicationUser.Employee != SecurityContext.CurrentUser)
			{
				MessageBus.Publish(new InvalidPinEntered("Invalid PIN Entered"));
			}
			else
			{
				_oldIdPart = EnteredPIN.Split('0')[0];
				_currentStep = 2;
				Prompt = "Enter your new PIN starting with '" + _oldIdPart + "0'";
				EnteredPIN = "";
			}
		}

		private void CheckNewPIN()
		{
			if (!EnteredPIN.StartsWith(_oldIdPart + "0"))
			{
				EnteredPIN = "";
				MessageBus.Publish(new InvalidPinEntered("New PIN must start with '" + _oldIdPart + "0'"));
				return;
			}
			if(EnteredPIN.Length < _oldIdPart.Length + 3)
			{
				EnteredPIN = "";
				MessageBus.Publish(new InvalidPinEntered("PIN must be at least " + (_oldIdPart.Length + 3) + " digits long."));
				return;
			}
			_newPin = EnteredPIN;
			_currentStep = 3;
			Prompt = "Confirm your new PIN.";
			EnteredPIN = "";
		}

		private void ConfirmNewPIN()
		{
			if(_newPin == EnteredPIN)
			{
				_applicationUser.PersonalPart = EnteredPIN.Split(new[] {'0'}, 2)[1];
				// actually save the change.
				DataService.UpdateObject(_applicationUser);
				//TODO: The call to SaveChanges should happen on a background thread with a short-ish timeout so we don't block the UI.
				DataService.SaveChanges(SaveChangesOptions.Batch);
				Success = true;
			}
			else
			{
				_currentStep = 2;
				Prompt = "Enter your new PIN starting with '" + _oldIdPart + "0'";
				EnteredPIN = "";
				MessageBus.Publish(new InvalidPinEntered("PINs didn't match. Enter new PIN."));
			}
		}

		public bool Success
		{
			get { return _success; }
			set
			{
				if(value != _success)
				{
					_success = value;
					NotifyPropertyChanged("Success");
				}
			}
		}
	}
}
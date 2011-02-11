using System;
using System.Windows.Input;

namespace Controls
{
	public class DelegateCommand : ICommand
	{
		private readonly Action<object> mExecuteCallback;
		private readonly Func<object, bool> mCanExecuteCallback;

		public DelegateCommand(Action executeCallback) : this(o => executeCallback(), o => true)
		{
		}

		public DelegateCommand(Action<object> executeCallback) : this(executeCallback, o => true)
		{
		}

		public DelegateCommand(Action executeCallback, Func<bool> canExecuteCallback) : this(o => executeCallback(), o => canExecuteCallback())
		{
		}

		public DelegateCommand(Action<object> executeCallback, Func<object, bool> canExecuteCallback)
		{
			mExecuteCallback = executeCallback;
			mCanExecuteCallback = canExecuteCallback;
		}

		public void Execute(object parameter)
		{
			mExecuteCallback(parameter);
		}

		public bool CanExecute(object parameter)
		{
			return mCanExecuteCallback(parameter);
		}

		private EventHandler mCanExecuteChangedDelegate;

		public event EventHandler CanExecuteChanged
		{
			add
			{
				mCanExecuteChangedDelegate += value;
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
				mCanExecuteChangedDelegate -= value;
			}
		}

	
		public void RaiseCanExecuteChanged()
		{
			EventHandler handler = mCanExecuteChangedDelegate;
			if(handler != null)
				handler(this, EventArgs.Empty);
		}
	}
}
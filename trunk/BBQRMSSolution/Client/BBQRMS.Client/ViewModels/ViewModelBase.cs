using System.ComponentModel;
using System.Collections.ObjectModel;
using BBQRMSSolution.Models;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string propName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if(handler != null)
				handler(this, new PropertyChangedEventArgs(propName));
		}

	    public SecurityContext SecurityContext {
            get { return GlobalApplicationState.SecurityContext; }
	    }

	    public IMessageBus MessageBus {
            get { return GlobalApplicationState.MessageBus; }
	    }
	}
}
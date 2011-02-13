using System.ComponentModel;
using BBQRMSSolution.BusinessLogic;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string propName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propName));
		}

		public ISecurityContext SecurityContext { get; protected set; }

		public IMessageBus MessageBus { get; protected set; }

		public BBQRMSEntities DataService { get; protected set; }
	}
}
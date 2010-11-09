using System.ComponentModel;

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
	}
}
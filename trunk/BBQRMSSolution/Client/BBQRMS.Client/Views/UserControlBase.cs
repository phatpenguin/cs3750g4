using System.ComponentModel;
using System.Windows.Controls;

namespace BBQRMSSolution.Views
{
	public class UserControlBase<T> : UserControl
		where T : INotifyPropertyChanged
	{
		public T ViewModel
		{
			get { return (T)DataContext; }
		}
	}
}

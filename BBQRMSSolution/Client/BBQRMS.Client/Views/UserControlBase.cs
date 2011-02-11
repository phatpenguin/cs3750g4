using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution.Views
{
    public abstract class UserControlBase<T> where T : ViewModelBase
    {
        private T viewModel;
        public T ViewModel {
            get { return viewModel ?? (viewModel = (viewModel)); }
        }
    }

}

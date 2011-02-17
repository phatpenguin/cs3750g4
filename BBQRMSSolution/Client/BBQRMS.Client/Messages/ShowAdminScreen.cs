using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution.Messages
{
    public class ShowAdminScreen
    {
        public ShowAdminScreen(ViewModelBase viewModel)
        {
            ViewModelToShow = viewModel;
        }

        public ViewModelBase ViewModelToShow { get; private set; }
    }
}

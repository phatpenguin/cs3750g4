using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	class PaymentViewModel : ViewModelBase
	{
        public ObservableCollection<PaymentType> PaymentTypes { get; set; }

        public PaymentViewModel()
        {
            PaymentTypes = new ObservableCollection<PaymentType>
                               {
                                   new PaymentType {Description = "Cash"},
                                   new PaymentType {Description = "Credit"},
                                   new PaymentType {Description = "Check"}
                               };
        }

        public PaymentViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
        {
            PaymentTypes = new ObservableCollection<PaymentType>(DataService.PaymentTypes.Execute());
        }

	}
}

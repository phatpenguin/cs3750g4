using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBQRMSSolution.ServerProxy;
using System.Collections.ObjectModel;
using Controls;

namespace BBQRMSSolution.ViewModels
{
    public class SupplierDetailViewModel:ViewModelBase
    {
        private readonly IMessageBus _MessageBus;
        private BBQRMSEntities _DataService;
        private ObservableCollection<Supplier> _Supplier;

        public SupplierDetailViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
        {
            _DataService = dataService;
            Suppliers = new ObservableCollection<Supplier>(_DataService.Suppliers);
            
        }
        public ObservableCollection<Supplier> Suppliers
        {
            get
            {
                return _Supplier;
            }
            set
            {
                _Supplier = value; NotifyPropertyChanged("Suppliers");
            }

        }

    }
}

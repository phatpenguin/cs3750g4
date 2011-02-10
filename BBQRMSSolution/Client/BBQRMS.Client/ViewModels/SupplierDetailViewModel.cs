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
        private Supplier _SelectedSupplier;

        //constructor
        public SupplierDetailViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
        {
            _DataService = dataService;
            Suppliers = new ObservableCollection<Supplier>(_DataService.Suppliers);
            
        }

        //property --kinda like a method but also an attribute
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


        internal void addSupplier()
        {
            Supplier x = new Supplier();
            Suppliers.Add(x);
            SelectedSupplier = x;

       
        }


        public Supplier SelectedSupplier
        {
            get
            {
                return _SelectedSupplier;
            }
            set
            {
                _SelectedSupplier = value;
                NotifyPropertyChanged("SelectedSupplier");
            }
        }


        internal void saveSupplier()
        {
            // the ID of the supplier (if its new is 0) once its saved the id will be generated from the database sequence.
            if (_SelectedSupplier.Id > 0)
            {
                _DataService.UpdateObject(_SelectedSupplier);
            }
            else
            {

                _DataService.AddToSuppliers(_SelectedSupplier);
                _DataService.SaveChanges();
            }
        }
    }
}

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
        private ObservableCollection<Supplier> _supplier;
        private Supplier _selectedSupplier;

		[Obsolete("Used for design-time only", true)]
    	public SupplierDetailViewModel()
    	{
    		//TODO: fill in the properties with some simulated data for the VS designer
    	}

        //constructor
        public SupplierDetailViewModel(BBQRMSEntities dataService, IMessageBus messageBus)
        {
            DataService = dataService;
					MessageBus = messageBus;
            Suppliers = new ObservableCollection<Supplier>(DataService.Suppliers);
            
        }

        //property --kinda like a method but also an attribute
        public ObservableCollection<Supplier> Suppliers
        {
            get
            {
                return _supplier;
            }
            set
            {
                _supplier = value; NotifyPropertyChanged("Suppliers");
            }

        }


        internal void AddSupplier()
        {
            Supplier x = Supplier.CreateSupplier(0, "New Supplier", "", "");
            Suppliers.Add(x);
            SelectedSupplier = x;

        }


        public Supplier SelectedSupplier
        {
            get
            {
                return _selectedSupplier;
            }
            set
            {
                _selectedSupplier = value;
                NotifyPropertyChanged("SelectedSupplier");
            }
        }


        internal void SaveSupplier()
        {
            // the ID of the supplier (if its new is 0) once its saved the id will be generated from the database sequence.
            if (_selectedSupplier.Id > 0)
            {
                DataService.UpdateObject(_selectedSupplier);
								DataService.SaveChanges();
						}
            else
            {
                DataService.AddToSuppliers(_selectedSupplier);
                DataService.SaveChanges();
            }
        }

        internal void SaveItem()
        {
            throw new NotImplementedException();
        }

        internal void DeleteItem()
        {
            SaveSupplier();
            DataService.DeleteObject(_selectedSupplier);
            Suppliers.Remove(_selectedSupplier);
            DataService.SaveChanges();
            
        }

    }
}

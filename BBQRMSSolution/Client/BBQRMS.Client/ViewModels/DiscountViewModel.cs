using System;
using System.Collections.ObjectModel;
using System.Linq;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
    public class DiscountType
    {
        public int Id { get; set; }
        public String Description { get; set; }
    }

    public class DiscountViewModel : ViewModelBase
    {
        private OrderViewModel _orderViewModel;
        private string _discountVisible;
        private int _discountZIndex;
        private string _discountAmount = "0";
        private decimal _mDiscountAmount;

        public OrderViewModel Order { get { return _orderViewModel; } set { _orderViewModel = value; NotifyPropertyChanged("Order"); } }
        public string DiscountVisible { get { return _discountVisible; } set { _discountVisible = value; NotifyPropertyChanged("DiscountVisible"); } }
        public int DiscountZIndex { get { return _discountZIndex; } set { _discountZIndex = value; NotifyPropertyChanged("DiscountZIndex"); } }
        public DelegateCommand Cancel { get { return new DelegateCommand(CancelPayment); } }
        public DelegateCommand AddDiscount { get { return new DelegateCommand(DoAddDiscount); } }
        public ObservableCollection<DiscountType> DiscountTypes { get; set; }
        public Discount Discount { get; set; }
        public DiscountType DiscountType { get; set; }

        public DiscountViewModel()
        {

        }

        public string DiscountAmount { get { return _discountAmount; } set { _discountAmount = value; } }

        public DiscountViewModel(BBQRMSEntities dataService, IMessageBus messageBus, OrderViewModel order)
        {
            Order = order;
            DataService = dataService;
            MessageBus = messageBus;

            DiscountTypes = new ObservableCollection<DiscountType>
                                {
                                    new DiscountType {Id = 0, Description = "Amount"},
                                    new DiscountType {Id = 1, Description = "Percentage"}
                                };

            DiscountType = DiscountTypes[0];
            DiscountVisible = "Visible";
            DiscountZIndex = 200;
        }

        public void CancelPayment()
        {
            DiscountVisible = "Collapsed";
        }

        public void DoAddDiscount()
        {
            switch(DiscountType.Id)
            {
                case 0:
                    _mDiscountAmount = Convert.ToDecimal(DiscountAmount);
                    break;
                case 1:
                    _mDiscountAmount = (Convert.ToDecimal(DiscountAmount)/100) * Order.TotalPrice;
                    break;
            }

            Discount = new Discount { Amount = _mDiscountAmount, OrderId = Order.Order.Id, DiscountTypeId = DiscountType.Id, Id = 0 };
            var discountType = DiscountTypes.Where(x => x.Id == Discount.DiscountTypeId).FirstOrDefault();

            if(Order.AddDiscount(Discount)) DiscountVisible = "Collapsed";
        }
    }
}

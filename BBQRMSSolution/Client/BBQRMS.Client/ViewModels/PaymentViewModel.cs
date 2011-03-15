using System;
using System.Collections.ObjectModel;
using System.Linq;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class PaymentViewModel : ViewModelBase
	{
	    private OrderViewModel _orderViewModel;
	    private string _paymentVisible;
	    private CreditCardViewModel _creditViewModel;
	    private int _paymentZIndex;
	    private string _paymentAmount="0";

        public ObservableCollection<PaymentType> PaymentTypes { get; set; }
        public OrderViewModel Order { get { return _orderViewModel; } set { _orderViewModel = value; NotifyPropertyChanged("Order"); } }
        public string PaymentVisible { get { return _paymentVisible; } set { _paymentVisible = value; NotifyPropertyChanged("PaymentVisible"); } }
        public int PaymentZIndex { get { return _paymentZIndex; } set { _paymentZIndex = value; NotifyPropertyChanged("PaymentZIndex"); } }
        public DelegateCommand Cancel { get { return new DelegateCommand(CancelPayment); } }
        public DelegateCommand AddPayment { get { return new DelegateCommand(DoAddPayment); } }
        public Payment Payment { get; set; }
        public PaymentType PaymentType { get; set; }

        public CreditCardViewModel Credit { get { return _creditViewModel; } set { _creditViewModel = value; NotifyPropertyChanged("Order"); } }

        public PaymentViewModel()
        {
            PaymentTypes = new ObservableCollection<PaymentType>
                               {
                                   new PaymentType {Description = "Cash"},
                                   new PaymentType {Description = "Credit"},
                                   new PaymentType {Description = "Check"}
                               };
        }

        public string PaymentAmount { get { return _paymentAmount; } set { _paymentAmount = value; } }

        public PaymentViewModel(BBQRMSEntities dataService, IMessageBus messageBus, OrderViewModel order, IPOSDeviceManager posDeviceManager)
        {
            Order = order;
            DataService = dataService;
            MessageBus = messageBus;

			ICashDrawer cashDrawer = posDeviceManager.GetCashDrawer();
        	cashDrawer.OpenDrawer();

            PaymentTypes = new ObservableCollection<PaymentType>(DataService.PaymentTypes.Execute());
            PaymentType = PaymentTypes[0];
            PaymentVisible = "Visible";
            PaymentZIndex = 200;
        }

        public void CancelPayment()
        {
            PaymentVisible = "Collapsed";
        }

        public void DoAddPayment()
        {
            Payment = new Payment { Amount = Convert.ToDecimal(PaymentAmount), OrderId = Order.Order.Id, PaymentTypeId = PaymentType.Id, Id=0 };
            var paymentType = PaymentTypes.Where(x => x.Id == Payment.PaymentTypeId).FirstOrDefault();

            Order.AddPayment(Payment);

            PaymentVisible = "Collapsed";
        }

        public void NewCredit()
        {
            Credit = new CreditCardViewModel(DataService, MessageBus, this);
            NotifyPropertyChanged("Payment");
        }
	}
}

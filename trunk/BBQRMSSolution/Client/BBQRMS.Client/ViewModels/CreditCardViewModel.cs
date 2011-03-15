using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
    public class CreditCardViewModel : ViewModelBase
    {
        private PaymentViewModel _paymentViewModel;
	    private string _creditVisible;
	    private int _creditZIndex;

        public string CreditVisible { get { return _creditVisible; } set { _creditVisible = value; NotifyPropertyChanged("CreditVisible"); } }
        public int CreditZIndex { get { return _creditZIndex; } set { _creditZIndex = value; NotifyPropertyChanged("CreditZIndex"); } }
        public DelegateCommand Cancel { get { return new DelegateCommand(CancelCredit); } }
        public PaymentType PaymentType { get; set; }

        public PaymentViewModel Payment { get { return _paymentViewModel; } set { _paymentViewModel = value; NotifyPropertyChanged("Payment"); } }

        public CreditCardViewModel()
        {

        }

        public CreditCardViewModel(BBQRMSEntities dataService, IMessageBus messageBus, PaymentViewModel payment)
        {
            Payment = payment;

            DataService = dataService;
            MessageBus = messageBus;

            CreditVisible = "Visible";
            CreditZIndex = 300;
        }

        public void CancelCredit()
        {
            CreditVisible = "Collapsed";
        }
	}
}

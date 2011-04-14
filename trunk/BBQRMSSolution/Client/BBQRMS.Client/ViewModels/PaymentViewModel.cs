using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class PaymentViewModel : ViewModelBase
	{
	    private bool isCcIntegrationOn = true;
	    private OrderViewModel _orderViewModel;
	    private string _paymentVisible = "Collapsed";
	    private string _creditCardVisible = "Collapsed";

	    private readonly ICashDrawer _cashDrawer;

        public DelegateCommand CcCancelPayment { get { return new DelegateCommand(CcCancelPay); } }
        public DelegateCommand CcProcessPayment { get { return new DelegateCommand(CcProcessPay); } }

	    private int _paymentZIndex = 200;
        private int _creditCardZIndex = 300;

	    private string _paymentAmount="0";

        public ObservableCollection<PaymentType> PaymentTypes { get; set; }
        public OrderViewModel Order { get { return _orderViewModel; } set { _orderViewModel = value; NotifyPropertyChanged("Order"); } }

        public string PaymentVisible { get { return _paymentVisible; } set { _paymentVisible = value; NotifyPropertyChanged("PaymentVisible"); } }
        public string CcVisible { get { return _creditCardVisible; } set { _creditCardVisible = value; NotifyPropertyChanged("CcVisible"); } }

        public int PaymentZIndex { get { return _paymentZIndex; } set { _paymentZIndex = value; NotifyPropertyChanged("PaymentZIndex"); } }
        public int CcZIndex { get { return _creditCardZIndex; } set { _creditCardZIndex = value; NotifyPropertyChanged("CcZIndex"); } }
        
        public DelegateCommand Cancel { get { return new DelegateCommand(CancelPayment); } }
        public DelegateCommand AddPayment { get { return new DelegateCommand(DoAddPayment); } }

        public Payment Payment { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Memo { get; set; }

	    public string CcFirstName { get; set; }
	    public string CcLastName { get; set; }
	    public string CcCardNumber { get; set; }
        public string CcExpMonth { get; set; }
        public string CcExpYear { get; set; }

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

			_cashDrawer = posDeviceManager.GetCashDrawer();

            PaymentTypes = new ObservableCollection<PaymentType>(DataService.PaymentTypes.Execute());
            PaymentType = PaymentTypes.Where(x => x.Id == ServerProxy.PaymentTypes.Cash).FirstOrDefault();
            PaymentVisible = "Visible";
            PaymentZIndex = 200;
        }

        public void CancelPayment()
        {
            PaymentVisible = "Collapsed";
        }

        public void CcCancelPay()
        {
            CcVisible = "Collapsed";
            PaymentVisible = "Collapsed";
        }

        public void DoAddPayment()
        {
            Payment = new Payment { Amount = Convert.ToDecimal(PaymentAmount), OrderId = Order.Order.Id, PaymentTypeId = PaymentType.Id, Id = 0, Memo = Memo };

            if (Payment.Amount > Order.RemainingAmount && Payment.PaymentTypeId != ServerProxy.PaymentTypes.Cash)
            {
                MessageBox.Show(@"You cannot pay more than the amount due with this method of payment.", @"Confirmation");
            }
            else
            {
                if (PaymentType.IsCreditCard && isCcIntegrationOn)
                {
                    //TODO uncomment to get msr working with wpf
                    //CcZIndex = 300;
                    //CcVisible = "Visible";

                    //For now just use a winform. :(
                    var ccForm = new CcForm();
                    var result = ccForm.ShowDialog();
                    if (result == DialogResult.OK) CcProcessPay();
                    else MessageBox.Show(@"Card Declined.", @"Confirmation");
                }
                else
                {
                    CcAddPayment();
                }
            }
        }

        private void CcAddPayment()
        {
            Order.AddPayment(Payment);

            if(Payment.PaymentTypeId == ServerProxy.PaymentTypes.CreditCard) CcVisible = "Collapsed";
            PaymentVisible = "Collapsed";
            _cashDrawer.OpenDrawer();
        }

        private void CcProcessPay()
        {
            //TODO add logic to send credit card data to processor webservice or payment API
            //TODO if transaction approved CcAddPayment();
            CcAddPayment();
        }
	}
}

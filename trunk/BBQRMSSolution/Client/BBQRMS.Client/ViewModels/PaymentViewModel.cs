using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AxKbdWedgeOCX;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class PaymentViewModel : ViewModelBase
	{
	    private OrderViewModel _orderViewModel;
	    private string _paymentVisible = "Collapsed";
	    private string _creditCardVisible = "Collapsed";

        public DelegateCommand CcCancelPayment { get { return new DelegateCommand(CcCancelPay); } }
        public DelegateCommand CcProcessPayment { get { return new DelegateCommand(CcProcessPay); } }

	    private int _paymentZIndex = 200;
        private int _creditCardZIndex = 300;

	    private string _paymentAmount="0";

        private readonly AxKbdWedge _kbdWedge;

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

			ICashDrawer cashDrawer = posDeviceManager.GetCashDrawer();
        	cashDrawer.OpenDrawer();

            PaymentTypes = new ObservableCollection<PaymentType>(DataService.PaymentTypes.Execute());
            PaymentType = PaymentTypes[0];
            PaymentVisible = "Visible";
            PaymentZIndex = 200;

            _kbdWedge = new AxKbdWedge();
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
            if (PaymentType.IsCreditCard)
            {
                CcZIndex = 300;
                CcVisible = "Visible";

                //_kbdWedge.Enabled = true;
                //_kbdWedge.CardDataChanged += Card_Swiped;
                //_kbdWedge.PortOpen = true;
            }
            else
            {
                Payment = new Payment { Amount = Convert.ToDecimal(PaymentAmount), OrderId = Order.Order.Id, PaymentTypeId = PaymentType.Id, Id = 0, Memo = Memo };
                Order.AddPayment(Payment);

                PaymentVisible = "Collapsed";
            }
        }

        private void CcProcessPay()
        {
            Payment = new Payment { Amount = Convert.ToDecimal(PaymentAmount), OrderId = Order.Order.Id, PaymentTypeId = PaymentType.Id, Id = 0, Memo = Memo };
            Order.AddPayment(Payment);

            CcVisible = "Collapsed";
            PaymentVisible = "Collapsed";
        }

        private void Card_Swiped(object sender,EventArgs e)
        {
            _kbdWedge.PortOpen = false;

            CcFirstName = _kbdWedge.GetFName();
            CcLastName = _kbdWedge.GetLName();

            CcCardNumber = _kbdWedge.FindElement(2, ";", 0, "=");
            CcExpMonth = _kbdWedge.FindElement(2, "=", 2, "2");
            CcExpYear = _kbdWedge.FindElement(2, "=", 0, "2");

            _kbdWedge.ClearBuffer();
            _kbdWedge.PortOpen = true;
            CcVisible = "Collapsed";

            Payment = new Payment { Amount = Convert.ToDecimal(PaymentAmount), OrderId = Order.Order.Id, PaymentTypeId = PaymentType.Id, Id = 0, Memo = Memo };
            Order.AddPayment(Payment);

            PaymentVisible = "Collapsed";
        }
	}
}

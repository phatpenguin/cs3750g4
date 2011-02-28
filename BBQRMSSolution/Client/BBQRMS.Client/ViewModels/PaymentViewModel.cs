﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BBQRMSSolution.ServerProxy;
using Controls;

namespace BBQRMSSolution.ViewModels
{
	public class PaymentViewModel : ViewModelBase
	{
	    private OrderViewModel _orderViewModel;
	    private string _paymentVisible;
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

        public PaymentViewModel(BBQRMSEntities dataService, IMessageBus messageBus, OrderViewModel order)
        {
            Order = order;
            DataService = dataService;
            MessageBus = messageBus;

            PaymentTypes = new ObservableCollection<PaymentType>(DataService.PaymentTypes.Execute());

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
            Order.AddPayment(Payment);

            PaymentVisible = "Collapsed";
        }
	}
}

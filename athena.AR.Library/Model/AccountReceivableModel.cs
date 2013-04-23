using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace athena.AR.Library.Model
{
    public class ReceivableModel
    {
        private decimal _totalAmount;
        private decimal _unitPrice;
        private int _qty;

        public int Id { get; set; }


        public CustomerModel Customer { get; set; }


        public SalesModel Sales { get; set; }

        public decimal PaymentDue { get { return _totalAmount; } set { _totalAmount = value; } }


        public DateTime PaymentDueDate { get; set; }


        public decimal Balance { get; set; }


        public DateTime LastPaidDate { get; set; }

       
    }
}
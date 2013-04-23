using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace athena.AR.Library.Model
{
    public class SalesModel
    {
        private decimal _totalAmount;
        private decimal _unitPrice;
        private int _qty;

        public int Id { get; set; }


        public string Item { get; set; }


        public decimal UnitPrice { get { return _unitPrice; } set { _unitPrice = value; SumTotal(); } }


        public int Qty { get { return _qty; } set { _qty = value; SumTotal(); } }

        public decimal TotalAmount { get { return _totalAmount; } set { _totalAmount = value; } }

        public CustomerModel Customer { get; set; }

        public DateTime SalesDate { get; set; }

        public int MonthsToPay { get; set; }

        public decimal MonthlyInterest { get; set; }

         private void SumTotal()
        {
            if (!(UnitPrice * Qty).Equals(TotalAmount))
                _totalAmount = UnitPrice * Qty;
        }
    }
}
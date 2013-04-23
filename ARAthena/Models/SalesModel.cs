using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace athena.AR.Models
{
    public class SalesModel
    {

        private decimal _totalAmount;
        private decimal _unitPrice;
        private int _qty;
     
        public SalesModel():this(null) { }

        public SalesModel(athena.AR.Library.Model.SalesModel model)
        {
            if (model != null)
            {
                Id = model.Id;
                Item = model.Item;
                Qty = model.Qty;
                UnitPrice = model.UnitPrice;
               // TotalAmount = model.TotalAmount;
                Customer = new CustomerModel(model.Customer);
                SalesDate = model.SalesDate;
                MonthlyInterest = model.MonthlyInterest;
                MonthsToPay = model.MonthsToPay;

            }
        }

        [Key]
        [Display(Name = "Sales Id")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cannot be empty")]
        [Display(Name = "Item Description")]
        public string Item { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get { return _unitPrice; } set { _unitPrice = value; SumTotal(); } }

        [Required]
        [Display(Name = "Quantity")]
        public int Qty { get { return _qty; } set { _qty = value; SumTotal(); } }

        [Display(Name = "Total Price")]
        public decimal TotalAmount { get { return _totalAmount; } set { _totalAmount = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cannot be empty")]
        [Display(Name = "Customer")]
        public CustomerModel Customer { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cannot be empty")]
        [Display(Name = "Sales Date")]
        [DataType(DataType.Date)]
        public DateTime SalesDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cannot be empty")]
        [Display(Name = "Months to Pay")]
        public int MonthsToPay { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cannot be empty")]
        [Display(Name = "Monthly interest")]
        public decimal MonthlyInterest { get; set; }

         private void SumTotal()
        {
            if (!(UnitPrice * Qty).Equals(TotalAmount))
                _totalAmount = UnitPrice * Qty;
        }
    }
}
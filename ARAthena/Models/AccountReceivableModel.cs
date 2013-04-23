using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace athena.AR.Models
{
    public class ReceivableModel
    {
        public ReceivableModel() :this(null){ }

        public ReceivableModel(athena.AR.Library.Model.ReceivableModel model)
        {
            if (model != null)
            {
                Id = model.Id;
                PaymentDue = model.PaymentDue;
                PaymentDueDate = model.PaymentDueDate;
                Balance = model.Balance;
                LastPaidDate = model.LastPaidDate;
                Sales = new Models.SalesModel(model.Sales);
                Customer = new CustomerModel(model.Customer);
            }
        }

        private decimal _totalAmount;
        private decimal _unitPrice;
        private int _qty;
        [Key]
        [Display(Name = "Receivable Id")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings=false, ErrorMessage="Cannot be empty")]
        [Display(Name="Customer")]
        public CustomerModel Customer { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cannot be empty")]
        [Display(Name = "Sales")]
        public SalesModel Sales { get; set; }

        [Display(Name = "Payment Due")]
        public decimal PaymentDue { get { return _totalAmount; } set { _totalAmount = value; } }

        [Required(AllowEmptyStrings=false)]
        [DataType(DataType.Date)]
        [Display(Name = "Payment Due Date")]
        public DateTime PaymentDueDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Running Balance")]
        public decimal Balance { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Last Paid Date ")]
        public DateTime LastPaidDate { get; set; }

       
    }
}
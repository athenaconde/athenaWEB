using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace athena.AR.Models
{
    public class PaymentModel
    {

        public PaymentModel() : this(null)
        { 
            
        }

        public PaymentModel(athena.AR.Library.Model.PaymentModel model)
        {
            if (model != null)
            {
                this.PayAmount = model.PayAmount;
                this.PaymentDate = model.PaymentDate;
                this.EncodedDate = model.EncodedDate;
                
                if (model.AccountReceivable != null)
                {
                    this.AccountReceivable = new Models.ReceivableModel()
                    {
                        Id = model.AccountReceivable.Id,
                        PaymentDue = model.AccountReceivable.PaymentDue,
                        PaymentDueDate = model.AccountReceivable.PaymentDueDate,
                        LastPaidDate = model.AccountReceivable.LastPaidDate,
                        Balance = model.AccountReceivable.Balance
                    };

                    if (model.AccountReceivable.Customer != null)
                        this.AccountReceivable.Customer = new Models.CustomerModel()
                        {
                             Id = model.AccountReceivable.Customer.Id,
                             Name = model.AccountReceivable.Customer.Name,
                             Address = model.AccountReceivable.Customer.Address,
                             Phone = model.AccountReceivable.Customer.Phone,
                             Email = model.AccountReceivable.Customer.Email,
                             Active = model .AccountReceivable.Customer.Active,
                             DateCreated = model.AccountReceivable.Customer.DateCreated,
                             LastDateModified = model.AccountReceivable.Customer.LastDateModified
                        };

                    if (model.AccountReceivable.Sales != null)
                        this.AccountReceivable.Sales = new Models.SalesModel() 
                        { 
                            Id = model.AccountReceivable.Sales.Id,
                            Item = model.AccountReceivable.Sales.Item,
                            Qty = model.AccountReceivable.Sales.Qty,
                            UnitPrice = model.AccountReceivable.Sales.UnitPrice,
                            TotalAmount = model.AccountReceivable.Sales.TotalAmount,
                            SalesDate = model.AccountReceivable.Sales.SalesDate,
                            MonthsToPay = model.AccountReceivable.Sales.MonthsToPay,
                            MonthlyInterest = model.AccountReceivable.Sales.MonthlyInterest
                        };

                }
            }
        }

        [Required]
        [Display(Name = "Account Receivable")]
        public ReceivableModel AccountReceivable { get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage ="Not a valid amount!")]
        [Display(Name = "Amount")]
        public decimal PayAmount { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format")]
        [Display(Name = "Payment date")]
        public DateTime PaymentDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [Display(Name = "Encoded date")]
        public DateTime EncodedDate { get; set; }
    }
}
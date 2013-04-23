using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace athena.AR.Models
{
    public class PaymentHistoryModel
    {
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [DataType(DataType.Currency)]
        public Decimal AmountPaid { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name="Date Encoded", Description="Date Encoded")]
        public DateTime DateEncoded { get; set; }
    }
}
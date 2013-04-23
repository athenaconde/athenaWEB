using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace athena.AR.Models
{
    public class PaymentHistoryModel
    {
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [DataType(DataType.Currency)]
        public Decimal AmountPaid { get; set; }

        public DateTime DateEncoded { get; set; }
    }
}
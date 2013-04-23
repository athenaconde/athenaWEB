using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace athena.AR.Library.Model
{
    public class PaymentModel
    {


        public ReceivableModel AccountReceivable { get; set; }

        public decimal PayAmount { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime EncodedDate { get; set; }
    }
}
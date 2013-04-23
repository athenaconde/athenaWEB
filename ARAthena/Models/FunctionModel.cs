using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace athena.AR.Models
{
    public class FunctionModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Color { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string ImageSrc { get; set; }
        public string Float { get; set; }
    }
}
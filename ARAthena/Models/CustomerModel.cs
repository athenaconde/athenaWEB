using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace athena.AR.Models
{
    public class CustomerModel
    {
        public CustomerModel(): this(null) { }

        public CustomerModel(athena.AR.Library.Model.CustomerModel model)
        {
            if (model != null)
            {
               Id = model.Id;
               Name = model.Name;
               Address = model.Address;
               Phone = model.Phone;
               Email = model.Email;
               Active = model.Active;
               DateCreated = model.DateCreated;
               LastDateModified = model.LastDateModified;
            }
        }

        [Required]
        [Display(Name = "Customer Id")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings=false,ErrorMessage="The customer name cannot be empty!")]
        [Display(Name = "Customer name")]
        public string Name { get; set; }

        [Display(Name = "Primary address")]
        public string Address { get; set; }

        [Display(Name="Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display( Name="Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [Display( Name="Date last modified")]
        [DataType(DataType.DateTime)]
        public DateTime LastDateModified { get; set; }
    }
}
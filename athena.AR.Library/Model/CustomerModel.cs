using System;

namespace athena.AR.Library.Model
{
    public class CustomerModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastDateModified { get; set; }
    }
}
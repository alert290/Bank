using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bank.DAL.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public virtual ICollection<CreditCard> CreditCards { get; set; }
    }
}
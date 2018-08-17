using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.Domain.Models
{
    public class EditedCustomer
    {
        public int CustomerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
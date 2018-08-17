using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.Domain.Models
{
    public class EditedCreditCard
    {
        public int CreditCardID { get; set; }

        public decimal Amount { get; set; }

        public string CardNumber { get; set; }

        public string CurrentPIN { get; set; }

        public string NewPIN { get; set; }
    }
}
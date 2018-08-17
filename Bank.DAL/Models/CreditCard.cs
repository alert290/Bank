using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.DAL.Models
{
    public class CreditCard
    {
        public int CreditCardID { get; set; }

        public decimal Amount { get; set; }

        public string CardNumber { get; set; }

        public string PIN { get; set; }

        public DateTime DateOfCreation { get; set; }

        public virtual Customer Customer { get; set; }

        public int CustomerID { get; set; }

        public virtual ICollection<Transaction> TransactionsFrom { get; set; }

        public virtual ICollection<Transaction> TransactionsTo { get; set; }
    }
}
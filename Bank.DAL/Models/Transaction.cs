using System;

namespace Bank.DAL.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }

        public string Comment { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public virtual CreditCard FromCreditCard { get; set; }

        public int FromCreditCardID { get; set; }

        public virtual CreditCard ToCreditCard { get; set; }

        public int ToCreditCardID { get; set; }
    }
}
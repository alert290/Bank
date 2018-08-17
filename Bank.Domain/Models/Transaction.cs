using System;

namespace Bank.Domain.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }

        public string Comment { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string FromCreditCardNumber { get; set; }

        public int FromCreditCardID { get; set; }

        public string ToCreditCardNumber { get; set; }

        public int ToCreditCardID { get; set; }
    }
}
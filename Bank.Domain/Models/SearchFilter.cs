using System;

namespace Bank.Domain.Models
{
    public class SearchFilter
    {
        public int CreditCardID { get; set; }

        public decimal AmountFrom { get; set; }

        public decimal AmountTo { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
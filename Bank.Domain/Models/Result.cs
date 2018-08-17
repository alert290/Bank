using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.Domain.Models
{
    public class Result
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public string SuccessMessage { get; set; }
    }
}
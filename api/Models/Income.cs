using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Income
    {
        public int? IdIncome { get; set; }
        public string? IncomeName { get; set; }
        public decimal? IncomeSum { get; set; }
        public DateTime? IncomeDate { get; set; }
        public int? IdIncomeCategory { get; set; }
        public int? IdIncomeCurrency { get; set; }
        public int? IdIncomeFinAccount { get; set; }
    }
}

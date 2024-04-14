using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class FinanceAccount
    {
        public int? IdFinanceAccount { get; set; }
        public string? AccountName { get; set; }
        public string? AccountNumber { get; set; }
        public decimal? AccountBalance { get; set; }
        public int? IdBuisnessFinAccount { get; set; }
    }
}

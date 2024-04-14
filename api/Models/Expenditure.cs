using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Expenditure
    {
        public int? IdExpenditure { get; set; }
        public string? ExpenditureName { get; set; }
        public decimal? ExpenditureSum { get; set; }
        public DateTime? ExpenditureDate { get; set; }
        public int? IdExpendCategory { get; set; }
        public int? IdExpendCurrency { get; set; }
        public int? IdExpendFinAccount { get; set; }
    }
}

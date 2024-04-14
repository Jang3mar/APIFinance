using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Budget
    {
        public int? IdBudget { get; set; }
        public string? NameBudget { get; set; }
        public decimal? ExpectedBudget { get; set; }
        public decimal? ActualBudget { get; set; }
        public int? IdBudgetUser { get; set; }
    }
}

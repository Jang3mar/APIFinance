using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class BudgetTransactionSummary
    {
        public int? BudgetId { get; set; }
        public decimal? СуммаВсехОпераций { get; set; }
    }
}

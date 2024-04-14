using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Operation
    {
        public int? IdOperation { get; set; }
        public int? NumOperation { get; set; }
        public decimal? SumOperation { get; set; }
        public DateTime? DateOperation { get; set; }
        public string? TypeOperation { get; set; }
        public string? Sender { get; set; }
        public string? Recipient { get; set; }
        public int? BudgetId { get; set; }
        public int? CategoryId { get; set; }
        public int? CurrencyId { get; set; }
    }
}

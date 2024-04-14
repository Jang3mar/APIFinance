using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Tax
    {
        public int? IdTax { get; set; }
        public string? TaxName { get; set; }
        public decimal? TaxSum { get; set; }
        public DateTime? TaxDate { get; set; }
        public int? IdTaxFinAccount { get; set; }
        public int? IdTaxCurrency { get; set; }
    }
}

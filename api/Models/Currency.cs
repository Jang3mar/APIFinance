using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Currency
    {
        public int? IdCurrency { get; set; }
        public string? NameCurrency { get; set; }
        public string? SymbolAbbreviation { get; set; }
    }
}

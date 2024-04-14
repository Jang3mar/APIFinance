using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class BuisnessInfo
    {
        public int? IdBuisness { get; set; }
        public string? BuisnessName { get; set; }
        public DateTime? OpenDate { get; set; }
        public string? Msrnie { get; set; } = null!;
        public int? IdUserBuisness { get; set; }
    }
}

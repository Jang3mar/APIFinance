using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Log
    {
        public int? IdLog { get; set; }
        public string? LogMessage { get; set; }
        public DateTime? LogDate { get; set; }
        public TimeSpan? LogTime { get; set; }
        public int? IdUserLog { get; set; }

    }
}

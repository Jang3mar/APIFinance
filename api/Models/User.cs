using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class User
    {
        public int? IdUser { get; set; }
        public string? SecondName { get; set; }
        public string? FirstName { get; set; } 
        public string? MiddleName { get; set; }
        public string? Login { get; set; } 
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Salt { get; set; } 
        public int? RoleId { get; set; }
    }
}

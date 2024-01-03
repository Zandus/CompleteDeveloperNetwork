using System;
using System.Collections.Generic;

namespace CompleteDeveloperNetwork.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Skillsets { get; set; }
        public string? Hobby { get; set; }
    }
}

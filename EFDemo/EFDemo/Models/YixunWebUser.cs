using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunWebUser
    {
        public int? UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserPasswords { get; set; } = null!;
        public byte[]? UserHead { get; set; }
        public DateTime? FundationTime { get; set; }
        public long? PhoneNum { get; set; }
        public byte? PriorPnum { get; set; }
        public string? MailboxNum { get; set; }
        public string? UserGender { get; set; }
        public string? UserState { get; set; }
        public int? AddressId { get; set; }
    }
}

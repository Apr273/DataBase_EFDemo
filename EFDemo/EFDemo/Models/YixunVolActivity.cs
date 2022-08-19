using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunVolActivity
    {
        public int? VolActId { get; set; }
        public string VolActName { get; set; } = null!;
        public string? ActContent { get; set; }
        public DateTime? ExpTime { get; set; }
        public int AddressId { get; set; }
    }
}

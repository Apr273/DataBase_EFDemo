using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunCity
    {
        public long CId { get; set; }
        public string CityId { get; set; } = null!;
        public string? City { get; set; }
        public string? Parent { get; set; }
    }
}

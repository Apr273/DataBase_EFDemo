using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunAddress
    {
        public int AddressId { get; set; }
        public string ParentId { get; set; } = null!;
        public string Detail { get; set; } = null!;
    }
}

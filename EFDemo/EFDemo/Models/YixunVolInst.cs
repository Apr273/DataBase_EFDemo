using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunVolInst
    {
        public int? VolInstId { get; set; }
        public string InstName { get; set; } = null!;
        public byte[] VolInstCred { get; set; } = null!;
        public string Passwords { get; set; } = null!;
        public byte[]? InstHead { get; set; }
        public DateTime FundationTime { get; set; }
        public int? AddressId { get; set; }
    }
}

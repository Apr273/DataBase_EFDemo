using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunRelatedDp
    {
        public int DpId { get; set; }
        public string DpName { get; set; } = null!;
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public string? Website { get; set; }
        public int AdministratorId { get; set; }
    }
}

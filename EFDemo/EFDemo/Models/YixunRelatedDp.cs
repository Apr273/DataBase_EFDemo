using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunRelatedDp
    {
        public int DpId { get; set; }
        public string DpName { get; set; } = null!;
        public string? Website { get; set; }
        public int AdministratorId { get; set; }
        public string? DpPicUrl { get; set; }
        public string? ContactMethod { get; set; }
        public int? AddressId { get; set; }

        public virtual YixunAddress? Address { get; set; }
        public virtual YixunAdministrator Administrator { get; set; } = null!;
    }
}

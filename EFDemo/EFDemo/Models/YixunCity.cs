using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunCity
    {
        public YixunCity()
        {
            YixunAddresses = new HashSet<YixunAddress>();
            YixunAreas = new HashSet<YixunArea>();
        }

        public long CId { get; set; }
        public string CityId { get; set; } = null!;
        public string? City { get; set; }
        public string? Parent { get; set; }

        public virtual YixunProvince? ParentNavigation { get; set; }
        public virtual ICollection<YixunAddress> YixunAddresses { get; set; }
        public virtual ICollection<YixunArea> YixunAreas { get; set; }
    }
}

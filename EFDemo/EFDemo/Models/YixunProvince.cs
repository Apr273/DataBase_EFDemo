using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunProvince
    {
        public YixunProvince()
        {
            YixunAddresses = new HashSet<YixunAddress>();
            YixunCities = new HashSet<YixunCity>();
        }

        public long SId { get; set; }
        public string ProvinceId { get; set; } = null!;
        public string? Province { get; set; }

        public virtual ICollection<YixunAddress> YixunAddresses { get; set; }
        public virtual ICollection<YixunCity> YixunCities { get; set; }
    }
}

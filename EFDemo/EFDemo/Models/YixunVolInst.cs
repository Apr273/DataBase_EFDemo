using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunVolInst
    {
        public YixunVolInst()
        {
            YixunVolActivities = new HashSet<YixunVolActivity>();
        }

        public int VolInstId { get; set; }
        public string InstName { get; set; } = null!;
        public string Passwords { get; set; } = null!;
        public DateTime FundationTime { get; set; }
        public int? AddressId { get; set; }
        public string? InstSlogan { get; set; }
        public decimal? PeopleCount { get; set; }
        public string? ContactMethod { get; set; }
        public string? InstPicUrl { get; set; }
        public string VolInstCredUrl { get; set; } = null!;
        public string? VolInstIntroduce { get; set; }

        public virtual YixunAddress? Address { get; set; }
        public virtual ICollection<YixunVolActivity> YixunVolActivities { get; set; }
    }
}

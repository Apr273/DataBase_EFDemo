using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunVolActivity
    {
        public YixunVolActivity()
        {
            YixunRecruiteds = new HashSet<YixunRecruited>();
        }

        public int VolActId { get; set; }
        public string VolActName { get; set; } = null!;
        public DateTime ExpTime { get; set; }
        public int? AddressId { get; set; }
        public short Needpeople { get; set; }
        public string? ActPicUrl { get; set; }
        public string ContactMethod { get; set; } = null!;
        public int VolInstId { get; set; }
        public DateTime? ReleaseTime { get; set; }
        public short? SignupPeople { get; set; }
        public string? ActContent { get; set; }

        public virtual YixunAddress? Address { get; set; }
        public virtual YixunVolInst VolInst { get; set; } = null!;
        public virtual ICollection<YixunRecruited> YixunRecruiteds { get; set; }
    }
}

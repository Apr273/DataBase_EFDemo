using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunVolunteer
    {
        public YixunVolunteer()
        {
            YixunRecruiteds = new HashSet<YixunRecruited>();
            YixunSearchinfoFollowups = new HashSet<YixunSearchinfoFollowup>();
        }

        public int VolId { get; set; }
        public short VolTime { get; set; }
        public int VolUserId { get; set; }

        public virtual YixunWebUser VolUser { get; set; } = null!;
        public virtual ICollection<YixunRecruited> YixunRecruiteds { get; set; }
        public virtual ICollection<YixunSearchinfoFollowup> YixunSearchinfoFollowups { get; set; }
    }
}

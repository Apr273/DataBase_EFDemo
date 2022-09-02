using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunSearchinfoFollowup
    {
        public int SearchinfoId { get; set; }
        public int VolId { get; set; }
        public DateTime? Followtime { get; set; }

        public virtual YixunSearchinfo Searchinfo { get; set; } = null!;
        public virtual YixunVolunteer Vol { get; set; } = null!;
    }
}

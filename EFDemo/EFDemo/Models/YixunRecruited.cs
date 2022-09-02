using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunRecruited
    {
        public int VolActId { get; set; }
        public int VolId { get; set; }
        public DateTime? Recruittime { get; set; }

        public virtual YixunVolunteer Vol { get; set; } = null!;
        public virtual YixunVolActivity VolAct { get; set; } = null!;
    }
}

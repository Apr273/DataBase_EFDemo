using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunVolApply
    {
        public int VolApplyId { get; set; }
        public int UserId { get; set; }
        public string? Isreviewed { get; set; }
        public string Specialty { get; set; } = null!;
        public string Background { get; set; } = null!;
        public DateTime? ApplicationTime { get; set; }
        public int AdministratorId { get; set; }
        public string Career { get; set; } = null!;
        public string? Ispass { get; set; }

        public virtual YixunAdministrator Administrator { get; set; } = null!;
        public virtual YixunWebUser User { get; set; } = null!;
    }
}

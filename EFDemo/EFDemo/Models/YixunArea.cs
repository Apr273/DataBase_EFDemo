using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunArea
    {
        public long RId { get; set; }
        public string AreaId { get; set; } = null!;
        public string? Area { get; set; }
        public string? Parent { get; set; }

        public virtual YixunCity? ParentNavigation { get; set; }
    }
}

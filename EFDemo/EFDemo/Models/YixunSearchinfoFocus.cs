using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunSearchinfoFocus
    {
        public int SearchinfoId { get; set; }
        public int UserId { get; set; }
        public DateTime? Focustime { get; set; }

        public virtual YixunSearchinfo Searchinfo { get; set; } = null!;
        public virtual YixunWebUser User { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunClue
    {
        public YixunClue()
        {
            YixunCluesReports = new HashSet<YixunCluesReport>();
        }

        public int ClueId { get; set; }
        public string ClueContent { get; set; } = null!;
        public DateTime? ClueDate { get; set; }
        public int SearchinfoId { get; set; }
        public int UserId { get; set; }
        public string? Isactive { get; set; }

        public virtual YixunSearchinfo Searchinfo { get; set; } = null!;
        public virtual YixunWebUser User { get; set; } = null!;
        public virtual ICollection<YixunCluesReport> YixunCluesReports { get; set; }
    }
}

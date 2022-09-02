using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunInfoReport
    {
        public int InfoReportId { get; set; }
        public DateTime? ReportTime { get; set; }
        public string? ReportContent { get; set; }
        public int SearchinfoId { get; set; }
        public int UserId { get; set; }
        public int AdministratorId { get; set; }
        public string? Isreviewed { get; set; }
        public string? Ispass { get; set; }

        public virtual YixunAdministrator Administrator { get; set; } = null!;
        public virtual YixunSearchinfo Searchinfo { get; set; } = null!;
        public virtual YixunWebUser User { get; set; } = null!;
    }
}

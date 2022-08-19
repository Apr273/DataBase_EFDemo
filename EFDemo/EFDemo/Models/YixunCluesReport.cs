using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunCluesReport
    {
        public int ClueReportId { get; set; }
        public DateTime? ReportTime { get; set; }
        public string? ReportContent { get; set; }
        public string Isreviewed { get; set; } = null!;
        public int ClueId { get; set; }
        public int UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunAdministrator
    {
        public YixunAdministrator()
        {
            YixunCluesReports = new HashSet<YixunCluesReport>();
            YixunInfoReports = new HashSet<YixunInfoReport>();
            YixunNews = new HashSet<YixunNews>();
            YixunRelatedDps = new HashSet<YixunRelatedDp>();
            YixunVolApplies = new HashSet<YixunVolApply>();
        }

        public int AdministratorId { get; set; }
        public string AdministratorName { get; set; } = null!;
        public long? AdministratorPhone { get; set; }
        public string? AdministratorCode { get; set; }

        public virtual ICollection<YixunCluesReport> YixunCluesReports { get; set; }
        public virtual ICollection<YixunInfoReport> YixunInfoReports { get; set; }
        public virtual ICollection<YixunNews> YixunNews { get; set; }
        public virtual ICollection<YixunRelatedDp> YixunRelatedDps { get; set; }
        public virtual ICollection<YixunVolApply> YixunVolApplies { get; set; }
    }
}

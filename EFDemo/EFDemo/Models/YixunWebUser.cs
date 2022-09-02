using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunWebUser
    {
        public YixunWebUser()
        {
            YixunClues = new HashSet<YixunClue>();
            YixunCluesReports = new HashSet<YixunCluesReport>();
            YixunInfoReports = new HashSet<YixunInfoReport>();
            YixunSearchinfoFoci = new HashSet<YixunSearchinfoFocus>();
            YixunSearchinfos = new HashSet<YixunSearchinfo>();
            YixunVolApplies = new HashSet<YixunVolApply>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserPasswords { get; set; } = null!;
        public DateTime? FundationTime { get; set; }
        public long PhoneNum { get; set; }
        public string MailboxNum { get; set; } = null!;
        public string? UserGender { get; set; }
        public string? UserState { get; set; }
        public int? AddressId { get; set; }
        public string? Isactive { get; set; }
        public string? UserHeadUrl { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Token { get; set; }
        public DateTime? LastloginTime { get; set; }
        public string? LastloginIp { get; set; }

        public virtual YixunAddress? Address { get; set; }
        public virtual YixunVolunteer YixunVolunteer { get; set; } = null!;
        public virtual ICollection<YixunClue> YixunClues { get; set; }
        public virtual ICollection<YixunCluesReport> YixunCluesReports { get; set; }
        public virtual ICollection<YixunInfoReport> YixunInfoReports { get; set; }
        public virtual ICollection<YixunSearchinfoFocus> YixunSearchinfoFoci { get; set; }
        public virtual ICollection<YixunSearchinfo> YixunSearchinfos { get; set; }
        public virtual ICollection<YixunVolApply> YixunVolApplies { get; set; }
    }
}

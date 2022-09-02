using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunSearchinfo
    {
        public YixunSearchinfo()
        {
            YixunClues = new HashSet<YixunClue>();
            YixunInfoReports = new HashSet<YixunInfoReport>();
            YixunSearchinfoFoci = new HashSet<YixunSearchinfoFocus>();
            YixunSearchinfoFollowups = new HashSet<YixunSearchinfoFollowup>();
        }

        public int SearchinfoId { get; set; }
        public DateTime? SearchinfoDate { get; set; }
        public string SoughtPeopleState { get; set; } = null!;
        public string SoughtPeopleName { get; set; } = null!;
        public DateTime SoughtPeopleBirthday { get; set; }
        public string SoughtPeopleGender { get; set; } = null!;
        public string? SoughtPeopleDetail { get; set; }
        public int UserId { get; set; }
        public string SearchType { get; set; } = null!;
        public string Isreport { get; set; } = null!;
        public int? AddressId { get; set; }
        public DateTime SearchinfoLostdate { get; set; }
        public string? SoughtPeopleHeight { get; set; }
        public string? SearchinfoPhotoUrl { get; set; }
        public string? ContactMethod { get; set; }
        public string? Isactive { get; set; }

        public virtual YixunAddress? Address { get; set; }
        public virtual YixunWebUser User { get; set; } = null!;
        public virtual ICollection<YixunClue> YixunClues { get; set; }
        public virtual ICollection<YixunInfoReport> YixunInfoReports { get; set; }
        public virtual ICollection<YixunSearchinfoFocus> YixunSearchinfoFoci { get; set; }
        public virtual ICollection<YixunSearchinfoFollowup> YixunSearchinfoFollowups { get; set; }
    }
}

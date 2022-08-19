using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunSearchinfo
    {
        public int SearchinfoId { get; set; }
        public byte[]? SearchinfoPhoto { get; set; }
        public DateTime SearchinfoDate { get; set; }
        public string SoughtPeopleState { get; set; } = null!;
        public string SoughtPeopleName { get; set; } = null!;
        public DateTime SoughtPeopleBirthday { get; set; }
        public string SoughtPeopleGender { get; set; } = null!;
        public string? SoughtPeopleDetail { get; set; }
        public int UserId { get; set; }
        public string SearchType { get; set; } = null!;
        public string Isreport { get; set; } = null!;
        public int? AddressId { get; set; }
        public DateTime? SearchinfoLostdate { get; set; }
    }
}

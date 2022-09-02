using System;
using System.Collections.Generic;


namespace EFDemo.Models
{
    public class SearchinfoFocus
    {
        public int UserId { get; set; }
        public YixunWebUser User { get; set; }
        public int SearchinfoId { get; set; }
        public YixunSearchinfo Searchinfo { get; set; }
    }
}

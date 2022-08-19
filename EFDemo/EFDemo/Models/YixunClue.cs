using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunClue
    {
        public int ClueId { get; set; }
        public string ClueContent { get; set; } = null!;
        public DateTime? ClueDate { get; set; }
        public int SearchinfoId { get; set; }
        public int UserId { get; set; }
    }
}

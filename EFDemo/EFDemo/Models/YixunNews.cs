using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunNews
    {
        public int NewsId { get; set; }
        public string NewsContent { get; set; } = null!;
        public DateTime? NewsTime { get; set; }
        public int AdministratorId { get; set; }
        public string NewsType { get; set; } = null!;
        public string? NewsTitlepagesUrl { get; set; }
        public string? NewsHeadlines { get; set; }
        public string? Isactive { get; set; }

        public virtual YixunAdministrator Administrator { get; set; } = null!;
    }
}

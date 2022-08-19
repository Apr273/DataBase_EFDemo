using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunAdministrator
    {
        public int AdministratorId { get; set; }
        public string AdministratorName { get; set; } = null!;
        public long? AdministratorPhone { get; set; }
        public string? AdministratorCode { get; set; }
    }
}

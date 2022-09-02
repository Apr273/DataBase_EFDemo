using System;
using System.Collections.Generic;

namespace EFDemo.Models
{
    public partial class YixunAddress
    {
        public YixunAddress()
        {
            YixunRelatedDps = new HashSet<YixunRelatedDp>();
            YixunSearchinfos = new HashSet<YixunSearchinfo>();
            YixunVolActivities = new HashSet<YixunVolActivity>();
            YixunVolInsts = new HashSet<YixunVolInst>();
            YixunWebUsers = new HashSet<YixunWebUser>();
        }

        public int AddressId { get; set; }
        public string AreaId { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public string CityId { get; set; } = null!;
        public string ProvinceId { get; set; } = null!;

        public virtual YixunCity City { get; set; } = null!;
        public virtual YixunProvince Province { get; set; } = null!;
        public virtual ICollection<YixunRelatedDp> YixunRelatedDps { get; set; }
        public virtual ICollection<YixunSearchinfo> YixunSearchinfos { get; set; }
        public virtual ICollection<YixunVolActivity> YixunVolActivities { get; set; }
        public virtual ICollection<YixunVolInst> YixunVolInsts { get; set; }
        public virtual ICollection<YixunWebUser> YixunWebUsers { get; set; }
    }
}

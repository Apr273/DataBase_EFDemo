namespace EFDemo.DTO
{
    public  class VolActInfoDTO_H
    {
        public VolActInfoDTO_H(){}
         public int vol_act_id { get; set; }
        public string vol_act_name { get; set; } = null!;
        public string vol_act_content { get; set; } = null!;
        public string vol_act_time { get; set; } = null!;
        public string province_id { get; set; } = null!;
        public string city_id { get; set; } = null!;
        public string area_id { get; set; } = null!;
        public string detail { get; set; } = null!;
        public short need_people { get; set; }
        public string? actpicurl { get; set; }
        public string contact_method { get; set; } = null!;

    }
}

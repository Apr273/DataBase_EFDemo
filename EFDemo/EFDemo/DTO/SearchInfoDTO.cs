namespace EFDemo.DTO
{
    public class SearchInfoDTO
    {
        public SearchInfoDTO() { }
        public int search_info_id { get; set; } = 0!;
        public string search_type { get; set; } = null!;
        public string search_info_date { get; set; } = null!;//寻人信息上传时间
        public string sought_people_state { get; set; } = null!;
        public string sought_people_name { get; set; } = null!;
        public string search_info_lostdate { get; set; } = null!;
        public string sought_people_birthday { get; set; } = null!;
        public string sought_people_gender { get; set; } = null!;
        public string? sought_people_height { get; set; }
        public string? sought_people_detail { get; set; }

        public string isreport { get; set; } = null!;
        public string province_id { get; set; } = null!;
        public string city_id { get; set; } = null!;
        public string area_id { get; set; } = null!;
        public string detail { get; set; } = null!;
        public string? search_info_photourl { get; set; }
        public string? contact_method { get; set; }
    }
}

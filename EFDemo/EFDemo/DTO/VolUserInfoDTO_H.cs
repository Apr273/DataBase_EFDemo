namespace EFDemo.DTO
{
    public  class VolUserInfoDTO_H
    {
        public VolUserInfoDTO_H(){}
        public string? user_state { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; } = null!;
        public DateTime? fundation_time { get; set; }
        public long phone_num { get; set; }
        public string mail_num { get; set; } = null!;
       
        public int vol_id { get; set; }
        public short vol_time { get; set; }
        public string  inst_name { get; set; } = null!;
        public int info_followup_num { get; set; }
        public int act_num { get; set; }

    }
}

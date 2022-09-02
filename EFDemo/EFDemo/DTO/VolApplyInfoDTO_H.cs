namespace EFDemo.DTO
{
    public  class VolApplyInfoDTO_H
    {
        public VolApplyInfoDTO_H(){}
        public int vol_apply_id { get; set; }
        public string? user_state { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; } = null!;
        public string  career { get; set; } = null!;
        public string specialty { get; set; } = null!;
        public string background { get; set; } = null!;
        public DateTime? application_time { get; set; }
        public string? ispass { get; set; }
        

    }
}

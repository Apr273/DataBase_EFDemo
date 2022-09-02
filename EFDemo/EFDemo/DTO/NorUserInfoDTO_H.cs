namespace EFDemo.DTO 
{
    public  class NorUserInfoDTO_H
    {
        public NorUserInfoDTO_H (){}
        
        public string? isactive { get; set; }
        public string? user_state { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; } = null!;
        public DateTime? fundation_time { get; set; }
        public long phone_num { get; set; }
        public int search_info_num { get; set; }//发布的寻人信息
        public int info_report_num { get; set; }//举报的信息数
        public int clue_num { get; set; }//发布的线索数
        public int clue_report_num { get; set; }//举报的线索数
      
    }
}

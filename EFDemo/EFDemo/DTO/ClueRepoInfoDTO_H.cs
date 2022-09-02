namespace EFDemo.DTO 
{
    public  class ClueRepoInfoDTO_H
    {
        public ClueRepoInfoDTO_H  (){}
        public int clue_repo_id { get; set; }
        public int user_id { get; set; }
        public int clue_id { get; set; }
        public string? repo_content { get; set; }
        public DateTime? repo_time { get; set; }
        public string? ispass { get; set; }
    }
}

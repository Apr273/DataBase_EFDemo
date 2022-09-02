namespace EFDemo.DTO
{
    public class InfoRepoInfoDTO_H
    {
        public InfoRepoInfoDTO_H() { }
        public int info_repo_id { get; set; }
        public int user_id { get; set; }
        public int search_info_id { get; set; }
        public string? repo_content { get; set; }
        public DateTime? repo_time { get; set; }
        public string? ispass { get; set; }

    }
}

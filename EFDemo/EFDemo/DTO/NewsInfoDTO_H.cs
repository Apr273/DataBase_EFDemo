namespace EFDemo.DTO
{
    public class NewsInfoDTO_H
    {
        public NewsInfoDTO_H(){}
        public string? Isactive { get; set; }

        public int news_id { get; set; }
        public string? news_title { get; set; } = null!;
        public DateTime? news_time { get; set; }
        public int administrator_id { get; set; }
        public string? news_type { get; set; } = null!;


    }

    
}

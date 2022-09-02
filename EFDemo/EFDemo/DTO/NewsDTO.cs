namespace EFDemo.DTO
{
    public class NewsDTO
    {
        public int NewsId { get; set; } = 0!;
        public string NewsContent { get; set; } = null!;
        public string? Cover { get; set; }
        public string? Title { get; set; }
    }
}

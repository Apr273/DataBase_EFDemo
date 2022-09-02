namespace EFDemo.DTO
{
    public class CluePublished
    {
        public int ClueId { get; set; } = 0!;
        public string ClueContent { get; set; } = null!;
        public DateTime? ClueDate { get; set; }
       public int SearchinfoId { get; set; } = 0!;
    }
}

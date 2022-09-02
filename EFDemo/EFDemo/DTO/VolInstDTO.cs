namespace EFDemo.DTO
{
    public class VolInstDTO
    {
        public int VolInstId { get; set; } = 0!;
        public string InstName { get; set; } = null!;
        public string FundationTime { get; set; } = null!;
        public string? Detail { get; set; } = null!;
        public string? Province { get; set; } = null!;
        public string? City { get; set; } = null!;
        public string? Area { get; set; } = null!;
        public string? InstSlogan { get; set; }
        public decimal? PeopleCount { get; set; }
        public string? ContactMethod { get; set; }
        public string? InstPicUrl { get; set; }
    }
}

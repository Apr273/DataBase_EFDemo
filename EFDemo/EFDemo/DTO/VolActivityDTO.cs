namespace EFDemo.DTO
{
    public class VolActivityDTO
    {
        public int VolActId { get; set; } = 0!;//biaohao
        public string VolActName { get; set; } = null!;//mingzi
        public string ExpTime { get; set; } = null!;//time
        public string? Detail { get; set; } = null!;
        public string? Province { get; set; } = null!;
        public string? City { get; set; } = null!;
        public string? Area { get; set; } = null!;
        public short Needpeople { get; set; } = 0!;
        public string? ActPicUrl { get; set; }//tu
        public string ContactMethod { get; set; } = null!;
        public short? SignupPeople { get; set; }
    }
}

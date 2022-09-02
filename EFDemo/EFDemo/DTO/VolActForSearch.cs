namespace EFDemo.DTO
{
    public class VolActForSearch
    {
        public int VolActId { get; set; } = 0!;//biaohao
        public string VolActName { get; set; } = null!;//mingzi
        public DateTime ExpTime { get; set; }//time
        public string? Address { get; set; } = null!;
        public short Needpeople { get; set; } = 0!;
        public string? ActPicUrl { get; set; }//tu
        public string ContactMethod { get; set; } = null!;
        public short? SignupPeople { get; set; }
    }
}

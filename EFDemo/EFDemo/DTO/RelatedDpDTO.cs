namespace EFDemo.DTO
{
    public class RelatedDpDTO
    {
        public int DpId { get; set; } = 0!;
        public string DpName { get; set; } = null!;
        public string? ContactMethod { get; set; }
        public string? Province { get; set; }
        public string? City { get; set; }

        public string? Area { get; set; }
        public string? Detail { get; set; }
    }
}

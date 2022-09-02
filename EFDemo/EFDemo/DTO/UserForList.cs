namespace EFDemo.DTO
{
    public class UserForList
    {
        public int UserId { get; set; } = 0!;
        public string UserName { get; set; } = null!;
        //public string UserPasswords { get; set; } = null!;
        public DateTime? FundationTime { get; set; }
        public long PhoneNum { get; set; } = 0!;
        //public string MailboxNum { get; set; } = null!;
        // public string? UserGender { get; set; }
        public string? Isactive { get; set; }   //是否为封禁态
        //public string? AddressString { get; set; }
        public int ReportNum { get; set; } = 0!;  //举报信息数量
        public int SearchInfoNum { get; set; } = 0!;  //发布寻人信息数量
    }
}

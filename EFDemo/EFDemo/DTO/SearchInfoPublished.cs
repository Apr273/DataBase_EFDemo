namespace EFDemo.DTO
{
    public class SearchInfoPublished
    {
        public int SearchinfoId { get; set; } = 0!;//ID
        public string? SearchinfoPhotoURL { get; set; }//照片
        public string SoughtPeopleName { get; set; } = null!;//姓名
        public string SoughtPeopleBirthday { get; set; } = null!; //出生日期
        public string SoughtPeopleGender { get; set; } = null!;//性别
        public string SearchType { get; set; } = null!;//类型
        //public string? AddressString { get; set; }  //地址
        //public int? AddressId { get; set; }  //地址
        public string? Province { get; set; } = null!;  //地址
        public string? City { get; set; } = null!;  //地址
        public string? Area { get; set; } = null!;  //地址
        public string? Detail { get; set; } = null!; //地址
        public string SearchinfoLostdate { get; set; }//失散日期
    }
}

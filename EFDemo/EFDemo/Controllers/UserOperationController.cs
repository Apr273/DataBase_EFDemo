using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EFDemo.Models; // 使用Models命名空间
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using EFDemo.DTO;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace EFDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationController : ControllerBase
    {
        private readonly ModelContext ctx;// 创建一个模型上下文对象

        public UserOperationController(ModelContext modelContext)// 构造函数，在每次调用该Controller中的api函数时，都会创建一个context对象用于操作数据库
        {
            ctx = modelContext;
        }


        //@Author：刘睿萌
        [HttpGet("GetAllCLuesPublished")]
        public async Task<string> GetAllCLuesPublished(int user_id, int pageNum, int pageSize)
        {

            MessageFormat message = new();
            try
            {
                var total = ctx.YixunClues.Count(clue => clue.UserId == user_id && clue.Isactive == "Y");
                message.data.Add("total", total);
                var CluesList = await ctx.YixunClues
                .Where(clue => clue.UserId == user_id && clue.Isactive == "Y")
                .OrderBy(clue => clue.ClueId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .Select(clue => new CluePublished
                {
                    ClueId = clue.ClueId,
                    ClueContent = clue.ClueContent,
                    ClueDate = clue.ClueDate,
                    SearchinfoId=clue.SearchinfoId
                }).ToListAsync();
                message.data.Add("clue_list", CluesList.ToArray());

                int count = CluesList.Count;
                if (count > 0)
                    message.data.Add("getcount", count);
                else
                    message.data.Add("getcount", 0);
                message.status = true;
                message.errorCode = 200;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }

        //@Author：刘睿萌
        [HttpGet("GetAllSearchInfoPublished")]
        public async Task<string> GetAllSearchInfoPublished(int user_id,int pageNum, int pageSize)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunSearchinfos.Count(searchInfo => searchInfo.UserId == user_id&&searchInfo.Isactive=="Y"));
                var searchInfoList = await ctx.YixunSearchinfos
                .Where(searchInfo => searchInfo.UserId == user_id && searchInfo.Isactive == "Y")
                .OrderByDescending(searchInfo => searchInfo.SearchinfoId)
                .Skip((pageNum - 1)* pageSize)
                .Take(pageSize)
                .Select(searchInfo => new SearchInfoPublished
                {
                    SearchinfoId = searchInfo.SearchinfoId,
                    SearchinfoPhotoURL = searchInfo.SearchinfoPhotoUrl,
                    SoughtPeopleName = searchInfo.SoughtPeopleName,
                    SoughtPeopleBirthday = searchInfo.SoughtPeopleBirthday.ToString("d"),
                    SoughtPeopleGender = searchInfo.SoughtPeopleGender,
                    SearchType = searchInfo.SearchType,
                    SearchinfoLostdate = searchInfo.SearchinfoLostdate.ToString("d"),
                    Province = searchInfo.Address.ProvinceId,
                    City = searchInfo.Address.CityId,
                    Area = searchInfo.Address.AreaId,
                    Detail = searchInfo.Address.Detail,

                }).ToListAsync();
                message.data.Add("searchInfo_list", searchInfoList.ToArray());

                int count = searchInfoList.Count;
                if (count > 0)
                    message.data.Add("getcount", count);
                else
                    message.data.Add("getcount", 0);
                message.status = true;
                message.errorCode = 200;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }

        //@Author：刘睿萌，韩淑荣
        [HttpGet("GetFollowInfo")]
        public async Task<string> GetFollowInfo(int userid, int pagenum, int pagesize)
        {
            MessageFormat message = new();
            try
            {

                message.data.Add("total", ctx.YixunSearchinfos.Where(i => i.Isactive == "Y" && i.YixunSearchinfoFoci.Any(i => i.UserId == userid)).Count());
                var infos = await ctx.YixunSearchinfos.Where(i => i.Isactive == "Y" && i.YixunSearchinfoFoci.Any(i => i.UserId == userid))
                .OrderBy(info => info.SearchinfoId).Skip((pagenum - 1) * pagesize).Take(pagesize)
                .Select(info => new SearchInfoDTO
                {
                    search_info_id = info.SearchinfoId,
                    search_type = info.SearchType,
                    search_info_date = (info.SearchinfoDate!=null)?info.SearchinfoDate.Value.ToString("d"):null!,
                    sought_people_state = info.SoughtPeopleState,
                    sought_people_name = info.SoughtPeopleName,
                    search_info_lostdate = info.SearchinfoLostdate.ToString("d"),
                    sought_people_birthday = info.SoughtPeopleBirthday.ToString("d"),
                    sought_people_gender = info.SoughtPeopleGender,
                    sought_people_height = info.SoughtPeopleHeight,
                    sought_people_detail = info.SoughtPeopleHeight,
                    isreport = info.Isreport,
                    province_id = (info.Address != null) ? info.Address.ProvinceId : null!,
                    city_id = (info.Address != null) ? info.Address.CityId : null!,
                    area_id = (info.Address != null) ? info.Address.AreaId : null!,
                    detail = (info.Address != null) ? info.Address.Detail : null!,
                    search_info_photourl = info.SearchinfoPhotoUrl,
                    contact_method = info.ContactMethod
                })
                .ToListAsync();

                message.data.Add("follow_info", infos.ToArray());

                int count = infos.Count;
                if (count > 0)
                    message.data.Add("getcount", count);
                else
                    message.data.Add("getcount", 0);

                message.status = true;
                message.errorCode = 200;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }


        //@Author：刘睿萌
        [HttpPost("AddSearchPeopleInfo")] 

        public async Task<string> AddSearchPeopleInfo(dynamic inputData)
        {
            MessageFormat message = new(); 
            try
            {
          
                int user_id = int.Parse(inputData.GetProperty("user_id").ToString());
                string search_type = inputData.GetProperty("search_type").ToString();
                string sought_people_name = inputData.GetProperty("sought_people_name").ToString();
                string sought_people_gender = inputData.GetProperty("sought_people_gender").ToString();
                string sought_people_height = inputData.GetProperty("sought_people_height").ToString();
                string sought_people_detail = inputData.GetProperty("sought_people_detail").ToString();
                string sought_people_birthday = inputData.GetProperty("sought_people_birthday").ToString();
                string sought_people_state = inputData.GetProperty("sought_people_state").ToString();
                string isreport = inputData.GetProperty("isreport").ToString();
                string searchinfo_lostdate = inputData.GetProperty("searchinfo_lostdate").ToString();
                string contact_method = inputData.GetProperty("contact_method").ToString();



                YixunSearchinfo NewSearchInfo = new YixunSearchinfo();
                NewSearchInfo.UserId = user_id;
                NewSearchInfo.SearchType = search_type;
                NewSearchInfo.SoughtPeopleName = sought_people_name;
                NewSearchInfo.SoughtPeopleGender = sought_people_gender;
                NewSearchInfo.SoughtPeopleHeight = sought_people_height;
                NewSearchInfo.SoughtPeopleBirthday = Convert.ToDateTime(sought_people_birthday);
                NewSearchInfo.SoughtPeopleState = sought_people_state;
                NewSearchInfo.SoughtPeopleDetail = sought_people_detail;
                NewSearchInfo.Isreport = isreport;
                NewSearchInfo.SearchinfoLostdate = Convert.ToDateTime(searchinfo_lostdate);
                NewSearchInfo.ContactMethod = contact_method;
         
                //YIXUN_ADDRESS表，首先新建地址
                string province_id = inputData.GetProperty("province_id").ToString();
                string city_id = inputData.GetProperty("city_id").ToString();
                string area_id = inputData.GetProperty("area_id").ToString();
                string address_detail = inputData.GetProperty("address_detail").ToString();
                if (province_id != "")
                {
                    YixunAddress NewAddress = new YixunAddress();

                    NewAddress.ProvinceId = province_id;
                    NewAddress.CityId = city_id;
                    NewAddress.AreaId = area_id;
                    NewAddress.Detail = address_detail;

                    ctx.YixunAddresses.Add(NewAddress);
                    await ctx.SaveChangesAsync();     //保存更改
                    var addressid = ctx.YixunAddresses.Select(s => s.AddressId).Max();
                    NewSearchInfo.AddressId = addressid;
                }
                ctx.YixunSearchinfos.Add(NewSearchInfo);
                await ctx.SaveChangesAsync();      //保存更改


                YixunVolunteer volunteer = ctx.Set<YixunVolunteer>().Where(x => 1 == 1).OrderBy(x => Guid.NewGuid()).First();//随机一个志愿者
                var SearchinfoId = ctx.YixunSearchinfos.Select(s => s.SearchinfoId).Max();

                YixunSearchinfoFollowup follow = new YixunSearchinfoFollowup();
                follow.VolId = volunteer.VolId;
                follow.SearchinfoId = SearchinfoId;
                ctx.YixunSearchinfoFollowups.Add(follow);
                await ctx.SaveChangesAsync();    //保存更改


                //YixunVolActivity _activity = ctx.YixunVolActivities.Single(b => b.VolActName == act_name && b.VolInstId == volInst_Id);

                message.data.Add("searchInfo_id", SearchinfoId);

                message.errorCode = 200;
                message.status = true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }

            return message.ReturnJson();
        }
        //@Author：刘睿萌
        [HttpPut("AddSearchInfoPic")]
        public string AddSearchInfoPic(dynamic inputdata)
        {
            MessageFormat message = new();
            try
            {
                int searchInfo_id = int.Parse(inputdata.GetProperty("searchInfo_id").ToString());
                YixunSearchinfo searchInfo = ctx.YixunSearchinfos.Single(b => b.SearchinfoId == searchInfo_id);
                string img_base64 = inputdata.GetProperty("searchInfo_pic").ToString();
                string type = "." + img_base64.Split(',')[0].Split(';')[0].Split('/')[1];
                img_base64 = img_base64.Split("base64,")[1];
                byte[] img_bytes = Convert.FromBase64String(img_base64);
                var client = OSSHelper.createClient();
                MemoryStream stream = new MemoryStream(img_bytes, 0, img_bytes.Length);
                string path = "searchInfo_pic/" + searchInfo_id.ToString() + type;
                client.PutObject(OSSHelper.bucketName, path, stream); // 直接覆盖
                string imgurl = "https://yixun-picture.oss-cn-shanghai.aliyuncs.com/" + path;
                searchInfo.SearchinfoPhotoUrl = imgurl;
                ctx.YixunSearchinfos.Update(searchInfo);
                ctx.SaveChanges();
                message.data.Add("searchInfo_pic", imgurl);
                message.errorCode = 200;
                message.status = true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }

        //@Author：梁厚
        [HttpPost("AddSearchInfoReport")]    //设置路由（路径）
        //还没有图片
        public async Task<string> AddSearchInfoReport(dynamic inputData)
        {
            MessageFormat message = new();  //MessageFormat是数据返回的类型，见MessageFormat.cs文件
            try
            {
                //parse用于转换类型，int.Parse()表示转为int;
                int user_id = int.Parse(inputData.GetProperty("user_id").ToString());
                int searchinfo_id = int.Parse(inputData.GetProperty("searchinfo_id").ToString());
                string report_content = inputData.GetProperty("report_content").ToString();

                //随机分配一个管理员
                var admin_id = ctx.YixunAdministrators.Where(x => 1 == 1).OrderBy(x => Guid.NewGuid()).First();
                //取YixunWebUser类中where YiXunWebUser.UserId == user_id的数据给user对象
                //如果没有，user是null，后面不会向message的data字典里面加输出信息

                YixunInfoReport NewAddInfoReport = new YixunInfoReport();
                //NewAddNews.NewsId = ;
                NewAddInfoReport.UserId = user_id;
                NewAddInfoReport.SearchinfoId = searchinfo_id;
                NewAddInfoReport.ReportContent = report_content;
                NewAddInfoReport.AdministratorId = admin_id.AdministratorId;

                //EFEntities 可以理解成EF的上下文数据操作类，负责与数据库打交道。

                //将数据添加到EF,并且标记为添加标记，返回受影响的行数。
                ctx.YixunInfoReports.Add(NewAddInfoReport);
                await ctx.SaveChangesAsync();
                //SaveChanges()数据保存到数据库,根据前面的标记生成对应的Sql语句，交给数据库执行。
                message.errorCode = 200;
                message.status = true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }

            return message.ReturnJson();
        }


        //@Author：梁厚
        [HttpPost("AddSearchClueReport")]    //设置路由（路径）
        //还没有图片
        public async Task<string> AddSearchClueReport(dynamic inputData)
        {
            MessageFormat message = new();  //MessageFormat是数据返回的类型，见MessageFormat.cs文件
            try
            {
                //parse用于转换类型，int.Parse()表示转为int;
                int user_id = int.Parse(inputData.GetProperty("user_id").ToString());
                int clue_id = int.Parse(inputData.GetProperty("clue_id").ToString());
                string report_content = inputData.GetProperty("report_content").ToString();

                //随机分配一个管理员
                var admin_id = ctx.YixunAdministrators.Where(x => 1 == 1).OrderBy(x => Guid.NewGuid()).First();
                //取YixunWebUser类中where YiXunWebUser.UserId == user_id的数据给user对象
                //如果没有，user是null，后面不会向message的data字典里面加输出信息

                YixunCluesReport NewAddClueReport = new YixunCluesReport();
                //NewAddNews.NewsId = ;
                NewAddClueReport.UserId = user_id;
                NewAddClueReport.ClueId = clue_id;
                NewAddClueReport.ReportContent = report_content;
                NewAddClueReport.AdministratorId = admin_id.AdministratorId;

                //EFEntities 可以理解成EF的上下文数据操作类，负责与数据库打交道。

                //将数据添加到EF,并且标记为添加标记，返回受影响的行数。
                ctx.YixunCluesReports.Add(NewAddClueReport);
                await ctx.SaveChangesAsync();
                //SaveChanges()数据保存到数据库,根据前面的标记生成对应的Sql语句，交给数据库执行。
                message.errorCode = 200;
                message.status = true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }

            return message.ReturnJson();
        }

        //@Author：梁厚
        [HttpPost("AddSearchPeopleClue")]    //设置路由（路径）
        //还没有图片
        public async Task<string> AddSearchPeopleClue(dynamic inputData)
        {
            MessageFormat message = new();  //MessageFormat是数据返回的类型，见MessageFormat.cs文件
            try
            {
                //parse用于转换类型，int.Parse()表示转为int;
                int user_id = int.Parse(inputData.GetProperty("user_id").ToString());
                int searchinfo_id = int.Parse(inputData.GetProperty("searchinfo_id").ToString());
                string clue_content = inputData.GetProperty("clue_content").ToString();

                //随机分配一个管理员
                //var admin_id = ctx.YixunAdministrators.Where(x => 1 == 1).OrderBy(x => Guid.NewGuid()).First();
                //取YixunWebUser类中where YiXunWebUser.UserId == user_id的数据给user对象
                //如果没有，user是null，后面不会向message的data字典里面加输出信息

                YixunClue NewAddClue = new YixunClue();
                //NewAddNews.NewsId = ;
                NewAddClue.UserId = user_id;
                NewAddClue.SearchinfoId = searchinfo_id;
                NewAddClue.ClueContent = clue_content;
                //EFEntities 可以理解成EF的上下文数据操作类，负责与数据库打交道。

                //将数据添加到EF,并且标记为添加标记，返回受影响的行数。
                ctx.YixunClues.Add(NewAddClue);
                await ctx.SaveChangesAsync();
                //SaveChanges()数据保存到数据库,根据前面的标记生成对应的Sql语句，交给数据库执行。
                message.errorCode = 200;
                message.status = true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }

            return message.ReturnJson();
        }


    }
}

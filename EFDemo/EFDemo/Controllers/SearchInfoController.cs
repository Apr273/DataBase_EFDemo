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
using Microsoft.EntityFrameworkCore;


//未明确如何返回地址和图片
namespace EFDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchInfoController : ControllerBase
    {
        private readonly ModelContext ctx;// 创建一个模型上下文对象

        public SearchInfoController(ModelContext modelContext)// 构造函数，在每次调用该Controller中的api函数时，都会创建一个context对象用于操作数据库
        {
            ctx = modelContext;
        }

        //@Author：杨嘉仪
        [HttpGet("GetSearchInfo")]    //设置路由（路径）
        public async Task<string> GetSearchInfo(int search_id)
        {
            MessageFormat message = new();  //MessageFormat是数据返回的类型，见MessageFormat.cs文件
            try
            {

                YixunSearchinfo searchinfo = await ctx.YixunSearchinfos.SingleOrDefaultAsync(b => b.SearchinfoId == search_id&& b.Isactive == "Y");
                //取YixunWebUser类中where YiXunWebUser.UserId == user_id的数据给user对象
                //YixunWebUser user1 = ctx.YixunWebUsers.SingleOrDefault(b => (b.UserName == user_name||b.UserName == null)&&(b.UserId==user_id));
                //如果没有，user是null，后面不会向message的data字典里面加输出信息

                message.data.Add("search_id", search_id);//data是字典，这是向message里面加输出的信息
                message.data.Add("search_name", searchinfo.SoughtPeopleName);
                message.data.Add("search_birthday", searchinfo.SoughtPeopleBirthday.ToString("d"));
                message.data.Add("search_gender", searchinfo.SoughtPeopleGender);
                message.data.Add("search_detail", searchinfo.SoughtPeopleDetail);
                message.data.Add("search_photo", searchinfo.SearchinfoPhotoUrl);

                var clues = await ctx.YixunClues.Where(i => i.SearchinfoId== search_id)
                              .Select(i => new CluePublished
                              {
                                  ClueId = i.ClueId,
                                  ClueContent = i.ClueContent,
                                  SearchinfoId = i.SearchinfoId,
                              })
                              .ToListAsync();
                message.data.Add("search_clue", clues.ToArray());
                var vols = await ctx.YixunVolunteers.Where(i => i.YixunSearchinfoFollowups.Any(i=>i.SearchinfoId== search_id))
                                .Select(i => new VolunteerDTO
                                {
                                    Name = i.VolUser.UserName,
                                    PhoneNum = i.VolUser.PhoneNum,
                                })
                              .ToListAsync();
                message.data.Add("search_vols", vols.ToArray());
                if (searchinfo.AddressId != null)
                {
                    YixunAddress address = ctx.YixunAddresses.SingleOrDefault(b => b.AddressId == searchinfo.AddressId);
                    message.data.Add("search_province", address.ProvinceId);
                    message.data.Add("search_city", address.CityId);
                    message.data.Add("search_area", address.AreaId);
                    message.data.Add("search_address", address.Detail);
                }
                else
                {
                    message.data.Add("search_province", null);
                    message.data.Add("search_city", null);
                    message.data.Add("search_area", null);
                    message.data.Add("search_address", null);
                }
                message.status = true;  //status表示状态，修改为成功，原来默认是false
                message.errorCode = 200;        //修改状态码，默认的300表示找不到指定资源，200表示请求成功，400表示服务器未能处理

            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }

            return message.ReturnJson();
        }

    }
}

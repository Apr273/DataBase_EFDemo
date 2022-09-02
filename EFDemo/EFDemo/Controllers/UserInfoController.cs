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

namespace EFDemo.Controllers
{
    //为了列表输出所有user时使用，其中address以string记录地址


    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly ModelContext ctx;// 创建一个模型上下文对象

        public UserInfoController(ModelContext modelContext)// 构造函数，在每次调用该Controller中的api函数时，都会创建一个context对象用于操作数据库
        {
            ctx = modelContext;
        }

        [HttpGet("GetUserInfo")]    //设置路由（路径）

        public async Task<string> GetUserInfo(int user_id)
        {
            MessageFormat message = new();
            try
            {
                YixunWebUser user = await ctx.YixunWebUsers.SingleOrDefaultAsync(b => b.UserId == user_id);
                message.data.Add("user_id", user.UserId);
                message.data.Add("user_name", user.UserName);
                message.data.Add("user_password", user.UserPasswords);
                message.data.Add("user_head", user.UserHeadUrl);
                message.data.Add("user_fundationtime", user.FundationTime);
                message.data.Add("user_phonenum", user.PhoneNum);
                message.data.Add("user_mailbox", user.MailboxNum);
                message.data.Add("user_gender", user.UserGender);

                if (user.AddressId != null)
                {
                    YixunAddress address = ctx.YixunAddresses.SingleOrDefault(b => b.AddressId == user.AddressId);
                    message.data.Add("user_province", address.ProvinceId);
                    message.data.Add("user_city", address.CityId);
                    message.data.Add("user_area", address.AreaId);
                    message.data.Add("user_address", address.Detail);
                }
                else
                {
                    message.data.Add("user_province", null);
                    message.data.Add("user_city", null);
                    message.data.Add("user_area", null);
                    message.data.Add("user_address", null);
                }
                message.status = true;  
                message.errorCode = 200; 

            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }

            return message.ReturnJson();
        }


        //这里没做分页处理
        [HttpGet("GetAllUserInfo")]
        public async Task<string> GetAllUserInfo()
        {
            MessageFormat message = new();
            try
            {
                var usersList =await ctx.YixunWebUsers
                .ToListAsync();      //获取所有YixunWebUser的数据
                List<UserForList> users = new List<UserForList>();
                //users是一个用来存储YixunWebUser的List
                foreach (var user in usersList)     //对于每一行表中数据user（相当于i）
                {
                    //YiXunWebUser userInfo=...
                    UserForList userInfo = new UserForList(); //user信息赋值给userInfo对象，然后userInfo对象加到users这个List里面
                    userInfo.UserId = user.UserId;
                    userInfo.UserName = user.UserName;
                    userInfo.FundationTime = user.FundationTime;
                    userInfo.PhoneNum = user.PhoneNum;

                    userInfo.Isactive = user.Isactive;
                    int ReportNum=ctx.YixunCluesReports.Count(b => b.UserId == user.UserId)
                        + ctx.YixunInfoReports.Count(b => b.UserId == user.UserId);
                    int SearchInfoNum = ctx.YixunSearchinfos.Count(b => b.UserId == user.UserId);
                    userInfo.ReportNum = ReportNum;
                    userInfo.SearchInfoNum = SearchInfoNum;
                    users.Add(userInfo);
                }
                //到这里得到了一个完整的List——users
                //以数组形式返回
                message.data.Add("user_info", users.ToArray());
                //null ,XX:NULL,XX:NULL
                message.status = true;
                message.errorCode = 200;
            }

            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }

            return message.ReturnJson();
        }

        //前端传入格式：{"user_id":13,"user_password":"11111","new_password":"123456"}
        //输出格式：{"errorCode":200,"status":true,"data":{"user_id":1,"user_new_password":"123456"}}
        [HttpPut("ChangePassword")]
        public string changePassword(dynamic inputData)
        {
            MessageFormat message = new MessageFormat();
            try
            {
                int user_id = int.Parse(inputData.GetProperty("user_id").ToString());
                //parse用于转换类型，int.Parse()表示转为int;
                string user_password = inputData.GetProperty("user_password").ToString();
                string new_password = inputData.GetProperty("new_password").ToString();

                YixunWebUser user = ctx.YixunWebUsers.Single(b => b.UserId == user_id && b.UserPasswords == user_password);
                user.UserPasswords = new_password;
                message.errorCode = 200;
                message.status = true;
                ctx.YixunWebUsers.Update(user);
                ctx.SaveChanges();      //保存更改
                //YixunWebUser newuser = ctx.YixunWebUsers.SingleOrDefault(b => b.UserId == user_id);
                //message.data.Add("user_id", user_id);
                //message.data.Add("user_new_password", newuser.UserPasswords);
                //上面三行本来是测试有没有成功的，不用管他
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }

        //注册的时候默认没有头像，地址，性别，需要后面添加
        //传入：{"user_id":1,"user_name":"2323","user_phone":12345678909,"user_email":"34343434","user_province":"110000","user_city":"110100","user_area":"110102","user_address":"一二一大街"}
        [HttpPut("ChangeUserInfo")]
        public async Task<string> ChangeUserInfo(dynamic inputData)
        {
            MessageFormat message = new MessageFormat();
            try
            {
                int user_id = int.Parse(inputData.GetProperty("user_id").ToString());
                string user_name = inputData.GetProperty("user_name").ToString();
                long user_phone = long.Parse(inputData.GetProperty("user_phone").ToString());
                string user_email = inputData.GetProperty("user_email").ToString();
                string user_province = inputData.GetProperty("user_province").ToString();//null
                string user_city = inputData.GetProperty("user_city").ToString();
                string user_area = inputData.GetProperty("user_area").ToString();
                string user_address = inputData.GetProperty("user_address").ToString();

                YixunWebUser user = await ctx.YixunWebUsers.SingleOrDefaultAsync(b => b.UserId == user_id);
                user.UserName = user_name;
                user.PhoneNum = user_phone;
                user.MailboxNum = user_email;
                if(user_province != "")
                {
                    if (user.AddressId != null)
                    {
                        YixunAddress address = await ctx.YixunAddresses.SingleOrDefaultAsync(b => b.AddressId == user.AddressId);
                        address.ProvinceId = user_province;
                        address.CityId = user_city;
                        address.AreaId = user_area;
                        address.Detail = user_address;
                        ctx.YixunAddresses.Update(address);
                        await ctx.SaveChangesAsync();
                    }
                    else
                    {
                        //新建ADDRESS ID
                        YixunAddress address = new YixunAddress();
                        address.Detail = user_address;
                        address.AreaId = user_area;
                        address.CityId = user_city;
                        address.ProvinceId = user_province;
                        ctx.YixunAddresses.Add(address);
                        await ctx.SaveChangesAsync();
                        var addressid = ctx.YixunAddresses.Select(s => s.AddressId).Max();
                        user.AddressId = addressid;
                    }
                }

                message.errorCode = 200;
                message.status = true;
                ctx.YixunWebUsers.Update(user);
                await ctx.SaveChangesAsync();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }


        [HttpPut("upLoadUserHead")]
        public string upLoadUserHead(dynamic inputdata)
        {
            MessageFormat message = new();
            try
            {
                int user_id = int.Parse(inputdata.GetProperty("user_id").ToString());
                YixunWebUser user = ctx.YixunWebUsers.Single(b => b.UserId == user_id);
                string img_base64 = inputdata.GetProperty("user_head").ToString();
                string type = "." + img_base64.Split(',')[0].Split(';')[0].Split('/')[1];
                img_base64 = img_base64.Split("base64,")[1];
                byte[] img_bytes = Convert.FromBase64String(img_base64);
                var client = OSSHelper.createClient();
                MemoryStream stream = new MemoryStream(img_bytes, 0, img_bytes.Length);
                string path = "user_head/" + user_id.ToString() + type;
                client.PutObject(OSSHelper.bucketName, path, stream); // 直接覆盖
                string imgurl = "https://yixun-picture.oss-cn-shanghai.aliyuncs.com/" + path;
                user.UserHeadUrl = imgurl;

                ctx.YixunWebUsers.Update(user);
                ctx.SaveChanges();
                message.data.Add("img_url", imgurl);
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

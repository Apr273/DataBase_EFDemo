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

namespace EFDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly ModelContext ctx;// 创建一个模型上下文对象

        public LogInController(ModelContext modelContext)// 构造函数，在每次调用该Controller中的api函数时，都会创建一个context对象用于操作数据库
        {
            ctx = modelContext;
        }
        //@Author：刘睿萌
        [HttpPost("LogInCheck")]   
        //注意！！HTTPGet不能传入requestbody接收参数
        public string LogInCheck(dynamic inputData)
        {
            MessageFormat message = new();
            try
            {
                long user_phone = long.Parse(inputData.GetProperty("user_phone").ToString());
                string user_password = inputData.GetProperty("user_password").ToString();
                YixunWebUser user = ctx.YixunWebUsers.SingleOrDefault(b => b.PhoneNum == user_phone && b.UserPasswords==user_password);
                if(user!=null)
                {
                    if (user.Isactive == "N" || user.UserState == "N")
                        return message.ReturnJson();
                    YixunVolunteer volunteer = ctx.YixunVolunteers.SingleOrDefault(b => b.VolUserId == user.UserId);
                    if(volunteer!=null)
                    {
                        message.data.Add("identity", "volunteer");
                        message.data.Add("vol_id", volunteer.VolId);
                        message.data.Add("user_id", volunteer.VolUserId);
                    }
                    else
                    {
                        message.data.Add("identity", "user");
                        message.data.Add("id", user.UserId);
                    }
                }
                else
                {
                    YixunAdministrator administrator = ctx.YixunAdministrators.Single(b => b.AdministratorPhone == user_phone && b.AdministratorCode == user_password);
                    message.data.Add("identity", "administrator");
                    message.data.Add("id", administrator.AdministratorId);

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

        //@Author：刘睿萌
        [HttpPost("Regist")]
        public string Regist(dynamic new_user)
        {
            MessageFormat message = new MessageFormat();

            //JsonElement jsonElement = new();
            //jsonElement.GetProperty().ToString();
            long user_phone = long.Parse(new_user.GetProperty("user_phone").ToString());
            string user_password = new_user.GetProperty("user_password").ToString();
            string user_name = new_user.GetProperty("user_name").ToString();
            string user_email = new_user.GetProperty("user_email").ToString();

            try
            {
       

                var user = ctx.YixunWebUsers
                    .SingleOrDefault(b => b.PhoneNum == user_phone);
                if(user != null)
                {
                    return message.ReturnJson();
                }
                else
                {
                    YixunWebUser newUser = new YixunWebUser();
                    newUser.UserName = user_name;
                    newUser.PhoneNum = user_phone;
                    newUser.UserPasswords = user_password;
                    newUser.MailboxNum = user_email;
                    ctx.YixunWebUsers.Add(newUser);
                    ctx.SaveChanges();
                    var user_id = ctx.YixunWebUsers.Select(s => s.UserId).Max();
                    message.data.Add("user_id", user_id);
                    message.status = true;
                    message.errorCode = 200;

                }
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            return message.ReturnJson();
        }
        //@Author：刘睿萌
        [HttpPut("UpLoadInfo")]
        public string UpLoadInfo(dynamic inputData)
        {
            MessageFormat message = new();
            try
            {
                int user_id = int.Parse(inputData.GetProperty("user_id").ToString());
                string user_gender= inputData.GetProperty("user_gender").ToString();
                string user_province = inputData.GetProperty("user_province").ToString();//null
                string user_city = inputData.GetProperty("user_city").ToString();
                string user_area = inputData.GetProperty("user_area").ToString();
                string user_address = inputData.GetProperty("user_address").ToString();
                YixunWebUser user = ctx.YixunWebUsers.Single(b => b.UserId == user_id);
                if(user_province!="")
                {
                    //新建ADDRESS ID
                    YixunAddress address = new YixunAddress();
                    address.Detail = user_address;
                    address.AreaId = user_area;
                    address.CityId = user_city;
                    address.ProvinceId = user_province;
                    ctx.YixunAddresses.Add(address);
                    ctx.SaveChanges();
                    var AddressId = ctx.YixunAddresses.Select(s => s.AddressId).Max();
                    user.AddressId = AddressId;
                }
                user.UserGender = user_gender;
                string img_base64 = inputData.GetProperty("user_head").ToString();
                if(img_base64!="")
                {
                    string type = "." + img_base64.Split(',')[0].Split(';')[0].Split('/')[1];
                    img_base64 = img_base64.Split("base64,")[1];
                    byte[] img_bytes = Convert.FromBase64String(img_base64);
                    var client = OSSHelper.createClient();
                    MemoryStream stream = new MemoryStream(img_bytes, 0, img_bytes.Length);
                    string path = "user_head/" + user_id.ToString() + type;
                    client.PutObject(OSSHelper.bucketName, path, stream); // 直接覆盖
                    string imgurl = "https://yixun-picture.oss-cn-shanghai.aliyuncs.com/" + path;
                    user.UserHeadUrl = imgurl;
                    message.data.Add("img_url", imgurl);
                }
                ctx.YixunWebUsers.Update(user);
                ctx.SaveChanges();


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

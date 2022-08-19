using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EFDemo.Models; // 使用Models命名空间
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace EFDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly ModelContext ctx;

        public LogInController(ModelContext modelContext)
        {
            ctx = modelContext;
        }

        //[HttpPost]
        //public string login(string user_phone, string password)
        //{
        //    Message message = new();
        //    try
        //    {
        //        var user_info = ctx.YixunWebUsers
        //        .Where(user => user.UserPhone == user_phone && user.UserPassword == password)
        //        .Select(user => new
        //        {
        //            user.UserId,
        //            user.UserName,
        //        }).First();
        //        message.Add("userid", user_info.UserId);
        //        message.Add("username", user_info.UserName);
        //        message.Add("errorCode", 200);
        //    }
        //    catch (Exception error)
        //    {
        //        Console.WriteLine(error.ToString());
        //    }

        //    return message.ReturnJson();
        //}





    }
}

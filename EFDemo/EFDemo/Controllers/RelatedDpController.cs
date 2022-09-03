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
    [Route("api/[controller]")]
    [ApiController]
    public class RelatedDpController : ControllerBase
    {
        private readonly ModelContext ctx;// 创建一个模型上下文对象

        public RelatedDpController(ModelContext modelContext)// 构造函数，在每次调用该Controller中的api函数时，都会创建一个context对象用于操作数据库
        {
            ctx = modelContext;
        }

        //@Author：刘睿萌
        [HttpGet("GetRelatedDps")]
        public async Task<string> GetRelatedDps(string province,string city)
        {
            MessageFormat message = new();
            try
            {

                var AreaList = await ctx.YixunAreas.Where(i => i.Parent == city).Select(i => i.AreaId).ToListAsync();
                foreach(var area in AreaList)
                {
                    var DpList = await ctx.YixunRelatedDps.Where(i => i.AddressId != null && i.Address.AreaId == area)
                    .OrderBy(i => i.DpId)
                    .Select(i => new RelatedDpDTO
                    {
                        DpId = i.DpId,
                        DpName = i.DpName,
                        ContactMethod = i.ContactMethod,
                        Province = i.Address.ProvinceId,
                        City = i.Address.CityId,
                        Area = i.Address.AreaId,
                        Detail = i.Address.Detail,
                    })
                    .ToListAsync();

                    message.data.Add(area, DpList.ToArray());
                }
                //var DpList = await ctx.YixunRelatedDps.Where(i => i.AddressId != null && i.Address.ProvinceId == province && i.Address.CityId == city)
                //.OrderBy(i => i.DpId)
                //.Select(i => new RelatedDpDTO
                //{
                //    DpId = i.DpId,
                //    DpName = i.DpName,
                //    ContactMethod = i.ContactMethod,
                //    Province = i.Address.ProvinceId,
                //    City = i.Address.CityId,
                //    Area = i.Address.AreaId,
                //    Detail = i.Address.Detail,
                //})
                //.ToListAsync();
                //message.data.Add("DP_list", DpList.ToArray());
                message.status = true;
                message.errorCode = 200;

            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }

            return message.ReturnJson();
        }

        //@Author：杨嘉仪
        [HttpGet("GetDPDetail")]    //设置路由（路径）
        //感觉没问题了，返回图片url
        public async Task<string> GetDPDetail(int DP_id)
        {
            MessageFormat message = new();  //MessageFormat是数据返回的类型，见MessageFormat.cs文件
            try
            {
                YixunRelatedDp DP = await ctx.YixunRelatedDps.SingleOrDefaultAsync(b => b.DpId == DP_id);
                // message.data.Add("user", user);
                message.data.Add("DP_id", DP.DpId);//data是字典，这是向message里面加输出的信息
                message.data.Add("DP_name", DP.DpName);
                message.data.Add("DP_web", DP.Website);
                message.data.Add("DP_contact", DP.ContactMethod);
                message.data.Add("DP_photo", DP.DpPicUrl);

                if (DP.AddressId != null)
                {
                    YixunAddress address = ctx.YixunAddresses.SingleOrDefault(b => b.AddressId == DP.AddressId);
                    //message.data.Add("DP_province", address.ProvinceId);
                    message.data.Add("DP_city", address.CityId);
                    //message.data.Add("DP_area", address.AreaId);
                    message.data.Add("DP_address", address.Detail);
                }
                else
                    message.data.Add("DP_address", DP.AddressId);
                message.status = true;  //status表示状态，修改为成功，原来默认是false
                message.errorCode = 200;        //修改状态码，默认的300表示找不到指定资源，200表示请求成功，400表示服务器未能处理
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }

            return message.ReturnJson();
        }

        //@Author：刘睿萌
        [HttpPost("PutDP")]
        public string PutDP(dynamic inputData)
        {
            MessageFormat message = new();  //MessageFormat是数据返回的类型，见MessageFormat.cs文件
            try
            {
                string act_province = inputData.GetProperty("province").ToString();//null
                string act_city = inputData.GetProperty("city").ToString();
                string act_area = inputData.GetProperty("area").ToString();
                string act_address = inputData.GetProperty("address").ToString();
                string web = inputData.GetProperty("website").ToString();
                string name = inputData.GetProperty("name").ToString();
                string contact = inputData.GetProperty("contact").ToString();
                int volInst_Id = int.Parse(inputData.GetProperty("admin").ToString());
                YixunRelatedDp activity = new YixunRelatedDp();
                activity.DpName = name;
                activity.ContactMethod = contact;
                activity.Website = web;
                activity.AdministratorId = volInst_Id;


                if (act_province != "")
                {
                    YixunAddress address = new YixunAddress();
                    address.Detail = act_address;
                    address.AreaId = act_area;
                    address.CityId = act_city;
                    address.ProvinceId = act_province;
                    ctx.YixunAddresses.Add(address);
                    ctx.SaveChanges();
                    var addressid = ctx.YixunAddresses.Select(s => s.AddressId).Max();

                    activity.AddressId = addressid;
                }

                ctx.YixunRelatedDps.Add(activity);
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

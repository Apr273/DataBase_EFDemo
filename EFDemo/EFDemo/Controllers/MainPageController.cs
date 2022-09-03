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
    public class MainPageController : ControllerBase
    {
        private readonly ModelContext ctx;// 创建一个模型上下文对象

        public MainPageController(ModelContext modelContext)// 构造函数，在每次调用该Controller中的api函数时，都会创建一个context对象用于操作数据库
        {
            ctx = modelContext;
        }
        //@Author：刘睿萌
        [HttpGet("GetAllSearchInfo")]
        public async Task<string> GetAllSearchInfo(int pageNum, int pageSize)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunSearchinfos.Count(searchInfo => searchInfo.Isactive == "Y"));
                var searchInfoList = await ctx.YixunSearchinfos
                .Where(searchInfo => searchInfo.Isactive == "Y")
                .OrderBy(searchInfo => searchInfo.SearchinfoId)
                .Skip((pageNum - 1) * pageSize)
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
        //@Author：刘睿萌
        [HttpPost("ScreenSearchInfo")]
        public async Task<string> ScreenSearchInfo(dynamic inputData)
        {
            MessageFormat message = new();
            try
            {

                string search_type_1 = inputData.GetProperty("search_type_1").ToString();
                string search_type_2 = inputData.GetProperty("search_type_2").ToString();
                string search_type_3 = inputData.GetProperty("search_type_3").ToString();
                string search_type_4 = inputData.GetProperty("search_type_4").ToString();
                string search_type_5 = inputData.GetProperty("search_type_5").ToString();
                string search_type_6 = inputData.GetProperty("search_type_6").ToString();
                string gender = inputData.GetProperty("gender").ToString();
                string birthday = inputData.GetProperty("birthday").ToString();
                string lostdate = inputData.GetProperty("lostdate").ToString();
                int height_low = int.Parse(inputData.GetProperty("height_low").ToString());
                int height_high = int.Parse(inputData.GetProperty("height_high").ToString());
                int pageNum = int.Parse(inputData.GetProperty("pageNum").ToString());
                int pageSize = int.Parse(inputData.GetProperty("pageSize").ToString());

                string search = inputData.GetProperty("search").ToString();//关键字
                string province = inputData.GetProperty("province").ToString();
                string city = inputData.GetProperty("city").ToString();
                string area = inputData.GetProperty("area").ToString();


                var predicate = PredicateBuilder.True<YixunSearchinfo>();
                predicate = predicate.And(x => x.Isactive == "Y");
                if (gender != "")
                {
                    predicate = predicate.And(x => x.SoughtPeopleGender == gender);
                }
                if (birthday != "")
                {
                    predicate = predicate.And(x => x.SoughtPeopleBirthday == Convert.ToDateTime(birthday));
                }
                if (lostdate != "")
                {
                    predicate = predicate.And(x => x.SearchinfoLostdate == Convert.ToDateTime(lostdate));
                }
                predicate = predicate.And(x => Convert.ToInt32(x.SoughtPeopleHeight) >= height_low && Convert.ToInt32(x.SoughtPeopleHeight) <= height_high);

                if (search != "")
                {
                    predicate = predicate.And(x => EF.Functions.Like(x.SoughtPeopleDetail, "%" + search + "%"));
                }
                if (province != "")
                {
                    predicate = predicate.And(x => x.AddressId != null && x.Address.ProvinceId == province);
                }
                if (city != "")
                {
                    predicate = predicate.And(x => x.AddressId != null && x.Address.CityId == province);
                }
                if (area != "")
                {
                    predicate = predicate.And(x => x.AddressId != null && x.Address.AreaId == province);
                }

                List<SearchInfoPublished> lists = new List<SearchInfoPublished>();
                var searchInfoList = await ctx.YixunSearchinfos
                .Where(predicate)
                .OrderBy(searchInfo => searchInfo.SearchinfoId)
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
                if (search_type_1 != "")
                {
                    foreach(var searchInfo in searchInfoList)
                    {
                        if(searchInfo.SearchType== "寻找亲人")
                        {
                            lists.Add(searchInfo);
                        }
                    }
                }
                if (search_type_2 != "")
                {
                    foreach (var searchInfo in searchInfoList)
                    {
                        if (searchInfo.SearchType == "亲人寻家")
                        {
                            lists.Add(searchInfo);
                        }
                    }
                }
                if (search_type_3 != "")
                {
                    foreach (var searchInfo in searchInfoList)
                    {
                        if (searchInfo.SearchType == "寻找朋友")
                        {
                            lists.Add(searchInfo);
                        }
                    }
                }
                if (search_type_4 != "")
                {
                    foreach (var searchInfo in searchInfoList)
                    {
                        if (searchInfo.SearchType == "寻找恩人")
                        {
                            lists.Add(searchInfo);
                        }
                    }
                }
                if (search_type_5 != "")
                {
                    foreach (var searchInfo in searchInfoList)
                    {
                        if (searchInfo.SearchType == "寻找战友")
                        {
                            lists.Add(searchInfo);
                        }
                    }
                }
                if (search_type_6 != "")
                {
                    foreach (var searchInfo in searchInfoList)
                    {
                        if (searchInfo.SearchType == "其它寻人")
                        {
                            lists.Add(searchInfo);
                        }
                    }
                }
                if(search_type_1 == ""&& search_type_2 == ""&& search_type_3 == ""&& search_type_4 == ""&& search_type_5 == ""&& search_type_6 == "")
                {
                    foreach (var searchInfo in searchInfoList)
                    {
                            lists.Add(searchInfo);
                    }
                }
                message.data.Add("total", lists.Count());
                var listss = lists.Skip((pageNum - 1) * pageSize).Take(pageSize);
                message.data.Add("searchInfo_list", listss.ToArray());

                int count = listss.Count();
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

        //接收前端传来的地址码，返回该地址码处的寻人信息
        // GET: api/<SearchInfoController>
        //@Author：刘睿萌，钟倩如
        [HttpGet("GetSearchInfoPos")]//设置路由
        public async Task<string> GetSearchInfoPos(string infoAd)
        {
            MessageFormat message = new();
            /*            string searchInfoPos = infoAd;*/
            try
            {
                if (infoAd != null)
                {
                    int count = 0;
                    List<SearchInfoDTO> searchInfoList = new List<SearchInfoDTO>();
                    var addressList = await ctx.YixunAddresses
                      .Where(b => b.AreaId == infoAd)
                      .Select(address => new AddressDTO
                      {
                          AddressId = address.AddressId,
                      }).ToListAsync();
                    if (addressList != null)
                    {
                        foreach(var a in addressList)
                        {
                            YixunSearchinfo searchinfo = await ctx.YixunSearchinfos.SingleOrDefaultAsync(b => b.AddressId == a.AddressId&& b.Isactive=="Y");
                            if(searchinfo!=null)
                            {
                                var search = new SearchInfoDTO();
                                search.search_info_id = searchinfo.SearchinfoId;
                                search.search_type = searchinfo.SearchType;
                                search.search_info_date = (searchinfo.SearchinfoDate!=null)?searchinfo.SearchinfoDate.Value.ToString("d"):null;
                                search.sought_people_state = searchinfo.SoughtPeopleState;
                                search.sought_people_name = searchinfo.SoughtPeopleName;
                                search.search_info_lostdate = searchinfo.SearchinfoLostdate.ToString("d");
                                search.sought_people_birthday = searchinfo.SoughtPeopleBirthday.ToString("d");
                                search.sought_people_gender = searchinfo.SoughtPeopleGender;
                                search.sought_people_height = searchinfo.SoughtPeopleHeight;
                                search.sought_people_detail = searchinfo.SoughtPeopleDetail;
                                search.isreport = searchinfo.Isreport;
                                YixunAddress address = await ctx.YixunAddresses.SingleOrDefaultAsync(b => b.AddressId == searchinfo.AddressId);
                                if(address!=null)
                                {
                                    search.province_id = address.ProvinceId;
                                    search.city_id = address.CityId;
                                    search.area_id = address.AreaId;
                                    search.detail = address.Detail;
                                  
                                }
                               
                               
                                search.search_info_photourl = searchinfo.SearchinfoPhotoUrl;
                                search.contact_method = searchinfo.ContactMethod;

                                searchInfoList.Add(search);
                                count++;
                                if (count == 20)
                                    break;
                            }

                        }
                    }
                    message.data.Add("aroundSearchInfo", searchInfoList.ToArray());
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

        //已发布项目，寻人信息总数
        //@Author：钟倩如
        [HttpGet("GetSearchInfoNum")]//设置路由
        public async Task<string> GetSearchInfoNum()
        {
            var allSearchInfo = await ctx.YixunSearchinfos.ToListAsync();
            return allSearchInfo.Count.ToString();
        }

        //已获得线索，线索总数
        //@Author：钟倩如
        [HttpGet("GetCluesNum")]//设置路由
        public async Task<string> GetCluesNum()
        {
            var allClues = await ctx.YixunClues.ToListAsync();
            return allClues.Count.ToString();
        }

        //累计已帮助，就是已经找到的信息
        //@Author：钟倩如
        [HttpGet("GetFoundInfoNum")]//设置路由
        public async Task<string> GetFoundInfoNum()
        {
            var allFoundInfo = await ctx.YixunSearchinfos.Where(b => b.SoughtPeopleState == "已找到").ToListAsync();
            return allFoundInfo.Count.ToString();
        }
    }
}

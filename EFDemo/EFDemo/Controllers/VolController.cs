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
    public class VolController : ControllerBase
    {
        private readonly ModelContext ctx;// 创建一个模型上下文对象

        public VolController(ModelContext modelContext)// 构造函数，在每次调用该Controller中的api函数时，都会创建一个context对象用于操作数据库
        {
            ctx = modelContext;
        }
        //@Author：梁厚
        [HttpGet("ShowVolActivityList")] 
        public async Task<string> ShowVolActivityList(int pageNum, int pageSize)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunVolActivities.Count(activity => activity.SignupPeople < activity.Needpeople && activity.ExpTime > System.DateTime.Now));
                var VolActivityList = await ctx.YixunVolActivities.Where(activity => activity.SignupPeople < activity.Needpeople && activity.ExpTime > System.DateTime.Now)
                .OrderBy(activity => activity.VolActId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .Select(activity => new VolActivityDTO
                {
                    VolActId = activity.VolActId,
                    VolActName = activity.VolActName,
                    ExpTime = activity.ExpTime.ToString("U"),
                    Province = activity.Address.ProvinceId,
                    City = activity.Address.CityId,
                    Area = activity.Address.AreaId,
                    Detail = activity.Address.Detail,
                    Needpeople = activity.Needpeople,
                    ActPicUrl = activity.ActPicUrl,
                    ContactMethod = activity.ContactMethod,
                    SignupPeople = activity.SignupPeople
                }).ToListAsync();

                message.data.Add("activity_list", VolActivityList.ToArray());

                message.status = true;
                message.errorCode = 200;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }

        //@Author：梁厚
        [HttpGet("ShowSingleVolActivity")]    
        public async Task<string> ShowSingleVolActivity(int VolActId)
        {
            MessageFormat message = new();
            try
            {
                YixunVolActivity activity = await ctx.YixunVolActivities.SingleOrDefaultAsync(b => b.VolActId == VolActId);
                message.data.Add("activity_id", activity.VolActId);
                message.data.Add("activity_name", activity.VolActName);
                message.data.Add("activity_pic", activity.ActPicUrl);
                message.data.Add("activity_needpeople", activity.Needpeople);
                message.data.Add("activity_signupPeople", activity.SignupPeople);
                message.data.Add("activity_expTime", activity.ExpTime.ToString());
                message.data.Add("activity_contactMethod", activity.ContactMethod);
                message.data.Add("activity_content", activity.ActContent);
                YixunVolInst inst = await ctx.YixunVolInsts.SingleOrDefaultAsync(b => b.VolInstId == activity.VolInstId);
                message.data.Add("activity_inst", inst.InstName);
                var is_overdue = (activity.ExpTime > System.DateTime.Now) ? true : false;
                message.data.Add("is_overdue", is_overdue);
                if (activity.AddressId != null)
                {
                    YixunAddress address = ctx.YixunAddresses.SingleOrDefault(b => b.AddressId == activity.AddressId);
                    message.data.Add("activity_province", address.ProvinceId);
                    message.data.Add("activity_city", address.CityId);
                    message.data.Add("activity_area", address.AreaId);
                    message.data.Add("activity_address", address.Detail);
                }
                else
                {
                    message.data.Add("activity_province", null);
                    message.data.Add("activity_city", null);
                    message.data.Add("activity_area", null);
                    message.data.Add("activity_address", null);
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

        //@Author：梁厚
        [HttpGet("ShowAllVolInstitution")] 
        public async Task<string> ShowAllVolInstitution(int pageNum, int pageSize)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunVolInsts.Count());
                var VolInstList = await ctx.YixunVolInsts
                .OrderBy(inst => inst.VolInstId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .Select(inst => new VolInstDTO
                {
                    VolInstId = inst.VolInstId,
                    InstName = inst.InstName,
                    FundationTime = inst.FundationTime.ToString("d"),
                    Province = inst.Address.ProvinceId,
                    City = inst.Address.CityId,
                    Area = inst.Address.AreaId,
                    Detail = inst.Address.Detail,
                    InstSlogan = inst.InstSlogan,
                    PeopleCount = inst.PeopleCount,
                    ContactMethod = inst.ContactMethod,
                    InstPicUrl=inst.InstPicUrl,
                }).ToListAsync();

                message.data.Add("volInst_list", VolInstList.ToArray());

                message.status = true;
                message.errorCode = 200;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }

        //@Author：梁厚
        [HttpGet("ShowSingleVolInstitution")]    //设置路由（路径）
        //还没有图片
        public async Task<string> ShowSingleVolInstitution(int VolInstId)
        {
            MessageFormat message = new();
            try
            {
                YixunVolInst inst = await ctx.YixunVolInsts.SingleOrDefaultAsync(b => b.VolInstId == VolInstId);
                message.data.Add("inst_id", inst.VolInstId);
                message.data.Add("inst_name", inst.InstName);
                message.data.Add("inst_pic", inst.InstPicUrl);
                message.data.Add("inst_fundationTime", inst.FundationTime.ToString("d"));
                message.data.Add("inst_contactMethod", inst.ContactMethod);
                message.data.Add("inst_credUrl", inst.VolInstCredUrl);
                message.data.Add("inst_InstSlogan", inst.InstSlogan);
                message.data.Add("inst_Introduce", inst.VolInstIntroduce);

                if (inst.AddressId != null)
                {
                    YixunAddress address = ctx.YixunAddresses.SingleOrDefault(b => b.AddressId == inst.AddressId);
                    message.data.Add("inst_province", address.ProvinceId);
                    message.data.Add("inst_city", address.CityId);
                    message.data.Add("inst_area", address.AreaId);
                    message.data.Add("inst_address", address.Detail);
                }
                else
                {
                    message.data.Add("inst_province", null);
                    message.data.Add("inst_city", null);
                    message.data.Add("inst_area", null);
                    message.data.Add("inst_address", null);
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

        //@Author：梁厚
        [HttpGet("ShowTenVolunteer")]    //设置路由（路径）
        public string ShowTenVolunteer()
        {
            MessageFormat message = new();
            try
            {
                const int TopItem = 10;
                var VolunteerList =
                    from Volunteer in ctx.YixunVolunteers
                    join User in ctx.YixunWebUsers
                        on Volunteer.VolUserId equals User.UserId
                    orderby Volunteer.VolTime descending
                    select new { VolunteerTime = Volunteer.VolTime, UserName = User.UserName, UserHeadUrl=User.UserHeadUrl };

                if (VolunteerList.Count() < TopItem)
                {
                    message.data.Add("AllVolActivity_list", VolunteerList.ToList());
                }
                else
                    message.data.Add("AllVolActivity_list", VolunteerList.Take(10).ToList());

                message.status = true;
                message.errorCode = 200;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }

        //@Author：梁厚
        [HttpGet("IfApplyActivity")]
        public async Task<string> IfApplyActivity(int vol_id,int volAct_id)
        {
            MessageFormat message = new();
            try
            {
                var ifApply=ctx.YixunVolActivities.Where(i =>i.VolActId== volAct_id&& i.YixunRecruiteds.Any(i => i.VolId == vol_id)).Count();
                if(ifApply==1)
                {
                    message.data.Add("is_applied", "true");
                }
                else
                {
                    message.data.Add("is_applied", "false");
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
        [HttpPost("SearchVolActivity")]
        public async Task<string> SearchVolActivity(dynamic inputData)
        {
            MessageFormat message = new();
            try
            {
                string search = inputData.GetProperty("search").ToString();
                int pageNum = int.Parse(inputData.GetProperty("pageNum").ToString());
                int pageSize = int.Parse(inputData.GetProperty("pageSize").ToString());
                if (search!="")
                {
                    message.data.Add("total", ctx.YixunVolActivities.Count(activity => EF.Functions.Like(activity.VolActName, "%" + search + "%")));
                    var VolActivityList = await ctx.YixunVolActivities.Where(activity => EF.Functions.Like(activity.VolActName, "%" + search + "%"))
                   .OrderBy(activity => activity.VolActId)
                   .Skip((pageNum - 1) * pageSize)
                   .Take(pageSize)
                   .Select(activity => new VolActivityDTO
                   {
                       VolActId = activity.VolActId,
                       VolActName = activity.VolActName,
                       ExpTime = activity.ExpTime.ToString("U"),
                       Province = activity.Address.ProvinceId,
                       City = activity.Address.CityId,
                       Area = activity.Address.AreaId,
                       Detail = activity.Address.Detail,
                       Needpeople = activity.Needpeople,
                       ActPicUrl = activity.ActPicUrl,
                       ContactMethod = activity.ContactMethod,
                       SignupPeople = activity.SignupPeople,
                   }).ToListAsync();

                        message.data.Add("activity_list", VolActivityList.ToArray());
                    int count = VolActivityList.Count;
                    if (count > 0)
                        message.data.Add("getcount", count);
                    else
                        message.data.Add("getcount", 0);
                }
                else
                {
                    message.data.Add("total", ctx.YixunVolActivities.Count());
                    var VolActivityList = await ctx.YixunVolActivities
                   .OrderBy(activity => activity.VolActId)
                   .Select(activity => new VolActivityDTO
                   {
                       VolActId = activity.VolActId,
                       VolActName = activity.VolActName,
                       ExpTime = activity.ExpTime.ToString("U"),
                       Province = activity.Address.ProvinceId,
                       City = activity.Address.CityId,
                       Area = activity.Address.AreaId,
                       Detail = activity.Address.Detail,
                       Needpeople = activity.Needpeople,
                       ActPicUrl = activity.ActPicUrl,
                       ContactMethod = activity.ContactMethod,
                       SignupPeople = activity.SignupPeople
                   }).ToListAsync();

                    message.data.Add("activity_list", VolActivityList.ToArray());
                    int count = VolActivityList.Count;
                    if (count > 0)
                        message.data.Add("getcount", count);
                    else
                        message.data.Add("getcount", 0);
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
        [HttpPost("SearchVolInst")]
        public async Task<string> SearchVolInst(dynamic inputData)
        {
            MessageFormat message = new();
            try
            {
                string search = inputData.GetProperty("search").ToString();
                int pageNum = int.Parse(inputData.GetProperty("pageNum").ToString());
                int pageSize = int.Parse(inputData.GetProperty("pageSize").ToString());
                if (search!="")
                {
                    message.data.Add("total", ctx.YixunVolInsts.Count(inst => EF.Functions.Like(inst.InstName, "%" + search + "%")));
                    var VolInstList = await ctx.YixunVolInsts.Where(inst => EF.Functions.Like(inst.InstName, "%" + search + "%"))
                    .OrderBy(inst => inst.VolInstId)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .Select(inst => new VolInstDTO
                    {
                        VolInstId = inst.VolInstId,
                        InstName = inst.InstName,
                        FundationTime = inst.FundationTime.ToString("d"),
                        Province = inst.Address.ProvinceId,
                        City = inst.Address.CityId,
                        Area = inst.Address.AreaId,
                        Detail = inst.Address.Detail,
                        InstSlogan = inst.InstSlogan,
                        PeopleCount = inst.PeopleCount,
                        ContactMethod = inst.ContactMethod,
                        InstPicUrl = inst.InstPicUrl,
                    }).ToListAsync();
                        message.data.Add("volInst_list", VolInstList.ToArray());
                    int count = VolInstList.Count;
                    if (count > 0)
                        message.data.Add("getcount", count);
                    else
                        message.data.Add("getcount", 0);
                }
                else
                {
                    var VolInstList = await ctx.YixunVolInsts
                    .OrderBy(inst => inst.VolInstId)
                    .Select(inst => new VolInstDTO
                    {
                        VolInstId = inst.VolInstId,
                        InstName = inst.InstName,
                        FundationTime = inst.FundationTime.ToString("d"),
                        Province = inst.Address.ProvinceId,
                        City = inst.Address.CityId,
                        Area = inst.Address.AreaId,
                        Detail = inst.Address.Detail,
                        InstSlogan = inst.InstSlogan,
                        PeopleCount = inst.PeopleCount,
                        ContactMethod = inst.ContactMethod,
                        InstPicUrl = inst.InstPicUrl,
                    }).ToListAsync();
                    message.data.Add("volInst_list", VolInstList.ToArray());
                    int count = VolInstList.Count;
                    if (count > 0)
                        message.data.Add("getcount", count);
                    else
                        message.data.Add("getcount", 0);
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
        //@Author：梁厚
        [HttpPost("UserApplyVolunteer")]    //设置路由（路径）
        //还没有图片
        public string UserApplyVolunteer(dynamic inputData)
        {
            MessageFormat message = new();  //MessageFormat是数据返回的类型，见MessageFormat.cs文件
            try
            {
                //parse用于转换类型，int.Parse()表示转为int;
                int UserId = int.Parse(inputData.GetProperty("UserId").ToString());
                string Specialty = inputData.GetProperty("Specialty").ToString();
                string Background = inputData.GetProperty("Background").ToString();
                string Career = inputData.GetProperty("Career").ToString();
                //取YixunWebUser类中where YiXunWebUser.UserId == user_id的数据给user对象
                //如果没有，user是null，后面不会向message的data字典里面加输出信息

                //随机分配一个管理员
                var admin_id = ctx.YixunAdministrators.Where(x => 1 == 1).OrderBy(x => Guid.NewGuid()).First();

                YixunVolApply NewApply = new YixunVolApply();
                //NewAddNews.NewsId = ;
                NewApply.UserId = UserId;
                NewApply.Specialty = Specialty;
                NewApply.Background = Background;
                NewApply.Career = Career;
                NewApply.AdministratorId = admin_id.AdministratorId;

                //EFEntities 可以理解成EF的上下文数据操作类，负责与数据库打交道。

                //将数据添加到EF,并且标记为添加标记，返回受影响的行数。
                ctx.YixunVolApplies.Add(NewApply);
                ctx.SaveChanges();      //保存更改
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
        [HttpGet("IsReviewApply")]    //设置路由（路径）
        public string IsReviewApply(int user_id)
        {
            MessageFormat message = new();  //MessageFormat是数据返回的类型，见MessageFormat.cs文件
            try
            {
                //判断是否申请
                var ThisUserApply = ctx.YixunVolApplies.Where(x => x.UserId == user_id).FirstOrDefault();
                if (ThisUserApply != null)
                {
                    if (ThisUserApply.Isreviewed == "N")
                        message.data.Add("ApplyHistory", 1);
                    else
                    {
                        if (ThisUserApply.Ispass == "N")
                            message.data.Add("ApplyHistory", 2);
                        else
                            message.data.Add("ApplyHistory", 3);
                    }
                }
                else
                    message.data.Add("ApplyHistory", 0);

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
        [HttpGet("SignupOrCancelVolActivity")]    //设置路由（路径）
        public string SignupOrCancelVolActivity(int VolId, int VolActId)
        {
            MessageFormat message = new();
            try
            {
                //找到这条招募信息
                var ThisRecruit = ctx.YixunRecruiteds.Where(x => x.VolActId == VolActId && x.VolId == VolId).FirstOrDefault();

                //在Activity表中找到这个志愿者参加的活动
                var VolunteerInAct = ctx.YixunVolActivities.Where(x => x.VolActId == VolActId).First();

                //原来已报名（有这条记录）
                if (ThisRecruit != null)
                {
                    VolunteerInAct.SignupPeople--;//取消报名，人数减少
                    ctx.YixunRecruiteds.Remove(ThisRecruit);//从recruit表中删去这条数据
                    message.data.Add("ApplyState", false);
                }
                else
                {
                    YixunRecruited NewRecruit = new YixunRecruited();
                    NewRecruit.VolActId = VolActId;
                    NewRecruit.VolId = VolId;
                    ctx.YixunRecruiteds.Add(NewRecruit);
                    VolunteerInAct.SignupPeople++;//取消报名，人数减少
                    message.data.Add("ApplyState", true);
                }
                ctx.Update(VolunteerInAct);
                ctx.SaveChanges();      //保存更改
                message.status = true;
                message.errorCode = 200;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }

        //@Author：韩淑榕
        [HttpGet("IfVolFocllowup")]
        public string IfVolFocllowup(int volid, int infoid)
        {
            MessageFormat message = new();
            try
            {
                if (ctx.YixunSearchinfoFollowups.Count(i => i.VolId == volid && i.SearchinfoId == infoid) > 0)
                {
                    message.data.Add("result", "Y");
                }
                else
                {
                    message.data.Add("result", "N");
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
        //@Author：韩淑榕
        [HttpPost("VolFocllowup")]
        public async Task<string> VolFocllowup(int volid, int infoid)
        {
            MessageFormat message = new();
            try
            {
                if (ctx.YixunSearchinfoFollowups.Count(i => i.VolId == volid && i.SearchinfoId == infoid) > 0)
                {
                    //待加入新增的follow吗？？
                    var followup = ctx.YixunSearchinfoFollowups.First(i => i.VolId == volid && i.SearchinfoId == infoid);
                    ctx.Remove(followup);
                }
                else
                {
                    var vol = ctx.YixunVolunteers.Single(i => i.VolId == volid);
                    var info = ctx.YixunSearchinfos.Single(i => i.SearchinfoId == infoid);
                    var followup = new YixunSearchinfoFollowup { VolId = volid, SearchinfoId = infoid };
                    ctx.YixunSearchinfoFollowups.Add(followup);

                }
                await ctx.SaveChangesAsync();
                message.status = true;
                message.errorCode = 200;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }
        //@Author：韩淑榕
        [HttpGet("IfVolRecruited")]
        public string IfVolRecruited(int volid, int actid)
        {
            MessageFormat message = new();
            try
            {
                if (ctx.YixunRecruiteds.Count(i => i.VolId == volid && i.VolActId == actid) > 0)
                {
                    message.data.Add("result", "Y");
                }
                else
                {
                    message.data.Add("result", "N");
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
        //@Author：韩淑榕
        [HttpPost("VolRecruited")]
        public async Task<string> VolRecruited(int volid, int actid)
        {
            MessageFormat message = new();
            try
            {
                if (ctx.YixunRecruiteds.Count(i => i.VolId == volid && i.VolActId == actid) > 0)
                {
                    var act = ctx.YixunVolActivities.Single(i => i.VolActId == actid);
                    act.SignupPeople--;
                    ctx.YixunVolActivities.Update(act);
                    var recruit = ctx.YixunRecruiteds.First(i => i.VolId == volid && i.VolActId == actid);

                    ctx.Remove(recruit);
                }
                else
                {
                    var act = ctx.YixunVolActivities.Single(i => i.VolActId == actid);
                    if (act.SignupPeople < act.Needpeople)
                    {
                        var vol = ctx.YixunVolunteers.Single(i => i.VolId == volid);
                        var recruit = new YixunRecruited { VolId = volid, VolActId = actid };
                        ctx.YixunRecruiteds.Add(recruit);
                        act.SignupPeople++;
                        ctx.YixunVolActivities.Update(act);
                    }
                }
                await ctx.SaveChangesAsync();
                message.status = true;
                message.errorCode = 200;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }
        //@Author：韩淑榕
        [HttpPut("VolAddVolTime")]
        public async Task<string> VolAddVolTime(int volid, short addtime)
        {
            MessageFormat message = new();
            try
            {
                var vol = ctx.YixunVolunteers.Single(i => i.VolId == volid);
                vol.VolTime += addtime;
                ctx.YixunVolunteers.Update(vol);
                await ctx.SaveChangesAsync();
                message.status = true;
                message.errorCode = 200;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }

        //@Author：韩淑榕
        [HttpGet("GetFollowUpInfo")]
        public async Task<string> GetFollowUpInfo(int volid, int pagenum, int pagesize)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunSearchinfoFollowups.Count(i => i.VolId == volid));
                var infos = await ctx.YixunSearchinfoFollowups.Where(i => i.VolId == volid)
                .OrderBy(i => i.SearchinfoId).Skip((pagenum - 1) * pagesize).Take(pagesize)
                .Select(i => new SearchInfoDTO_H
                {
                    search_info_id = i.SearchinfoId,
                    search_type = i.Searchinfo.SearchType,
                    search_info_date = i.Searchinfo.SearchinfoDate,
                    sought_people_state = i.Searchinfo.SoughtPeopleState,
                    sought_people_name = i.Searchinfo.SoughtPeopleName,
                    search_info_lostdate = i.Searchinfo.SearchinfoLostdate.ToString("d"),
                    sought_people_birthday = i.Searchinfo.SoughtPeopleBirthday.ToString("d"),
                    sought_people_gender = i.Searchinfo.SoughtPeopleGender,
                    sought_people_height = i.Searchinfo.SoughtPeopleHeight,
                    sought_people_detail = i.Searchinfo.SoughtPeopleHeight,
                    isreport = i.Searchinfo.Isreport,
                    province_id = (i.Searchinfo.Address != null) ? i.Searchinfo.Address.ProvinceId : null!,
                    city_id = (i.Searchinfo.Address != null) ? i.Searchinfo.Address.CityId : null!,
                    area_id = (i.Searchinfo.Address != null) ? i.Searchinfo.Address.AreaId : null!,
                    detail = (i.Searchinfo.Address != null) ? i.Searchinfo.Address.Detail : null!,
                    search_info_photourl = i.Searchinfo.SearchinfoPhotoUrl,
                    contact_method = i.Searchinfo.ContactMethod
                })
                .ToListAsync();

                message.data.Add("followup_info", infos.ToArray());

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
        //@Author：韩淑榕
        [HttpGet("GetVolAct")]
        public async Task<string> GetVolAct(int volid, int pagenum, int pagesize)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunRecruiteds.Count(i => i.VolId == volid));
                var acts = await ctx.YixunRecruiteds.Where(i => i.VolId == volid)
                .OrderBy(act => act.VolActId).Skip((pagenum - 1) * pagesize).Take(pagesize)
                .Select(act => new VolActInfoDTO_H
                {
                    vol_act_id = act.VolActId,
                    vol_act_name = act.VolAct.VolActName,
                    vol_act_content = act.VolAct.ActContent,
                    vol_act_time = act.VolAct.ExpTime.ToString("U"),
                    province_id = (act.VolAct.Address != null) ? act.VolAct.Address.ProvinceId : null!,
                    city_id = (act.VolAct.Address != null) ? act.VolAct.Address.CityId : null!,
                    area_id = (act.VolAct.Address != null) ? act.VolAct.Address.AreaId : null!,
                    detail = (act.VolAct.Address != null) ? act.VolAct.Address.Detail : null!,
                    need_people = act.VolAct.Needpeople,
                    actpicurl = act.VolAct.ActPicUrl,
                    contact_method = act.VolAct.ContactMethod,
                })
                .ToListAsync();

                message.data.Add("vol_act_info", acts.ToArray());
                int count = acts.Count;
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

    }
}

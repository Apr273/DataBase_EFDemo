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
    public class AdministratorController : ControllerBase
    {
        private readonly ModelContext ctx;// 创建一个模型上下文对象

        public AdministratorController(ModelContext modelContext)// 构造函数，在每次调用该Controller中的api函数时，都会创建一个context对象用于操作数据库
        {
            ctx = modelContext;
        }

        //1.1 发布咨询
        //@Author：刘睿萌
        [HttpPost("ReleaseNews")]
        public async Task<string> ReleaseNews(dynamic inputData)
        {
            MessageFormat message = new(); 
            try
            {
                int admin_id = int.Parse(inputData.GetProperty("admin_id").ToString());
                string news_content = inputData.GetProperty("news_content").ToString();
                string news_title = inputData.GetProperty("news_title").ToString();
                string news_type = inputData.GetProperty("news_type").ToString();

                YixunNews NewAddNews = new YixunNews();
                NewAddNews.AdministratorId = admin_id;
                NewAddNews.NewsContent = news_content;
                NewAddNews.NewsHeadlines = news_title;
                NewAddNews.NewsType = news_type;
                ctx.YixunNews.Add(NewAddNews);
                await ctx.SaveChangesAsync();
                var NewsId = ctx.YixunNews.Select(s => s.NewsId).Max();
                message.data.Add("news_id", NewsId);
            
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
        [HttpPut("AddNewsCover")]
        public string AddNewsCover(dynamic inputdata)
        {
            MessageFormat message = new();
            try
            {
                int news_id = int.Parse(inputdata.GetProperty("news_id").ToString());
                YixunNews news = ctx.YixunNews.Single(b => b.NewsId == news_id);
                string img_base64 = inputdata.GetProperty("news_cover").ToString();
                string type = "." + img_base64.Split(',')[0].Split(';')[0].Split('/')[1];
                img_base64 = img_base64.Split("base64,")[1];
                byte[] img_bytes = Convert.FromBase64String(img_base64);
                var client = OSSHelper.createClient();
                MemoryStream stream = new MemoryStream(img_bytes, 0, img_bytes.Length);
                string path = "news_cover/" + news_id.ToString() + type;
                client.PutObject(OSSHelper.bucketName, path, stream); // 直接覆盖
                string imgurl = "https://yixun-picture.oss-cn-shanghai.aliyuncs.com/" + path;
                news.NewsTitlepagesUrl = imgurl;
                ctx.YixunNews.Update(news);
                ctx.SaveChanges();
                message.data.Add("news_cover", imgurl);
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
        [HttpGet("GetAllVolInst")]
        public async Task<string> GetAllVolInst()
        {

            MessageFormat message = new();
            try
            {

                var volInstList = await ctx.YixunVolInsts
                .OrderBy(volInst => volInst.VolInstId)
                .Select(volInst => new VolInst
                {
                    VolInstId = volInst.VolInstId,
                    InstName = volInst.InstName,
                }).ToListAsync();
                message.data.Add("clue_list", volInstList.ToArray());
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
        [HttpPost("ReleaseVolActivity")]
        public string ReleaseVolActivity(dynamic inputData)
        {
            MessageFormat message = new();
            try
            {
                string act_name = inputData.GetProperty("act_name").ToString();
                string act_content = inputData.GetProperty("act_content").ToString();
                string act_time = inputData.GetProperty("act_time").ToString();
                short need_people = short.Parse(inputData.GetProperty("need_people").ToString());
                string act_province = inputData.GetProperty("act_province").ToString();//null
                string act_city = inputData.GetProperty("act_city").ToString();
                string act_area = inputData.GetProperty("act_area").ToString();
                string act_address = inputData.GetProperty("act_address").ToString();
                string contact_method = inputData.GetProperty("contact_method").ToString();
                int volInst_Id = int.Parse(inputData.GetProperty("volInst_Id").ToString());
                YixunVolActivity activity = new YixunVolActivity();
                activity.VolActName = act_name;
                activity.ActContent = act_content;
                activity.ExpTime = Convert.ToDateTime(act_time); 
                activity.ContactMethod = contact_method;
                activity.Needpeople = need_people;
                activity.VolInstId = volInst_Id;
                if (act_province!="")
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

                ctx.YixunVolActivities.Add(activity);
                ctx.SaveChanges();

                //YixunVolActivity _activity = ctx.YixunVolActivities.Single(b => b.VolActName == act_name && b.VolInstId == volInst_Id);
                var volActId = ctx.YixunVolActivities.Select(s => s.VolActId).Max();
                message.data.Add("volAct_id", volActId);

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
        [HttpPut("AddVolActivityPic")]
        public string AddVolActivityPic(dynamic inputdata)
        {
            MessageFormat message = new();
            try
            {
                int volAct_id = int.Parse(inputdata.GetProperty("volAct_id").ToString());
                YixunVolActivity activity = ctx.YixunVolActivities.Single(b => b.VolActId == volAct_id);
                string img_base64 = inputdata.GetProperty("volAct_pic").ToString();
                string type = "." + img_base64.Split(',')[0].Split(';')[0].Split('/')[1];
                img_base64 = img_base64.Split("base64,")[1];
                byte[] img_bytes = Convert.FromBase64String(img_base64);
                var client = OSSHelper.createClient();
                MemoryStream stream = new MemoryStream(img_bytes, 0, img_bytes.Length);
                string path = "volAct_pic/" + volAct_id.ToString() + type;
                client.PutObject(OSSHelper.bucketName, path, stream); // 直接覆盖
                string imgurl = "https://yixun-picture.oss-cn-shanghai.aliyuncs.com/" + path;
                activity.ActPicUrl = imgurl;
                ctx.YixunVolActivities.Update(activity);
                ctx.SaveChanges();
                message.data.Add("volAct_pic", imgurl);
                message.errorCode = 200;
                message.status = true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            return message.ReturnJson();
        }
        //@Author：韩淑榕
        [HttpGet("GetUserByName")]
        public async Task<string> GetUserByName(string word, int pagenum, int pagesize)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunWebUsers.Count(user => user.UserName.Contains(word)));
                var usersList = await ctx.YixunWebUsers
                .Where(user => user.UserName.Contains(word))
                .OrderBy(user => user.UserId).Skip((pagenum - 1) * pagesize).Take(pagesize)
                .Select(user => new NorUserInfoDTO_H
                {
                    isactive = user.Isactive,
                    user_id = user.UserId,
                    user_name = user.UserName,
                    user_state = user.UserState,
                    fundation_time = user.FundationTime,
                    phone_num = user.PhoneNum,
                    clue_report_num = user.YixunCluesReports.Count,
                    info_report_num = user.YixunInfoReports.Count,
                    clue_num = user.YixunClues.Count(i => i.Isactive == "Y"),
                    search_info_num = user.YixunSearchinfos.Count(i => i.Isactive == "Y"),
                }).ToListAsync();
                message.data.Add("user_info", usersList.ToArray());

                int count = usersList.Count;
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
        [HttpGet("GetAllNorUser")]
        public async Task<string> GetAllNorUser(int pagenum, int pagesize)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunWebUsers.Count());
                var usersList = await ctx.YixunWebUsers
                .Where(User => User.UserId > (pagenum - 1) * pagesize).Take(pagesize)
                .Select(user => new NorUserInfoDTO_H
                {
                    isactive = user.Isactive,
                    user_id = user.UserId,
                    user_name = user.UserName,
                    user_state = user.UserState,
                    fundation_time = user.FundationTime,
                    phone_num = user.PhoneNum,
                    clue_report_num = user.YixunCluesReports.Count,
                    info_report_num = user.YixunInfoReports.Count,
                    clue_num = user.YixunClues.Count(i => i.Isactive == "Y"),
                    search_info_num = user.YixunSearchinfos.Count(i => i.Isactive == "Y"),
                }).ToListAsync();
                message.data.Add("user_info", usersList.ToArray());

                int count = usersList.Count;
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
        [HttpGet("GetAllVol")]
        public async Task<string> GetAllVol(int pagenum, int pagesize)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunVolunteers.Count());
                var volList = await ctx.YixunVolunteers
                 .Where(vol => vol.VolId > (pagenum - 1) * pagesize).Take(pagesize)
                 .Select(vol => new VolUserInfoDTO_H
                 {
                     user_state = vol.VolUser.UserState,
                     user_id = vol.VolUser.UserId,
                     user_name = vol.VolUser.UserName,
                     fundation_time = vol.VolUser.FundationTime,
                     phone_num = vol.VolUser.PhoneNum,
                     mail_num = vol.VolUser.MailboxNum,
                     vol_id = vol.VolId,
                     vol_time = vol.VolTime,
                     info_followup_num = vol.YixunSearchinfoFollowups.Count,
                     act_num = vol.YixunRecruiteds.Count

                 }).ToListAsync();
                message.data.Add("vol_info", volList.ToArray());

                int count = volList.Count;
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
        [HttpGet("GetAllNews")]
        public async Task<string> GetAllNews(int pagenum, int pagesize)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunNews.Count());
                var newsList = await ctx.YixunNews
                .Where(news => news.NewsId > (pagenum - 1) * pagesize).Take(pagesize)
                 .Select(news => new NewsInfoDTO_H
                 {
                     Isactive = news.Isactive,
                     news_id = news.NewsId,
                     news_title = news.NewsHeadlines,
                     administrator_id = news.AdministratorId,
                     news_type = news.NewsType,
                     news_time = news.NewsTime
                 }).ToListAsync();
                message.data.Add("news_info", newsList.ToArray());

                int count = newsList.Count;
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
        [HttpGet("GetVolApplyCount")]
        public async Task<string> GetVolApplyCount(int adminId)
        {
            MessageFormat message = new();
            try
            {
                var volApply = await ctx.YixunAdministrators.Where(i => i.AdministratorId == adminId)
                .Select(e => new VolApplyDTO_H
                {
                    vol_apply_reviewed = e.YixunVolApplies.Count(i => i.Isreviewed == "Y"),
                    vol_apply_notreviewed = e.YixunVolApplies.Count(i => i.Isreviewed == "N")
                })
                .ToListAsync();
                message.data.Add("vol_apply_review", volApply.ToArray());
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
        [HttpGet("GetVolApplyReviewed")]
        public async Task<string> GetVolApplyReviewed(int adminId, int pagenum, int pagesize, string review)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunVolApplies.Count(i => i.AdministratorId == adminId && i.Isreviewed == review));
                var volApply = await ctx.YixunVolApplies.Where(i => i.AdministratorId == adminId && i.Isreviewed == review)
                .OrderBy(apply => apply.VolApplyId).Skip((pagenum - 1) * pagesize).Take(pagesize)
                .Select(apply => new VolApplyInfoDTO_H
                {
                    vol_apply_id = apply.VolApplyId,
                    user_state = apply.User.UserState,
                    user_id = apply.User.UserId,
                    user_name = apply.User.UserName,
                    career = apply.Career,
                    specialty = apply.Specialty,
                    background = apply.Background,
                    application_time = apply.ApplicationTime,
                    ispass = apply.Ispass

                })
                .ToListAsync();
                message.data.Add("vol_apply", volApply.ToArray());


                int count = volApply.Count;
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
        [HttpGet("GetInfoRepoCount")]
        public async Task<string> GetInfoRepoCount(int adminId)
        {
            MessageFormat message = new();
            try
            {
                var inforepo = await ctx.YixunAdministrators.Where(i => i.AdministratorId == adminId)
                .Select(e => new InfoRepoDTO_H
                {
                    info_repo_reviewed = e.YixunInfoReports.Count(i => i.Isreviewed == "Y"),
                    info_repo_notreviewed = e.YixunInfoReports.Count(i => i.Isreviewed == "N")
                })
                .ToListAsync();
                message.data.Add("info_repo_review", inforepo.ToArray());
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
        [HttpGet("GetInfoRepoReviewed")]
        public async Task<string> GetInfoRepoReviewed(int adminId, int pagenum, int pagesize, string review)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunInfoReports.Count(i => i.AdministratorId == adminId && i.Isreviewed == review));
                var inforepo = await ctx.YixunInfoReports.Where(i => i.AdministratorId == adminId && i.Isreviewed == review)
                .OrderBy(info => info.InfoReportId).Skip((pagenum - 1) * pagesize).Take(pagesize)
                .Select(repo => new InfoRepoInfoDTO_H
                {
                    info_repo_id = repo.InfoReportId,
                    user_id = repo.UserId,
                    search_info_id = repo.SearchinfoId,
                    repo_content = repo.ReportContent,
                    repo_time = repo.ReportTime,
                    ispass = repo.Ispass,
                })
                .ToListAsync();
                message.data.Add("info_repo", inforepo.ToArray());

                int count = inforepo.Count;
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
        [HttpGet("GetClueRepoCount")]
        public async Task<string> GetClueRepoCount(int adminId)
        {
            MessageFormat message = new();
            try
            {
                var cluerepo = await ctx.YixunAdministrators.Where(i => i.AdministratorId == adminId)
                .Select(e => new ClueRepoDTO_H
                {
                    clue_repo_reviewed = e.YixunCluesReports.Count(i => i.Isreviewed == "Y"),
                    clue_repo_notreviewed = e.YixunCluesReports.Count(i => i.Isreviewed == "N")
                })
                .ToListAsync();
                message.data.Add("clue_repo_review", cluerepo.ToArray());
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
        [HttpGet("GetClueRepoReviewed")]
        public async Task<string> GetClueRepoReviewed(int adminId, int pagenum, int pagesize, string review)
        {
            MessageFormat message = new();
            try
            {
                message.data.Add("total", ctx.YixunCluesReports.Count(i => i.AdministratorId == adminId && i.Isreviewed == review));
                var cluerepo = await ctx.YixunCluesReports.Where(i => i.AdministratorId == adminId && i.Isreviewed == review)
                .OrderBy(clue => clue.ClueReportId).Skip((pagenum - 1) * pagesize).Take(pagesize)
                .Select(repo => new ClueRepoInfoDTO_H
                {
                    clue_repo_id = repo.ClueReportId,
                    user_id = repo.UserId,
                    clue_id = repo.ClueId,
                    repo_content = repo.ReportContent,
                    repo_time = repo.ReportTime
                })
                .ToListAsync();
                message.data.Add("clue_repo", cluerepo.ToArray());

                int count = cluerepo.Count;
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
        [HttpGet("GetReviewCount")]
        public async Task<string> GetReviewCount(int adminId)
        {
            MessageFormat message = new();
            try
            {
                var reviews = await ctx.YixunAdministrators.Where(i => i.AdministratorId == adminId)
                .Select(e => new ReviewedCountDTO_H
                {
                    vol_apply_reviewed = e.YixunVolApplies.Count(i => i.Isreviewed == "Y"),
                    vol_apply_notreviewed = e.YixunVolApplies.Count(i => i.Isreviewed == "N"),

                    info_repo_reviewed = e.YixunInfoReports.Count(i => i.Isreviewed == "Y"),
                    info_repo_notreviewed = e.YixunInfoReports.Count(i => i.Isreviewed == "N"),

                    clue_repo_reviewed = e.YixunCluesReports.Count(i => i.Isreviewed == "Y"),
                    clue_repo_notreviewed = e.YixunCluesReports.Count(i => i.Isreviewed == "N")
                })
                .ToListAsync();
                message.data.Add("review_count", reviews.ToArray());
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
        [HttpPut("BanUser")]
        public async Task<string> BanUser(int userid)
        {
            MessageFormat message = new();
            try
            {
                var user = ctx.YixunWebUsers.Single(i => i.UserId == userid);
                if (user.UserState == "N")
                {
                    user.UserState = "Y";
                }
                else
                {
                    user.UserState = "N";
                }
                ctx.YixunWebUsers.Update(user);
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
        [HttpDelete("DeleteUser")]
        public async Task<string> DeleteUser(int userid)
        {
            MessageFormat message = new();
            try
            {
                var user = ctx.YixunWebUsers.Single(i => i.UserId == userid);
                user.UserName = "账号已注销";
                user.UserState = "N";
                user.Isactive = "N";
                ctx.YixunWebUsers.Update(user);
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
        [HttpDelete("DeleteNews")]
        public async Task<string> DeleteNews(int newsid)
        {
            MessageFormat message = new();
            try
            {
                var news = ctx.YixunNews.Single(i => i.NewsId == newsid);
                news.Isactive = "N";
                ctx.YixunNews.Update(news);
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
        [HttpPut("PassVolApply")]
        public async Task<string> PassVolApply(int volapplyid)
        {
            MessageFormat message = new();
            try
            {
                var apply = ctx.YixunVolApplies.Single(i => i.VolApplyId == volapplyid);
                var volapply = await ctx.YixunVolApplies.Where(i => i.UserId == apply.UserId)
                .ToListAsync();
                foreach (var appl in volapply)
                {
                    appl.Isreviewed = "Y";
                    appl.Ispass = "Y";
                    ctx.YixunVolApplies.Update(appl);
                }
                var vol = new YixunVolunteer { VolUserId = apply.UserId };
                ctx.YixunVolunteers.Add(vol);
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
        [HttpPut("DenyVolApply")]
        public async Task<string> DenyVolApply(int volapplyid)
        {
            MessageFormat message = new();
            try
            {
                var apply = ctx.YixunVolApplies.Single(i => i.VolApplyId == volapplyid);
                apply.Isreviewed = "Y";
                apply.Ispass = "N";
                ctx.YixunVolApplies.Update(apply);
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
        [HttpPut("PassClueRepo")]
        public async Task<string> PassClueRepo(int clueId)
        {
            MessageFormat message = new();
            try
            {
                var clue = ctx.YixunClues.Single(i => i.ClueId == clueId);
                clue.Isactive = "N";
                ctx.YixunClues.Update(clue);
                var cluerepo = ctx.YixunCluesReports.Where(i => i.ClueId == clueId)
                .ToList();
                foreach (var repo in cluerepo)
                {
                    repo.Isreviewed = "Y";
                    repo.Ispass = "Y";
                    ctx.YixunCluesReports.Update(repo);
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
        [HttpPut("DenyClueRepo")]
        public async Task<string> DenyClueRepo(int cluerepoid)
        {
            MessageFormat message = new();
            try
            {
                var cluerepo = ctx.YixunCluesReports.Single(i => i.ClueReportId == cluerepoid);
                cluerepo.Isreviewed = "Y";
                cluerepo.Ispass = "N";
                ctx.YixunCluesReports.Update(cluerepo);
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
        [HttpDelete("PassInfoRepo")]
        public async Task<string> PassInfoRepo(int infoid)
        {
            MessageFormat message = new();
            try
            {
                var info = ctx.YixunSearchinfos.Single(i => i.SearchinfoId == infoid);
                info.Isactive = "N";
                ctx.YixunSearchinfos.Update(info);
                var inforepo = await ctx.YixunInfoReports.Where(i => i.SearchinfoId == infoid)
                .ToListAsync();
                foreach (var repo in inforepo)
                {
                    repo.Isreviewed = "Y";
                    repo.Ispass = "Y";
                    ctx.YixunInfoReports.Update(repo);
                }

                ctx.BulkDelete(ctx.YixunSearchinfoFoci.Where(i => i.SearchinfoId == infoid));
                ctx.BulkDelete(ctx.YixunSearchinfoFollowups.Where(i => i.SearchinfoId == infoid));

                var clues = await ctx.YixunClues.Where(i => i.SearchinfoId == infoid)
                .ToListAsync();
                foreach (var clue in clues)
                {
                    clue.Isactive = "N";
                    var cluerepo = await ctx.YixunCluesReports.Where(i => i.ClueId == clue.ClueId)
                .ToListAsync();
                    foreach (var repo in cluerepo)
                    {
                        repo.Isreviewed = "Y";
                        repo.Ispass = "Y";
                        ctx.YixunCluesReports.Update(repo);
                    }
                    ctx.YixunClues.Update(clue);
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
        [HttpPost("DenyInfoRepo")]
        public async Task<string> DenyInfoRepo(int inforepoid)
        {
            MessageFormat message = new();
            try
            {
                var inforepo = ctx.YixunInfoReports.Single(i => i.InfoReportId == inforepoid);
                inforepo.Isreviewed = "Y";
                inforepo.Ispass = "N";
                ctx.YixunInfoReports.Update(inforepo);
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


    }
}

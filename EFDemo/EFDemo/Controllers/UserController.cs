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
    public class UserController : ControllerBase
    {
        private readonly ModelContext ctx;
        public UserController(ModelContext context)
        {
            ctx = context;
        }

        [HttpGet("IfIsVol")]
        public string IfIsVol(int userid)
        {
            MessageFormat message = new();
            try
            {
                if (ctx.YixunWebUsers.Include(i=>i.YixunVolunteer).Single(i=>i.UserId==userid).YixunVolunteer!=null)
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



        [HttpGet("IfUserFocus")]
        public string IfUserFocus(int userid, int infoid)
        {
            MessageFormat message = new();
            try
            {
                if (ctx.YixunSearchinfoFoci.Count(i => i.UserId == userid && i.SearchinfoId == infoid) > 0)
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

        [HttpGet("UserFocus")]
        public async Task<string> UserFocus(int userid, int infoid)
        {
            MessageFormat message = new();
            try
            {
                if (ctx.YixunSearchinfoFoci.Count(i => i.UserId == userid && i.SearchinfoId == infoid) > 0)
                {
                    var focus = ctx.YixunSearchinfoFoci.First(i => i.UserId == userid && i.SearchinfoId == infoid);
                    ctx.Remove(focus);
                    message.data.Add("state", false);
                }
                else
                {
                    var user = ctx.YixunWebUsers.Single(i => i.UserId == userid);
                    var info=ctx.YixunSearchinfos.Single(i => i.SearchinfoId == infoid);
                    var focus = new YixunSearchinfoFocus { UserId=userid, SearchinfoId=infoid };
                    ctx.YixunSearchinfoFoci.Add(focus);
                    message.data.Add("state", true);
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

        [HttpDelete("UserDeleteInfo")]
        public async Task<string> UserDeleteInfo(int userid, int infoid)
        {
            MessageFormat message = new();
            try
            {
                var info = ctx.YixunSearchinfos.Single(i => i.SearchinfoId == infoid && i.UserId==userid);
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

                ctx.BulkDelete(ctx.YixunSearchinfoFoci.Where(i=>i.SearchinfoId==infoid));
                ctx.BulkDelete(ctx.YixunSearchinfoFollowups.Where(i=>i.SearchinfoId==infoid));
          
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

        [HttpDelete("UserDeleteClue")]
        public async Task<string> UserDeleteClue(int userid, int clueid)
        {
            MessageFormat message = new();
            try
            {
                var clue = ctx.YixunClues.Single(i => i.ClueId == clueid && i.UserId==userid);
                clue.Isactive = "N";
                ctx.YixunClues.Update(clue);
                var cluerepo = ctx.YixunCluesReports.Where(i => i.ClueId == clueid)
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



    }
}
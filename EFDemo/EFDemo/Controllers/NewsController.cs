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
    public class NewsController : ControllerBase
    {
        private readonly ModelContext ctx;// 创建一个模型上下文对象

        public NewsController(ModelContext modelContext)// 构造函数，在每次调用该Controller中的api函数时，都会创建一个context对象用于操作数据库
        {
            ctx = modelContext;
        }

        [HttpGet("GetAllNews")]
        public async Task<string> GetAllNews(string news_type,int pageNum, int pageSize)
        {

            MessageFormat message = new();
            try
            {
                if(news_type=="全部")
                {
                    var total = ctx.YixunNews.Count(news => news.Isactive == "Y");
                    message.data.Add("total", total);
                    var NewsList = await ctx.YixunNews.Where(news=>news.Isactive=="Y")
                    .OrderBy(news => news.NewsId)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .Select(news => new NewsDTO
                    {
                        NewsId = news.NewsId,
                        NewsContent = news.NewsContent,
                        Title = news.NewsHeadlines,
                        Cover = news.NewsTitlepagesUrl
                    }).ToListAsync();
                    message.data.Add("news_list", NewsList.ToArray());
                    int count = NewsList.Count;
                    if (count > 0)
                        message.data.Add("getcount", count);
                    else
                        message.data.Add("getcount", 0);
                }
                else
                {
                    var total = ctx.YixunNews.Count(news => news.NewsType == news_type&& news.Isactive == "Y");
                    message.data.Add("total", total);
                    var NewsList = await ctx.YixunNews.Where(news => news.NewsType == news_type&& news.Isactive == "Y")
                    .OrderBy(news => news.NewsId)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .Select(news => new NewsDTO
                    {
                        NewsId = news.NewsId,
                        NewsContent = news.NewsContent,
                        Title = news.NewsHeadlines,
                        Cover = news.NewsTitlepagesUrl
                    }).ToListAsync();
                    message.data.Add("news_list", NewsList.ToArray());
                    int count = NewsList.Count;
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

        [HttpGet("GetNewsDetail")]    //设置路由（路径）
        public async Task<string> GetNewsDetail(int news_id)
        {
            MessageFormat message = new();  //MessageFormat是数据返回的类型，见MessageFormat.cs文件
            try
            {
                YixunNews news = await ctx.YixunNews.SingleOrDefaultAsync(b => b.NewsId == news_id&& b.Isactive == "Y");
                message.data.Add("news_id", news.NewsId);
                message.data.Add("news_content", news.NewsContent);
                message.data.Add("news_time", news.NewsTime);
                message.data.Add("news_title", news.NewsHeadlines);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }

            return message.ReturnJson();
        }

        [HttpPost("SearchNews")]
        public async Task<string> SearchNews(dynamic inputData)
        {
            MessageFormat message = new();
            try
            {
                string search = inputData.GetProperty("search").ToString();
                int pageNum = int.Parse(inputData.GetProperty("pageNum").ToString());
                int pageSize = int.Parse(inputData.GetProperty("pageSize").ToString());
                if (search != "")
                {
                    message.data.Add("total", ctx.YixunNews.Count(news => news.Isactive == "Y"&&(EF.Functions.Like(news.NewsHeadlines, "%" + search + "%")|| EF.Functions.Like(news.NewsContent, "%" + search + "%"))));
                    var NewsList = await ctx.YixunNews.Where(news => news.Isactive == "Y"&&( EF.Functions.Like(news.NewsHeadlines, "%" + search + "%") || EF.Functions.Like(news.NewsContent, "%" + search + "%")))
                    .OrderBy(news => news.NewsId)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .Select(news => new NewsDTO
                    {
                        NewsId = news.NewsId,
                        NewsContent = news.NewsContent,
                        Title = news.NewsHeadlines,
                        Cover = news.NewsTitlepagesUrl
                    }).ToListAsync();
                    message.data.Add("news_list", NewsList.ToArray());
                    int count = NewsList.Count;
                    if (count > 0)
                        message.data.Add("getcount", count);
                    else
                        message.data.Add("getcount", 0);
                }
                else
                {
                    message.data.Add("total", ctx.YixunNews.Count(news=>news.Isactive == "Y"));
                    var NewsList = await ctx.YixunNews.Where(news => news.Isactive == "Y")
                    .OrderBy(news => news.NewsId)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .Select(news => new NewsDTO
                    {
                        NewsId = news.NewsId,
                        NewsContent = news.NewsContent,
                        Cover = news.NewsHeadlines,
                        Title = news.NewsTitlepagesUrl
                    }).ToListAsync();
                    message.data.Add("news_list", NewsList.ToArray());
                    int count = NewsList.Count;
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
    }
}

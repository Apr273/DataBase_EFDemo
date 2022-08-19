using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDemo.Models; // 使用Models

namespace EFDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //解决跨域问题
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                        builder =>
                        {
                            builder.AllowAnyMethod()
                                .SetIsOriginAllowed(_ => true)
                                .AllowAnyHeader()
                                .AllowCredentials();
                        }));
            //services.AddCors(option => option.AddPolicy("cors", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().AllowAnyOrigin()));
            //注册Swagger生成器，定义一个或多个Swagger文件
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "YiXunWebAPI Api",
                    Version = "v1"
                });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "YiXunWebAPI Api");//这个就是刚刚配置的xml文件名
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath, true);
                }

            });
            services.AddControllers();

            services.AddScoped<ModelContext>(); 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //跨域处理
            app.UseCors("CorsPolicy");

            //启用中间件Swagger()
            app.UseSwagger();
            //启用中间件Swagger()的UI服务，他需要与Swagger()配置在一起
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "YiXunWebAPI Api V1");
            });
            app.UseRouting();
            //添加授权中间件
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

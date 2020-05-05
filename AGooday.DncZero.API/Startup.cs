using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AGooday.DncZero.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string ApiName = "AGooday.DncZero";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // 注册Swagger生成器，定义一个或多个Swagger文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",// 版本
                    Title = $"{ApiName} 接口文档",// 标题
                    Description = $"{ApiName} HTTP API",// 描述
                    TermsOfService = new Uri("https://example.com/terms"),// 服务条款
                    // 作者
                    Contact = new OpenApiContact
                    {
                        Name = ApiName,
                        Email = string.Empty,
                        Url = new Uri("https://github.com/xiangyisheng/agooday.dnczero"),
                    },
                    // 许可证
                    License = new OpenApiLicense
                    {
                        Name = ApiName,
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // 设置Swagger JSON和UI的注释路径。
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";// 反射用于生成与 Web API 项目相匹配的 XML 文件名。XML 文件生成：项目属性 => 生成 => 勾选 XML 文档文件
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // AppContext.BaseDirectory属性用于构造 XML 文件的路径。
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 启用中间件能够将生成的Swagger用作JSON端点。
            app.UseSwagger();
            //启用中间件能够为swagger ui（HTML、JS、CSS等）提供服务，指定Swagger JSON端点。
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ApiName} V1");
                // 路径配置，设置为空，表示直接在根域名（http://localhost:<port>/）访问该文件，注意localhost:<port>/swagger是访问不到的，去launchSettings.json把launchUrl去掉
                //c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

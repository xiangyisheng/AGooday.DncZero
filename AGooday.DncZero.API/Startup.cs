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

            // ע��Swagger������������һ������Swagger�ĵ�
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",// �汾
                    Title = $"{ApiName} �ӿ��ĵ�",// ����
                    Description = $"{ApiName} HTTP API",// ����
                    TermsOfService = new Uri("https://example.com/terms"),// ��������
                    // ����
                    Contact = new OpenApiContact
                    {
                        Name = ApiName,
                        Email = string.Empty,
                        Url = new Uri("https://github.com/xiangyisheng/agooday.dnczero"),
                    },
                    // ���֤
                    License = new OpenApiLicense
                    {
                        Name = ApiName,
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // ����Swagger JSON��UI��ע��·����
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";// �������������� Web API ��Ŀ��ƥ��� XML �ļ�����XML �ļ����ɣ���Ŀ���� => ���� => ��ѡ XML �ĵ��ļ�
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // AppContext.BaseDirectory�������ڹ��� XML �ļ���·����
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

            // �����м���ܹ������ɵ�Swagger����JSON�˵㡣
            app.UseSwagger();
            //�����м���ܹ�Ϊswagger ui��HTML��JS��CSS�ȣ��ṩ����ָ��Swagger JSON�˵㡣
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ApiName} V1");
                // ·�����ã�����Ϊ�գ���ʾֱ���ڸ�������http://localhost:<port>/�����ʸ��ļ���ע��localhost:<port>/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ��
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

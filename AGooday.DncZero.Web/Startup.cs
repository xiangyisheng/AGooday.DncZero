using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using AGooday.DncZero.Web.Extensions;
using AGooday.DncZero.Common.Helper;
using AGooday.DncZero.Infrastructure.Identity.Data;
using AGooday.DncZero.Common.DB;
using AGooday.DncZero.Infrastructure.Identity.Models;
using AGooday.DncZero.Infrastructure.Identity.Authorization;

namespace AGooday.DncZero.Web
{
    public class Startup
    {
        /*
         һ��Ǩ����Ŀ1��һ��Ҫ�л��� AGooday.DncZero.Infrastructure ��Ŀ�£�ʹ�� Package Manager Console����
           1��add-migration InitStudentDb -Context DncZeroDbContext 
           2��add-migration InitEventStoreDb -Context EventStoreSQLContext -o Migrations/EventStore
           3��update-database -Context DncZeroDbContext
           4��update-database -Context EventStoreSQLContext

         ����Ǩ����Ŀ2��һ��Ҫ�л��� AGooday.DncZero.Infrastructure.Identity ��Ŀ�£�ʹ�� Package Manager Console����
           1��add-migration InitIdentityDb -Context ApplicationDbContext -o Data/Migrations/ 
           2��update-database -Context ApplicationDbContext
             
        */
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton(new Appsettings(Env.ContentRootPath));

            services.AddDbContext<ApplicationDbContext>(options =>
               //options.UseSqlServer(BaseDBConfig.GetConnectionString(Configuration.GetConnectionString("DefaultConnectionFile"), Configuration.GetConnectionString("DefaultConnection")))
               options.UseSqlServer(BaseDBConfig.ConnectionString)
            );

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(o =>
            //    {
            //        o.LoginPath = new PathString("/login");
            //        o.AccessDeniedPath = new PathString("/home/access-denied");
            //    });
            //    //.AddFacebook(o =>
            //    //{
            //    //    o.AppId = Configuration["Authentication:Facebook:AppId"];
            //    //    o.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //    //})
            //    //.AddGoogle(googleOptions =>
            //    //{
            //    //    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
            //    //    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //    //});

            // Automapper ע��
            services.AddAutoMapperSetup();

            //services.AddControllersWithViews();
            services.AddMvc()
                //.AddRazorOptions(options =>
                //{
                //    //�޸� Razor �� ViewLocationFormats ���ϣ����Զ�����ͼ����·���� ���磬��������ӵ����ϣ�������·����/Components/{��ͼ�������}/{��ͼ����}���е���ͼ
                //    options.ViewLocationFormats.Add("/{0}.cshtml");//ռλ����{0}����ʾ·����Components/{��ͼ�������}/{��ͼ����}��
                //})
                ;

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("CanWriteUsersData", policy => policy.Requirements.Add(new ClaimRequirement("Users", "Write")));
            //    options.AddPolicy("CanRemoveUsersData", policy => policy.Requirements.Add(new ClaimRequirement("Users", "Remove")));
            //    options.AddPolicy("CanWriteOrRemoveUsersData", policy => policy.Requirements.Add(new ClaimRequirement("Users", "WriteOrRemove")));
            //});

            // Adding MediatR for Domain Events
            // ������������¼���ע��
            // ���ð� MediatR.Extensions.Microsoft.DependencyInjection
            //services.AddMediatR(typeof(MyxxxHandler));//����ע��ĳһ���������
            //��
            services.AddMediatR(typeof(Startup));//Ŀ����Ϊ��ɨ��Handler��ʵ�ֶ�����ӵ�IOC��������

            // .NET Core ԭ������ע��
            // ��дһ����������������չʾ�� Presentation �и���
            NativeInjectorBootStrapper.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

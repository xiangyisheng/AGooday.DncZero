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
         一、迁移项目1（一定要切换到 Christ3D.Infrastruct 项目下，使用 Package Manager Console）：
           1、add-migration InitStudentDb -Context StudyContext 
           2、add-migration InitEventStoreDb -Context EventStoreSQLContext -o Migrations/EventStore
           3、update-database -Context StudyContext
           4、update-database -Context EventStoreSQLContext

         二、迁移项目2（一定要切换到 Christ3D.Infrastruct.Identity 项目下，使用 Package Manager Console）：
           1、add-migration InitIdentityDb -Context ApplicationDbContext -o Data/Migrations/ 
           2、update-database -Context ApplicationDbContext
             
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
               //options.UseSqlServer(BaseDBConfig.GetConnectionString(Configuration.GetConnectionString("DefaultConnection_file"), Configuration.GetConnectionString("DefaultConnection")))
               options.UseSqlServer(BaseDBConfig.ConnectionString)
            );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(o =>
            //    {
            //        o.LoginPath = new PathString("/login");
            //        o.AccessDeniedPath = new PathString("/home/access-denied");
            //    })//;
            //    .AddFacebook(o =>
            //    {
            //        o.AppId = Configuration["Authentication:Facebook:AppId"];
            //        o.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //    })
            //    .AddGoogle(googleOptions =>
            //    {
            //        googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
            //        googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //    });

            // Automapper 注入
            services.AddAutoMapperSetup();

            //services.AddControllersWithViews();
            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanWriteUsersData", policy => policy.Requirements.Add(new ClaimRequirement("Users", "Write")));
                options.AddPolicy("CanRemoveUsersData", policy => policy.Requirements.Add(new ClaimRequirement("Users", "Remove")));
                options.AddPolicy("CanWriteOrRemoveUsersData", policy => policy.Requirements.Add(new ClaimRequirement("Users", "WriteOrRemove")));
            });

            // Adding MediatR for Domain Events
            // 领域命令、领域事件等注入
            // 引用包 MediatR.Extensions.Microsoft.DependencyInjection
            services.AddMediatR(typeof(Startup));

            // .NET Core 原生依赖注入
            // 单写一层用来添加依赖项，从展示层 Presentation 中隔离
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

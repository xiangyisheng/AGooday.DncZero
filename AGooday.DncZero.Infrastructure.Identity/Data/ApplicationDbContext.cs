using AGooday.DncZero.Common.DB;
using AGooday.DncZero.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AGooday.DncZero.Infrastructure.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //执行
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region
            //// get the configuration from the app settings
            //var config = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //// define the database to use
            //// 因为我使用的是txt文件，所以用的是 File.ReadAllText() ，如果你直接配置的是字符串，可以直接这么写：
            ////optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            //optionsBuilder.UseSqlServer(BaseDBConfig.GetConnectionString(config.GetConnectionString("DefaultConnectionFile"), config.GetConnectionString("DefaultConnection")));
            #endregion

            #region
            optionsBuilder
                // 安装NuGet包 Microsoft.EntityFrameworkCore.SqlServer
                .UseSqlServer(BaseDBConfig.ConnectionString);
            #endregion
        }
    }
}

using AGooday.DncZero.Common.DB;
using AGooday.DncZero.Domain.Models;
using AGooday.DncZero.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AGooday.DncZero.Infrastructure.Context
{
    public class DncZeroDbContext : DbContext
    {
        #region
        /// <summary>
        /// 用户信息
        /// </summary>
        public DbSet<Users> Users { get; set; }
        /// <summary>
        /// 用户授权
        /// </summary>
        public DbSet<UserAuths> UserAuths { get; set; }
        /// <summary>
        /// 用户组
        /// </summary>
        public DbSet<UserGroups> UserGroups { get; set; }
        /// <summary>
        /// 用户用户组关联
        /// </summary>
        public DbSet<UserGroupRelation> UserGroupRelation { get; set; }
        /// <summary>
        /// 用户角色关联
        /// </summary>
        public DbSet<UserRoleRelation> UserRoleRelation { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public DbSet<UserPermissionRelation> UserPermissionRelation { get; set; }
        /// <summary>
        /// 角色信息
        /// </summary>
        public DbSet<Roles> Roles { get; set; }
        /// <summary>
        /// 角色权限关联
        /// </summary>
        public DbSet<RolePermissionRelation> RolePermissionRelation { get; set; }
        /// <summary>
        /// 用户组角色关联
        /// </summary>
        public DbSet<GroupRoleRelation> GroupRoleRelation { get; set; }
        /// <summary>
        /// 权限项
        /// </summary>
        public DbSet<Permissions> Permissions { get; set; }
        /// <summary>
        /// 操作日志
        /// </summary>
        public DbSet<OperateLogs> OperateLogs { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        public DbSet<Menus> Menus { get; set; }
        /// <summary>
        /// 数据日志
        /// </summary>
        public DbSet<DataLogs> DataLogs { get; set; } 
        #endregion

        /// <summary>
        /// 重写自定义Map配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //对 UsersMap 进行配置
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new UserAuthsMap());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 重写连接数据库
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region
            //// 从 appsetting.json 中获取配置信息
            //var config = new ConfigurationBuilder()
            //    // 安装NuGet包 Microsoft.Extensions.Configuration
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    // 安装NuGet包 Microsoft.Extensions.Configuration.Json（支持JSON配置文件）
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //// 定义要使用的数据库
            //// 我是读取的文件内容，为了数据安全
            //optionsBuilder
            //    // 安装NuGet包 Microsoft.EntityFrameworkCore.SqlServer
            //    .UseSqlServer(BaseDBConfig.GetConnectionString(config.GetConnectionString("DefaultConnectionFile"), config.GetConnectionString("DefaultConnection")));
            #endregion

            #region
            optionsBuilder
                // 安装NuGet包 Microsoft.EntityFrameworkCore.SqlServer
                .UseSqlServer(BaseDBConfig.ConnectionString);
            #endregion
        }
    }
}

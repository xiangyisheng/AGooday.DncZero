﻿using AGooday.DncZero.Common.DB;
using AGooday.DncZero.Common.Enumerator;
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
        #region DbSets
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
        public DbSet<Groups> Groups { get; set; }
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
        /// 菜单
        /// </summary>
        public DbSet<Functions> Functions { get; set; }
        /// <summary>
        /// 数据日志
        /// </summary>
        public DbSet<DataLogs> DataLogs { get; set; }
        /// <summary>
        /// 登录日志
        /// </summary>
        public DbSet<LoginLogs> LoginLogs { get; set; }
        #endregion
        #region DbQuery
        public DbQuery<UserPermissions> UserPermissions { get; set; }
        #endregion
        //执行
        //public DncZeroDbContext(DbContextOptions<DncZeroDbContext> options)
        //    : base(options)
        //{
        //}
        /// <summary>
        /// 重写自定义Map配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //对 UsersMap 进行配置
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new UserAuthsMap());
            //modelBuilder.ApplyConfiguration(new GroupsMap());
            //modelBuilder.ApplyConfiguration(new UserGroupRelationMap());
            //modelBuilder.ApplyConfiguration(new UserRoleRelationMap());
            //modelBuilder.ApplyConfiguration(new UserPermissionRelationMap());
            //modelBuilder.ApplyConfiguration(new RolesMap());
            //modelBuilder.ApplyConfiguration(new RolePermissionRelationMap());
            //modelBuilder.ApplyConfiguration(new GroupRoleRelationMap());
            //modelBuilder.ApplyConfiguration(new PermissionsMap());
            //modelBuilder.ApplyConfiguration(new OperateLogsMap());
            //modelBuilder.ApplyConfiguration(new MenusMap());
            //modelBuilder.ApplyConfiguration(new FunctionsMap());
            //modelBuilder.ApplyConfiguration(new DataLogsMap());

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
            switch (BaseDBConfig.DbType)
            {
                case DataBaseType.MySql:
                    optionsBuilder.
                        // 安装NuGet包 Pomelo.EntityFrameworkCore.MySql
                        UseMySql(BaseDBConfig.ConnectionString);
                    break;
                case DataBaseType.SqlServer:
                    optionsBuilder
                        // 安装NuGet包 Microsoft.EntityFrameworkCore.SqlServer
                        .UseSqlServer(BaseDBConfig.ConnectionString);
                    break;
                case DataBaseType.Sqlite:
                    optionsBuilder.
                        // 安装NuGet包 Microsoft.EntityFrameworkCore.Sqlite
                        UseSqlite(BaseDBConfig.ConnectionString);
                    break;
                case DataBaseType.Oracle:
                    break;
                case DataBaseType.PostgreSQL:
                    break;
                default:
                    optionsBuilder.UseSqlServer(BaseDBConfig.ConnectionString);
                    break;
            }
            #endregion

            //base.OnConfiguring(optionsBuilder);
        }
    }
}

using AGooday.DncZero.Domain.Interfaces;
using AGooday.DncZero.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGooday.DncZero.Infrastructure.Repository
{
    /// <summary>
    /// 泛型仓储，实现泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class
        where TPrimaryKey : struct
    {
        protected readonly DncZeroDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DncZeroDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }
        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetById(TPrimaryKey id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }
        /// <summary>
        /// 根据对象进行更新
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }
        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
    public class Repository : IRepository
    {
        protected readonly DncZeroDbContext Db;
        public Repository(DncZeroDbContext context)
        {
            Db = context;
        }

        /// <summary>
        /// 是否拥有此权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
        public bool HasMenusAuthority(Guid userId, string url)
        {
            /// 检查是否是超级管理员,如果是超级管理员,则直接默认拥有所有权限
            if (Db.Users.Any(x => x.Id == userId && x.Type == 0))
            {
                return true;
            }
            var roleIds = Db.UserRoleRelation.Where(x => x.UserId == userId)
                .Select(x => x.RoleId).ToList();
            var userGroupRoleMenus = Db.UserGroupRelation
                .Join(Db.GroupRoleRelation, a => a.GroupId, b => b.GroupId, (a, b) => new { a, b })
                .Join(Db.RolePermissionRelation, c => c.b.RoleId, d => d.RoleId, (c, d) => new { a = c.a, b = c.b, c = d })
                .Join(Db.Permissions, e => e.c.PermissionId, f => f.Id, (e, f) => new { a = e.a, b = e.b, c = e.c, d = f })
                .Join(Db.Menus, g => g.d.ResourceId, h => h.Id, (g, h) => new { a = g.a, b = g.b, c = g.c, d = g.d, e = h })
                .Where(x => x.a.UserId == userId && x.d.ResourceType == Common.Enumerator.ResourceType.菜单).Select(x => x.e)
                .ToList();
            var userRoleMenus = Db.UserRoleRelation
                .Join(Db.RolePermissionRelation, a => a.RoleId, b => b.RoleId, (a, b) => new { a, b })
                .Join(Db.Permissions, c => c.b.PermissionId, d => d.Id, (c, d) => new { a = c.a, b = c.b, c = d })
                .Join(Db.Menus, e => e.c.ResourceId, f => f.Id, (e, f) => new { a = e.a, b = e.b, c = e.c, d = f })
                .Where(x => x.a.UserId == userId && x.c.ResourceType == Common.Enumerator.ResourceType.菜单).Select(x => x.d)
                .ToList();
            var userMenus = Db.UserPermissionRelation
                .Join(Db.Permissions, a => a.PermissionId, b => b.Id, (a, b) => new { a, b })
                .Join(Db.Menus, c => c.b.ResourceId, d => d.Id, (c, d) => new { a = c.a, b = c.b, c = d })
                .Where(x => x.a.UserId == userId && x.b.ResourceType == Common.Enumerator.ResourceType.菜单).Select(x => x.c)
                .ToList();

            var userAllMenus = new List<Domain.Models.Menus>();
            userAllMenus.AddRange(userGroupRoleMenus);
            userAllMenus.AddRange(userRoleMenus);
            userAllMenus.AddRange(userMenus);
            userAllMenus = userAllMenus.Distinct(p => p.Id).ToList();
            //.Any(x => x.b.ResourceType == Common.Enumerator.ResourceType.菜单 && url.StartsWith(x.c.Url));

            return userAllMenus.Any(x => url.StartsWith(x.Url));
        }
        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public class CommonEqualityComparer<T, V> : IEqualityComparer<T>
    {
        private Func<T, V> keySelector;

        public CommonEqualityComparer(Func<T, V> keySelector)
        {
            this.keySelector = keySelector;
        }

        public bool Equals(T x, T y)
        {
            return EqualityComparer<V>.Default.Equals(keySelector(x), keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return EqualityComparer<V>.Default.GetHashCode(keySelector(obj));
        }
    }
    public static class DistinctExtensions
    {
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }
    }
}

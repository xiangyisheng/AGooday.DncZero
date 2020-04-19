using AGooday.DncZero.Domain.Interfaces;
using AGooday.DncZero.Domain.Models;
using AGooday.DncZero.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGooday.DncZero.Infrastructure.Repository
{
    public class UsersRepository : Repository<Users,Guid>, IUsersRepository
    {
        public UsersRepository(DncZeroDbContext context)
            : base(context)
        {
        }

        public Users GetByName(string name)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Name == name);
        }

        public Users GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
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
            return GetAllMenus(userId).Any(x => url.StartsWith(x.Url));
        }

        /// <summary>
        /// 获取指定用户菜单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public IList<Menus> GetAllMenus(Guid userId)
        {
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
                .Join(Db.Functions, e => e.c.Id, f => f.MenuId, (e, f) => new { a = e.a, b = e.b, c = e.c, d = f })
                .Where(x => x.a.UserId == userId && x.b.ResourceType == Common.Enumerator.ResourceType.菜单).Select(x => x.c)
                .ToList();

            var userAllMenus = new List<Domain.Models.Menus>();
            userAllMenus.AddRange(userGroupRoleMenus);
            userAllMenus.AddRange(userRoleMenus);
            userAllMenus.AddRange(userMenus);
            userAllMenus = userAllMenus.Distinct(p => p.Id).ToList();

            return userAllMenus;
        }
    }
}

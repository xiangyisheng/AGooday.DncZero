﻿using AGooday.DncZero.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGooday.DncZero.Domain.Interfaces
{
    /// <summary>
    /// IUsersRepository 接口
    /// 注意，这里我们的对象，是领域对象
    /// </summary>
    public interface IUsersRepository : IRepository<Users, Guid>
    {
        /// <summary>
        /// 一些 Users 独有的接口
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Users GetByName(string name);
        Users GetByEmail(string email);
        [Obsolete("This function is obsolete", false)]
        Users Login(string identifier, string credential);
        Task<Users> LoginAsync(string identifier, string credential);
        [Obsolete("This function is obsolete", false)]
        Task<Users> RegisterAsync(Users user,UserAuths userauth);
        /// <summary>
        /// 是否拥有此权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
        bool HasMenusAuthority(Guid userId, string url);
        Users GetUserById(Guid userId);
        Task<Users> GetUserByIdAsync(Guid userId);
        List<Users> GetUsers();
        Task<List<Users>> GetUsersAsync();
        IList<Menus> GetAllMenus(Guid userId);
    }
}

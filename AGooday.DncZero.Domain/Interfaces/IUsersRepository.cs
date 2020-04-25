using AGooday.DncZero.Domain.Models;
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

        Task<Users> LoginAsync(string identifier, string credential);
        /// <summary>
        /// 是否拥有此权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
        bool HasMenusAuthority(Guid userId, string url);

        IList<Menus> GetAllMenus(Guid userId);
    }
}

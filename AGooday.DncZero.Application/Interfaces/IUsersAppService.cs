using AGooday.DncZero.Application.EventSourcedNormalizers.Users;
using AGooday.DncZero.Application.ViewModels;
using AGooday.DncZero.Domain.Communication;
using AGooday.DncZero.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGooday.DncZero.Application.Interfaces
{
    /// <summary>
    /// 定义 IUsersAppService 服务接口
    /// 并继承IDisposable，显式释放资源
    /// 注意这里我们使用的对象，是视图对象模型
    /// </summary>
    public interface IUsersAppService : IDisposable
    {
        IEnumerable<UsersViewModel> GetAll();
        Task<IEnumerable<UsersViewModel>> ListAsync();
        UsersViewModel GetById(Guid id);
        Task<UsersViewModel> FindByIdAsync(Guid id);
        void Update(UsersViewModel UsersViewModel);
        Task<Response<Users>> ModifyAsync(UsersViewModel UsersViewModel);
        void Remove(Guid id);
        IList<UsersHistoryData> GetAllHistory(Guid id);

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns></returns>
        Task<LoginResultViewModel> LoginAsync(LoginViewModel LoginViewModel);
        void Create(UsersViewModel UsersViewModel);
        Task<Response<Users>> RegisterAsync(UsersViewModel LoginViewModel);
        //void Register(UsersViewModel UsersViewModel, RegisterViewModel RegisterViewModel);
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns></returns>
        Task<UsersViewModel> RegisterAsync(RegisterViewModel RegisterViewModel);
        /// <summary>
        /// 是否拥有此权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
        bool HasMenusAuthority(Guid userId, string url);
        IList<MenusViewModel> GetAllMenus(Guid userId);
    }
}

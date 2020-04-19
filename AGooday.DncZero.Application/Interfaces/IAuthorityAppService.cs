using AGooday.DncZero.Application.EventSourcedNormalizers.Users;
using AGooday.DncZero.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Application.Interfaces
{
    /// <summary>
    /// 定义 IAuthorityAppService 服务接口
    /// 并继承IDisposable，显式释放资源
    /// 注意这里我们使用的对象，是视图对象模型
    /// </summary>
    public interface IAuthorityAppService : IDisposable
    {
        /// <summary>
        /// 是否拥有此权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
        bool HasMenusAuthority(Guid userId, string url);
    }
}

using AGooday.DncZero.Application.EventSourcedNormalizers.Users;
using AGooday.DncZero.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Application.Interfaces
{
    /// <summary>
    /// 定义 IUsersAppService 服务接口
    /// 并继承IDisposable，显式释放资源
    /// 注意这里我们使用的对象，是视图对象模型
    /// </summary>
    public interface IUsersAppService : IDisposable
    {
        void Register(UsersViewModel UsersViewModel);
        IEnumerable<UsersViewModel> GetAll();
        UsersViewModel GetById(Guid id);
        void Update(UsersViewModel UsersViewModel);
        void Remove(Guid id);
        IList<UsersHistoryData> GetAllHistory(Guid id);
    }
}

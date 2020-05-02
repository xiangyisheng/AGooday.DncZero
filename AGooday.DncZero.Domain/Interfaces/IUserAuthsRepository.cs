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
    public interface IUserAuthsRepository : IRepository<UserAuths, Guid>
    {
    }
}

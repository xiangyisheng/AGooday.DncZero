﻿using AGooday.DncZero.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
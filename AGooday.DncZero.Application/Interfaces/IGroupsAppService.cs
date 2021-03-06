﻿using AGooday.DncZero.Application.EventSourcedNormalizers.Users;
using AGooday.DncZero.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Application.Interfaces
{
    /// <summary>
    /// 定义 IGroupsAppService 服务接口
    /// 并继承IDisposable，显式释放资源
    /// 注意这里我们使用的对象，是视图对象模型
    /// </summary>
    public interface IGroupsAppService : IDisposable
    {
        IEnumerable<GroupsViewModel> GetAll();
    }
}

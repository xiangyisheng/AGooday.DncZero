﻿using AGooday.DncZero.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Interfaces
{
    /// <summary>
    /// 注意，这里我们的对象，是领域对象
    /// </summary>
    public interface IRolesRepository : IRepository<Roles, Guid>
    {
    }
}

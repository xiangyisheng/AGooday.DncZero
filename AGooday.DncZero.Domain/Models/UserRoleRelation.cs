using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 用户角色关联
    /// </summary>
    public class UserRoleRelation : Entity<Guid>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; private set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; private set; }
    }
}

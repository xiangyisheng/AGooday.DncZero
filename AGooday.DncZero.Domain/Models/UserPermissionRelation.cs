using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 用户权限关联（提供用户级别的定制权限）
    /// </summary>
    public class UserPermissionRelation : Entity<Guid>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; private set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public Guid PermissionId { get; private set; }
    }
}

using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 角色权限关联
    /// </summary>
    public class RolePermissionRelation : Entity<Guid>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; private set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public Guid PermissionId { get; private set; }
    }
}

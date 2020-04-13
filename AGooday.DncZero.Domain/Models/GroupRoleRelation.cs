using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 用户组角色关联
    /// </summary>
    public class GroupRoleRelation : Entity<Guid>
    {
        /// <summary>
        /// 用户组ID
        /// </summary>
        public Guid GroupId { get; private set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; private set; }
    }
}

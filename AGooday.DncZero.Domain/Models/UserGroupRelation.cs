using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 用户用户组关联
    /// </summary>
    public class UserGroupRelation : Entity<Guid>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; private set; }
        /// <summary>
        /// 用户组ID
        /// </summary>
        public Guid GroupId { get; private set; }
    }
}

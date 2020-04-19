using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 用户组
    /// </summary>
    public class Groups : SortableEntity<Guid, long>
    {
        /// <summary>
        /// 用户组名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 父用户组ID
        /// </summary>
        public Guid? ParentId { get; private set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }
    }
}

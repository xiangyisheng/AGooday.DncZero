using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class Roles : SortableEntity<Guid, long>
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 父角色ID
        /// </summary>
        public Guid ParentId { get; private set; }
        /// <summary>
        /// 描写(文字)
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; private set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; private set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public Guid CreatedByUserId { get; private set; }
        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime LastModifiedTime { get; private set; }
        /// <summary>
        /// 上次修改用户ID
        /// </summary>
        public Guid LastModifiedByUserId { get; private set; }
    }
}

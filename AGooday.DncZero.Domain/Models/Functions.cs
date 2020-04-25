using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    public class Functions : SortableEntity<Guid, long>
    {
        public Guid MenuId { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Icon { get; private set; }
        public string Description { get; private set; }
        public string Area { get; private set; }
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public string Url { get; private set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; private set; }
        /// <summary>
        /// 否是删除
        /// </summary>
        public bool IsDeleted { get; private set; }
        public int Type { get; private set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; private set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public Guid CreatedByUserId { get; private set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; private set; }
        /// <summary>
        /// 修改用户ID
        /// </summary>
        public Guid ModifiedByUserId { get; private set; }
    }
}

using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 权限项
    /// </summary>
    public class Permissions : Entity<Guid>
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid ResourceId { get; private set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public ResourceType ResourceType { get; private set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; private set; }
    }
    /// <summary>
    /// 资源类型
    /// </summary>
    public enum ResourceType
    {
        菜单 = 0,
        页面元素 = 1,
        数据字段 = 2,
        文件 = 3,
        功能操作 = 4,
    }
}

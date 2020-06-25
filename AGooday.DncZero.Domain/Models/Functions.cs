using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    public class Functions : SortableEntity<Guid, long>
    {
        public Guid MenuId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// 功能方法
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// Url地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 是否启用操作日志
        /// </summary>
        public bool OperateLogEnabled { get; set; }
        /// <summary>
        /// 是否启用数据日志
        /// </summary>
        public bool DataLogEnabled { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }
        /// <summary>
        /// 否是删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 功能类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public Guid CreatedByUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; }
        /// <summary>
        /// 修改用户ID
        /// </summary>
        public Guid ModifiedByUserId { get; set; }
    }
}

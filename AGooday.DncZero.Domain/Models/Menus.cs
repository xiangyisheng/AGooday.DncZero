using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menus : SortableEntity<Guid, long>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 父菜单Id
        /// </summary>
        public Guid? ParentId { get; private set; }
        /// <summary>
        /// 区域
        /// </summary>
        public Guid Area { get; private set; }
        /// <summary>
        /// 控制器
        /// </summary>
        public Guid Controller { get; private set; }
        /// <summary>
        /// 动作
        /// </summary>
        public Guid Action { get; private set; }
        /// <summary>
        /// Url地址
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; private set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; private set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; private set; }
        /// <summary>
        /// 否是删除
        /// </summary>
        public bool IsDeleted { get; private set; }
        /// <summary>
        /// 是否默认路由
        /// </summary>
        public bool IsDefaultRouter { get; private set; }
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
        /// <summary>
        /// 是否缓存
        /// </summary>
        public bool IsCache { get; private set; }
        /// <summary>
        /// 隐藏菜单
        /// </summary>
        public string HideInMenu { get; private set; }
        /// <summary>
        /// 关闭前执行方法
        /// </summary>
        public string BeforeCloseFun { get; private set; }

        /// <summary>
        /// 用户授权详情
        /// </summary>
        public virtual ICollection<Functions> Functions { get; private set; }
    }
}

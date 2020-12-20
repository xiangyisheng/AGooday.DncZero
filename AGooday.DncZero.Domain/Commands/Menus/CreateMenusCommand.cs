using AGooday.DncZero.Domain.Communication;
using AGooday.DncZero.Domain.Core.Commands;
using AGooday.DncZero.Domain.Models;
using AGooday.DncZero.Domain.Validations.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Commands.Menus
{
    /// <summary>
    /// 注册一个添加 Menus 命令
    /// 基础抽象菜单命令模型
    /// </summary>
    public class CreateMenusCommand : MenusCommand
    {
        // set 受保护，只能通过构造函数方法赋值
        public CreateMenusCommand(
              string name
            , Guid? parentid
            , string area
            , string controller
            , string action
            , string url
            , string alias
            , string icon
            , string description
            , int status
            , bool isdeleted
            , bool isdefaultrouter
            , DateTime createdtime
            , Guid createdbyuserid
            , DateTime modifiedtime
            , Guid modifiedbyuserid
            , bool iscache
            , bool ishideinmenu
            , string beforeclosefun
            , long? sort
            )
        {
            Name = name;
            ParentId = parentid;
            Area = area;
            Controller = controller;
            Action = action;
            Url = url;
            Alias = alias;
            Icon = icon;
            Description = description;
            Status = status;
            IsDeleted = isdeleted;
            IsDefaultRouter = isdefaultrouter;
            CreatedTime = createdtime;
            CreatedByUserId = createdbyuserid;
            ModifiedTime = modifiedtime;
            ModifiedByUserId = modifiedbyuserid;
            IsCache = iscache;
            IsHideInMenu = ishideinmenu;
            BeforeCloseFun = beforeclosefun;

            Sort = sort;
        }
        public Guid Id { get; protected set; }
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
        public string Area { get; private set; }
        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; private set; }
        /// <summary>
        /// 动作
        /// </summary>
        public string Action { get; private set; }
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
        /// 是否隐藏菜单
        /// </summary>
        public bool IsHideInMenu { get; private set; }
        /// <summary>
        /// 关闭前执行方法
        /// </summary>
        public string BeforeCloseFun { get; private set; }
        /// <summary>
        /// 排序
        /// </summary>
        public long? Sort { get; protected set; }

        /// <summary>
        /// 用户授权详情
        /// </summary>
        public virtual ICollection<Functions> Functions { get; private set; }
        // 重写基类中的 是否有效 方法
        // 主要是为了引入命令验证 CreateMenusCommandValidation。
        public override bool IsValid()
        {
            ValidationResult = new CreateMenusCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

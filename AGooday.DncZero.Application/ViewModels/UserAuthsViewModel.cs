using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AGooday.DncZero.Application.ViewModels
{
    public class UserAuthsViewModel
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [DisplayName("用户ID")]
        public Guid UserId { get; set; }
        /// <summary>
        /// 登录类型(手机号/邮箱/用户名)或第三方应用名称(微信/微博等)  身份类型(站内username 邮箱email 手机mobile 或者第三方的qq weibo weixin等等)
        /// </summary>
        [DisplayName("登录类型")]
        public string IdentityType { get; set; }
        /// <summary>
        /// 标识(手机号/邮箱/用户名或第三方应用的唯一标识)身份唯一标识(存储唯一标识，比如账号、邮箱、手机号、第三方获取的唯一标识等)
        /// </summary>
        [DisplayName("标识")]
        public string Identifier { get; set; }
        /// <summary>
        /// 密码凭证(站内的保存密码，站外的不保存或保存token)  授权凭证(比如密码 第三方登录的token等)
        /// </summary>
        [DisplayName("密码凭证")]
        public string Credential { get; set; }
        /// <summary>
        /// 状态 0:未启用 1:启用 2:禁用/0:未授权 1:已授权 2:取消授权
        /// </summary>
        [DisplayName("状态")]
        public int State { get; set; }
        /// <summary>
        /// 授权时间
        /// </summary>
        [DisplayName("授权时间")]
        public DateTime AuthTime { get; set; }
        /// <summary>
        /// 上次修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public DateTime LastModifiedTime { get; set; }
        /// <summary>
        /// 是否已经验证（存储 1、0 来区分是否已经验证通过）
        /// </summary>
        [DisplayName("是否已经验证")]
        public bool Verified { get; set; }

        public UsersViewModel User { get; set; }
    }
}
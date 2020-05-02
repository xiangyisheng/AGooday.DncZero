using AGooday.DncZero.Domain.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /*
     * JsonObject(IsReference = true)]适用于JSON.NET，[DataContract(IsReference = true)]适用于XmlDCSerializer。
     * 注意：DataContract在类上应用后，需要添加DataMember到要序列化的属性。
     * 这些属性可以同时应用于json和xml序列化程序，并提供了对模型类的更多控制。
     * 
     * **/
    /// <summary>
    /// 用户授权
    /// </summary>
    //[DataContract(IsReference = true)]
    //[JsonObject(IsReference = true)]
    public class UserAuths : Entity<Guid>
    {
        public UserAuths(Guid id)
        {
            Id = id;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 登录类型(手机号/邮箱/用户名)或第三方应用名称(微信/微博等)  身份类型(站内username 邮箱email 手机mobile 或者第三方的qq weibo weixin等等)
        /// </summary>
        public string IdentityType { get; set; }
        /// <summary>
        /// 标识(手机号/邮箱/用户名或第三方应用的唯一标识)身份唯一标识(存储唯一标识，比如账号、邮箱、手机号、第三方获取的唯一标识等)
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// 密码凭证(站内的保存密码，站外的不保存或保存token)  授权凭证(比如密码 第三方登录的token等)
        /// </summary>
        public string Credential { get; set; }
        /// <summary>
        /// 状态 0:未启用 1:启用 2:禁用/0:未授权 1:已授权 2:取消授权
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 授权时间
        /// </summary>
        public DateTime AuthTime { get; set; }
        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime LastModifiedTime { get; set; }
        /// <summary>
        /// 是否已经验证（存储 1、0 来区分是否已经验证通过）
        /// </summary>
        public bool Verified { get; set; }

        //控制模型或属性级别上的序列化行为。要忽略该属性
        [JsonIgnore]//JsonIgnore用于JSON.NET
        //[IgnoreDataMember] //用于XmlDCSerializer
        //[DataMember] 
        public virtual Users User { get; set; }
    }
}

using AGooday.DncZero.Common.Enumerator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AGooday.DncZero.Application.ViewModels
{
    public class LoginResultViewModel
    {
        /// <summary>
        /// 是否登陆成功
        /// </summary>
        public bool LoginSuccess { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 登陆结果
        /// </summary>
        public LoginResult Result { get; set; }

        /// <summary>
        /// 标识(手机号/邮箱/用户名或第三方应用的唯一标识)身份唯一标识(存储唯一标识，比如账号、邮箱、手机号、第三方获取的唯一标识等)
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// 返回的登陆用户数据
        /// </summary>
        public UsersViewModel User { get; set; }
    }
}

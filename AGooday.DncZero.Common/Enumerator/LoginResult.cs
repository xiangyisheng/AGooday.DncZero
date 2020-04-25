using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Common.Enumerator
{
    /// <summary>
    /// 登陆结果
    /// </summary>
    public enum LoginResult
    {
        /// <summary>
        /// 登陆成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 账号不存在
        /// </summary>
        AccountNotExists = 1,

        /// <summary>
        /// 登陆密码错误
        /// </summary>
        WrongPassword = 2,

        /// <summary>
        /// 账号或密码错误
        /// </summary>
        AccountOrPasswordWrong = 3
    }
}

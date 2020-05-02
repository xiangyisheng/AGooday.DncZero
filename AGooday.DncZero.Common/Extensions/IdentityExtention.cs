using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace AGooday.DncZero.Common.Extensions
{

    /// <summary>
    /// IIdentity扩展
    /// </summary>
    public static class IdentityExtention
    {
        /// <summary>
        /// 获取登录的用户ID
        /// </summary>
        /// <param name="identity">IIdentity</param>
        /// <returns></returns>
        public static string GetLoginUserId(this IIdentity identity)
        {
            var claim = (identity as ClaimsIdentity)?.FindFirst("UserId");
            if (claim != null)
            {
                return claim.Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取 Claim 值
        /// </summary>
        /// <param name="identity">IIdentity</param>
        /// <returns></returns>
        public static string GetClaimValue(this IIdentity identity, string type)
        {
            var claim = (identity as ClaimsIdentity)?.FindFirst(type);
            if (claim != null)
            {
                return claim.Value;
            }
            return string.Empty;
        }
    }
}

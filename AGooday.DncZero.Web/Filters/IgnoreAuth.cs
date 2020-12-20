using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGooday.DncZero.Web.Filters
{
    /// <summary>
    /// 忽略权限验证，不需要做权限验证的，就加上这个Attribute(Action和Controller都可以加)
    /// </summary>
    public class IgnoreAuth : AllowAnonymousAttribute
    {
    }
}

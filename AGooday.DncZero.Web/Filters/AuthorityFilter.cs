﻿using AGooday.DncZero.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace AGooday.DncZero.Web.Filters
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class AuthorityFilter : ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isIgnored = filterContext.ActionDescriptor.FilterDescriptors.Any(f => f.Filter is IgnoreAuth);

            if (isIgnored) return;

            //注意：引用 Microsoft.Extensions.DependencyInjection
            //var authorityAppService = filterContext.HttpContext.RequestServices.GetService<IAuthorityAppService>();
            var authorityAppService = filterContext.HttpContext.RequestServices.GetService<IUsersAppService>();

            var context = filterContext.HttpContext;
            var identity = context.User.Identity;
            var routeData = filterContext.RouteData.Values;
            var controller = routeData["controller"];
            var action = routeData["action"];
            var url = string.Format("/{0}/{1}", controller, action);
            //var userid = Guid.Parse(identity.GetLoginUserId());
            Guid.TryParse(identity.GetLoginUserId(), out Guid userid);
            var hasRight = authorityAppService.HasMenusAuthority(userid, url);

            if (hasRight) return;

            var isAjax = context.Request.Headers["X-Requested-With"].ToString()
                .Equals("XMLHttpRequest", StringComparison.CurrentCultureIgnoreCase);

            IActionResult result;
            if (isAjax)
            {
                var data = new
                {
                    flag = false,
                    code = (int)HttpStatusCode.Unauthorized,
                    msg = "您没有权限！"
                };
                result = new JsonResult(data);
            }
            else
            {
                result = new ViewResult
                {
                    ViewName = "~/Views/Shared/NoAuth.cshtml",
                };
            }
            filterContext.Result = result;
        }
    }

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
            var claim = (identity as ClaimsIdentity)?.FindFirst("LoginUserId");
            if (claim != null)
            {
                return claim.Value;
            }
            return string.Empty;
        }
    }
}

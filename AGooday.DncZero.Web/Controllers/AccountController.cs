using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using AGooday.DncZero.Common.Extensions;
using AGooday.DncZero.Application.ViewModels;
using Microsoft.Extensions.Logging;
using AGooday.DncZero.Application.Interfaces;
using AGooday.DncZero.Web.Filters;

namespace AGooday.DncZero.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersAppService _usersAppService;
        public AccountController(ILogger<UsersController> logger, IUsersAppService usersAppService)
        {
            _logger = logger;
            _usersAppService = usersAppService;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [IgnoreAuth]
        public IActionResult Login()
        {
            var model = new LoginViewModel
            {
                ReturnUrl = Request.Query["ReturnUrl"],
                Identifier = "agooday@dnc.com",
                Credential = "agooday*"
            };
            return View(model);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [IgnoreAuth]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid) return View(model);

            var connection = Request.HttpContext.Features.Get<IHttpConnectionFeature>();

            //var localIpAddress = connection.LocalIpAddress;    //本地IP地址
            //var localPort = connection.LocalPort;              //本地IP端口
            var remoteIpAddress = connection.RemoteIpAddress;  //远程IP地址
            //var remotePort = connection.RemotePort;            //本地IP端口
            
            model.IP = remoteIpAddress.ToString();
            var result = await _usersAppService.LoginAsync(model);
            if (result.LoginSuccess)
            {
                var authenType = CookieAuthenticationDefaults.AuthenticationScheme;
                var identity = new ClaimsIdentity(authenType);
                identity.AddClaim(new Claim(ClaimTypes.Name, result.Identifier));
                identity.AddClaim(new Claim("UserId", result.User.Id.ToString("N")));
                var properties = new AuthenticationProperties() { IsPersistent = true };
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(authenType, principal, properties);
                model.ReturnUrl = model.ReturnUrl.IsNotBlank() ? model.ReturnUrl : "/";
                return Redirect(model.ReturnUrl);
            }
            ViewData["LoginSuccess"] = result.LoginSuccess;
            //ModelState.AddModelError("登录失败");
            return View(model);
        }
        #endregion

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
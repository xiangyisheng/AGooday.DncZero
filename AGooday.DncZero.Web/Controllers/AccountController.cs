﻿using System;
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
using AGooday.DncZero.Domain.Core.Notifications;
using MediatR;
using AGooday.DncZero.Domain.Models;

namespace AGooday.DncZero.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersAppService _usersAppService;
        // 将领域通知处理程序注入Controller
        private readonly DomainNotificationHandler _notifications;

        public AccountController(ILogger<UsersController> logger, IUsersAppService usersAppService, INotificationHandler<DomainNotification> notifications)
        {
            _logger = logger;
            _usersAppService = usersAppService;
            // 强类型转换
            _notifications = (DomainNotificationHandler)notifications;
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
        [IgnoreAuth]
        public IActionResult Login(string returnUrl = null)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl ?? Request.Query["ReturnUrl"],
                Identifier = "agooday@dnc.com",
                Credential = "agooday*"
            };
            return View(model);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [IgnoreAuth]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var connection = Request.HttpContext.Features.Get<IHttpConnectionFeature>();

            var ip = HttpContext.Connection.RemoteIpAddress.ToIPv4String();

            //var localIpAddress = connection.LocalIpAddress;    //本地IP地址
            //var localPort = connection.LocalPort;              //本地IP端口
            var remoteIpAddress = connection.RemoteIpAddress;  //远程IP地址
            //var remotePort = connection.RemotePort;            //本地IP端口
            //_logger.LogWarning("登录开始");
            //_logger.LogInformation($"登录开始!");
            _logger.LogDebug("LogDebug");
            _logger.LogError("LogError");
            _logger.LogInformation($"LogInformation{model.Identifier}");
            _logger.LogWarning("LogWarning");
            model.IP = remoteIpAddress.ToString();
            var response = await _usersAppService.LoginAsync(model);

            if (response.Success)
            {
                // 是否存在消息通知
                if (!_notifications.HasNotifications())
                    ViewBag.Sucesso = "Login successful!";

                await SignInAsync(response.Data);

                model.ReturnUrl = model.ReturnUrl.IsNotBlank()
                    //阻止 ASP.NET Core 中的开放重定向攻击：https://docs.microsoft.com/zh-cn/aspnet/core/security/preventing-open-redirects?view=aspnetcore-5.0
                    && Url.IsLocalUrl(model.ReturnUrl)//检查 URL 是否是本地的，防止用户无意中重定向到恶意网站。
                    ? model.ReturnUrl : "/";
                return Redirect(model.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError("Credential", $"{response.Message}，账户或密码错误！");
            }
            return View(model);
        }
        /// <summary>
        /// 登录--弃用
        /// </summary>
        /// <returns></returns>
        [IgnoreAuth]
        [HttpPost]
        [Obsolete]//过时弃用
        public async Task<IActionResult> LoginDisuse(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var connection = Request.HttpContext.Features.Get<IHttpConnectionFeature>();

            //var localIpAddress = connection.LocalIpAddress;    //本地IP地址
            //var localPort = connection.LocalPort;              //本地IP端口
            var remoteIpAddress = connection.RemoteIpAddress;  //远程IP地址
            //var remotePort = connection.RemotePort;            //本地IP端口

            model.IP = remoteIpAddress.ToString();
            var result = _usersAppService.Login(model);
            if (result.LoginSuccess)
            {
                var authenType = CookieAuthenticationDefaults.AuthenticationScheme;
                var claims = new Claim[] {
                    new Claim(ClaimTypes.Name, result.Identifier),
                    new Claim("UserId", result.User.Id.ToString("N")),
                    new Claim("Avatar", result.User.Avatar ?? ""),
                    new Claim("NickName", result.User.NickName ?? ""),
                    new Claim("Email", result.User.Email ?? "")
                };
                var identity = new ClaimsIdentity(claims, authenType);
                var properties = new AuthenticationProperties()
                {
                    //Utc过期时长
                    //ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    //是否持久
                    IsPersistent = true,
                    //允许刷新
                    //AllowRefresh = false
                };
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(authenType, principal, properties);
                model.ReturnUrl = model.ReturnUrl.IsNotBlank() && Url.IsLocalUrl(model.ReturnUrl) ? model.ReturnUrl : "/";
                return Redirect(model.ReturnUrl);
            }
            //ModelState.AddModelError("登录失败");
            return View(model);
        }
        #endregion
        #region 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [IgnoreAuth]
        public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [IgnoreAuth]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                if (model.Credential != model.VerifyPassword)
                {
                    ModelState.AddModelError("VerifyPassword", "两次密码不一致！");
                    return View(model);
                }

                var connection = Request.HttpContext.Features.Get<IHttpConnectionFeature>();

                var remoteIpAddress = connection.RemoteIpAddress;  //远程IP地址

                model.IP = remoteIpAddress.ToString();

                var usersViewModel = new UsersViewModel()
                {
                    Id = Guid.NewGuid(),
                    Sort = 0,
                    Type = 0,
                    NickName = model.NickName,
                    Email = model.Identifier,
                    RegisterTime = DateTime.Now,
                    RegisterIp = model.IP,
                    LastLoginTime = DateTime.Now,
                    LastLoginIp = model.IP,
                    LastModifiedTime = DateTime.Now,
                    LastModifiedIp = model.IP,
                    UserAuths = new List<UserAuthsViewModel>() {
                        new UserAuthsViewModel()
                        {
                            Id = Guid.NewGuid(),
                            IdentityType = "email",
                            Identifier = model.Identifier,
                            Credential = model.Credential.ToMd5(),
                            State = 1,
                            AuthTime = DateTime.Now,
                            LastModifiedTime = DateTime.Now,
                            Verified = true,
                        }
                    }
                };

                // 执行添加方法
                //_usersAppService.Register(usersViewModel);
                var response = await _usersAppService.RegisterAsync(usersViewModel);

                // 是否存在消息通知
                if (!_notifications.HasNotifications())
                    ViewBag.Sucesso = "Users Registered!";

                if (response.Success)
                {
                    await SignInAsync(response.Data);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // 是否存在消息通知
                    if (!_notifications.HasNotifications())
                        ModelState.AddModelError("VerifyPassword", $"{response.Message}");
                }

                return View(model);
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }
        /// <summary>
        /// 注册--弃用
        /// </summary>
        /// <returns></returns>
        [IgnoreAuth]
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> RegisterDisuse(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.Credential != model.VerifyPassword)
            {
                ModelState.AddModelError(null, "两次密码不一致！");
                return View(model);
            }

            var connection = Request.HttpContext.Features.Get<IHttpConnectionFeature>();

            var remoteIpAddress = connection.RemoteIpAddress;  //远程IP地址

            model.IP = remoteIpAddress.ToString();
            var result = await _usersAppService.RegisterAsync(model);
            return View(model);
        }
        #endregion
        #region 个人资料
        /// <summary>
        /// 个人资料
        /// </summary>
        /// <returns></returns>
        [IgnoreAuth]
        public IActionResult Profile()
        {
            return View();
        }

        /// <summary>
        /// 个人资料
        /// </summary>
        /// <returns></returns>
        [IgnoreAuth]
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            return View(model);
        }
        #endregion
        #region 登出
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        #endregion

        private async Task SignInAsync(Users users)
        {
            var result = users;
            var authenType = CookieAuthenticationDefaults.AuthenticationScheme;
            var claims = new Claim[] {
                        new Claim(ClaimTypes.Name, result.UserAuths.FirstOrDefault().Identifier),
                        new Claim("UserId", result.Id.ToString("N")),
                        new Claim("Avatar", result.Avatar ?? ""),
                        new Claim("NickName", result.NickName ?? ""),
                        new Claim("Email", result.Email ?? "")
                };
            var identity = new ClaimsIdentity(claims, authenType);
            var properties = new AuthenticationProperties()
            {
                //Utc过期时长
                //ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                //是否持久
                IsPersistent = true,
                //允许刷新
                //AllowRefresh = false
            };
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(authenType, principal, properties);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AGooday.DncZero.Web.Models;
using AGooday.DncZero.Application.Interfaces;
using FluentValidation;
using AGooday.DncZero.Application.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using AGooday.DncZero.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace AGooday.DncZero.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersAppService _usersAppService;
        IValidator<UsersViewModel> _validator;
        private IMemoryCache _cache;
        // 将领域通知处理程序注入Controller
        private readonly DomainNotificationHandler _notifications;

        public UsersController(ILogger<UsersController> logger, IUsersAppService usersAppService, IMemoryCache cache, INotificationHandler<DomainNotification> notifications)
        {
            _logger = logger;
            _usersAppService = usersAppService;
            _cache = cache;
            // 强类型转换
            _notifications = (DomainNotificationHandler)notifications;
        }

        public IActionResult Index()
        {
            var users = _usersAppService.GetAll().OrderBy(o => o.Name);
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersViewModel = _usersAppService.GetById(id.Value);

            if (usersViewModel == null)
            {
                return NotFound();
            }

            return View(usersViewModel);
        }

        // GET: Users/Create
        // 页面
        //[Authorize(Policy = "CanWriteUsersData")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // 方法
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Policy = "CanWriteUsersData")]
        public ActionResult Create(UsersViewModel usersViewModel)
        {
            try
            {
                //_cache.Remove("ErrorData");
                //ViewBag.ErrorData = null;
                // 视图模型验证
                if (!ModelState.IsValid)
                    return View(usersViewModel);

                #region 删除命令验证
                ////添加命令验证
                //RegisterusersCommand registerusersCommand = new RegisterusersCommand(usersViewModel.Name, usersViewModel.Email, usersViewModel.BirthDate, usersViewModel.Phone);

                ////如果命令无效，证明有错误
                //if (!registerusersCommand.IsValid())
                //{
                //    List<string> errorInfo = new List<string>();
                //    //获取到错误，请思考这个Result从哪里来的 
                //    foreach (var error in registerusersCommand.ValidationResult.Errors)
                //    {
                //        errorInfo.Add(error.ErrorMessage);
                //    }
                //    //对错误进行记录，还需要抛给前台
                //    ViewBag.ErrorData = errorInfo;
                //    return View(usersViewModel);
                //} 
                #endregion

                // 执行添加方法
                _usersAppService.Register(usersViewModel);

                //var errorData = _cache.Get("ErrorData");
                //if (errorData == null)

                // 是否存在消息通知
                if (!_notifications.HasNotifications())
                    ViewBag.Sucesso = "Users Registered!";

                return View(usersViewModel);
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }

        // GET: Users/Edit/5
        [HttpGet]
        //[Authorize(Policy = "CanWriteUsersData")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersViewModel = _usersAppService.GetById(id.Value);

            if (usersViewModel == null)
            {
                return NotFound();
            }

            return View(usersViewModel);
        }

        // POST: Users/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Policy = "CanWriteUsersData")]
        public IActionResult Edit(UsersViewModel usersViewModel)
        {
            if (!ModelState.IsValid) return View(usersViewModel);

            _usersAppService.Update(usersViewModel);

            if (!_notifications.HasNotifications())
                ViewBag.Sucesso = "Users Updated!";

            return View(usersViewModel);
        }

        // GET: Users/Delete/5
        //[Authorize(Policy = "CanWriteOrRemoveUsersData")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersViewModel = _usersAppService.GetById(id.Value);

            if (usersViewModel == null)
            {
                return NotFound();
            }

            return View(usersViewModel);
        }

        // POST: Users/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Policy = "CanWriteOrRemoveUsersData")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _usersAppService.Remove(id);

            if (!_notifications.HasNotifications())
                return View(_usersAppService.GetById(id));

            ViewBag.Sucesso = "Users Removed!";
            return RedirectToAction("Index");
        }

        [Route("history/{id:guid}")]
        public JsonResult History(Guid id)
        {
            var usersHistoryData = _usersAppService.GetAllHistory(id);
            return Json(usersHistoryData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
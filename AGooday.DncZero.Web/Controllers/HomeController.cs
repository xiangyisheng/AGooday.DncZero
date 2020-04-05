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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersAppService _usersAppService;
        IValidator<UsersViewModel> _validator;
        private IMemoryCache _cache;
        // 将领域通知处理程序注入Controller
        private readonly DomainNotificationHandler _notifications;

        public HomeController(ILogger<HomeController> logger, IUsersAppService usersAppService, IMemoryCache cache, INotificationHandler<DomainNotification> notifications)
        {
            _logger = logger;
            _usersAppService = usersAppService;
            _cache = cache;
            // 强类型转换
            _notifications = (DomainNotificationHandler)notifications;
        }

        public IActionResult Index()
        {
            var users = _usersAppService.GetAll().OrderBy(o=>o.Name);
            return View(users);
        }

        // GET: Student/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentViewModel = _usersAppService.GetById(id.Value);

            if (studentViewModel == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }

        // GET: Student/Create
        // 页面
        [Authorize(Policy = "CanWriteUsersData")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // 方法
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanWriteUsersData")]
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
                //RegisterStudentCommand registerStudentCommand = new RegisterStudentCommand(studentViewModel.Name, studentViewModel.Email, studentViewModel.BirthDate, studentViewModel.Phone);

                ////如果命令无效，证明有错误
                //if (!registerStudentCommand.IsValid())
                //{
                //    List<string> errorInfo = new List<string>();
                //    //获取到错误，请思考这个Result从哪里来的 
                //    foreach (var error in registerStudentCommand.ValidationResult.Errors)
                //    {
                //        errorInfo.Add(error.ErrorMessage);
                //    }
                //    //对错误进行记录，还需要抛给前台
                //    ViewBag.ErrorData = errorInfo;
                //    return View(studentViewModel);
                //} 
                #endregion

                // 执行添加方法
                _usersAppService.Register(usersViewModel);

                //var errorData = _cache.Get("ErrorData");
                //if (errorData == null)

                // 是否存在消息通知
                if (!_notifications.HasNotifications())
                    ViewBag.Sucesso = "Student Registered!";

                return View(usersViewModel);
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }

        // GET: Student/Edit/5
        [HttpGet]
        [Authorize(Policy = "CanWriteUsersData")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentViewModel = _usersAppService.GetById(id.Value);

            if (studentViewModel == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanWriteUsersData")]
        public IActionResult Edit(UsersViewModel usersViewModel)
        {
            if (!ModelState.IsValid) return View(usersViewModel);

            _usersAppService.Update(usersViewModel);

            if (!_notifications.HasNotifications())
                ViewBag.Sucesso = "Student Updated!";

            return View(usersViewModel);
        }

        // GET: Student/Delete/5
        [Authorize(Policy = "CanWriteOrRemoveUsersData")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentViewModel = _usersAppService.GetById(id.Value);

            if (studentViewModel == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanWriteOrRemoveUsersData")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _usersAppService.Remove(id);

            if (!_notifications.HasNotifications())
                return View(_usersAppService.GetById(id));

            ViewBag.Sucesso = "Student Removed!";
            return RedirectToAction("Index");
        }

        [Route("history/{id:guid}")]
        public JsonResult History(Guid id)
        {
            var studentHistoryData = _usersAppService.GetAllHistory(id);
            return Json(studentHistoryData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

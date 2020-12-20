using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AGooday.DncZero.Web.Filters;
using AGooday.DncZero.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AGooday.DncZero.Web.Controllers
{
    [IgnoreAuth]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            //若是HttpContext.User.Identity.IsAuthenticated为true，
            //或者HttpContext.User.Claims.Count()大于0表示用户已经登陆
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //这里经过 HttpContext.User.Claims 能够将咱们在Login这个Action中存储到cookie中的全部
                //claims键值对都读出来，好比咱们刚才定义的UserName的值Wangdacui就在这里读取出来了
                var userName = HttpContext.User.Claims.First().Value;
            }
            return View();
        }

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AGooday.DncZero.Web.Filters;
using AGooday.DncZero.Web.Models;
using Microsoft.AspNetCore.Mvc;


namespace AGooday.DncZero.Web.Controllers
{
    [IgnoreAuth]
    public class HomeController : BaseController
    {
        [IgnoreAuth]
        public IActionResult Index()
        {
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

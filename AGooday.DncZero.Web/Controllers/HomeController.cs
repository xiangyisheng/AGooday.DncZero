using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGooday.DncZero.Web.Filters;
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
    }
}

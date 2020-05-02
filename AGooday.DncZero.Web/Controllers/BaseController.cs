using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGooday.DncZero.Domain.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AGooday.DncZero.Web.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [Authorize]
    public class BaseController : Controller
    {
        public virtual IActionResult ProduceResponse<T>(Response<T> response)
        {
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wcindicator.api.Controllers
{
    [Produces("application/json")]
    public class ApiError : Controller
    {
        [HttpPost]
        [Route("/apierror/{code}")]
        public IActionResult ErrorPage(int code)
        {
            return Json(new { Error = code });
        }
    }
}

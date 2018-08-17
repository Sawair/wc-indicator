using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wcindicator.api.Models;

namespace wcindicator.api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var vm = new IndexPageViewModel();
            vm.StatusString = "API W.I.P State";
            return View(vm);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

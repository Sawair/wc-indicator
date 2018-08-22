using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wcindicator.api.Models;
using wcindicator.api.Services;

namespace wcindicator.api.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWCStatusService _statusService;

        public HomeController(IWCStatusService statusService)
        {
            _statusService = statusService;
        }

        public IActionResult Index()
        {
            var vm = new IndexPageViewModel();
            vm.StatusString = _statusService.GetCurrentWCStatus().ToString();
            vm.Updated = _statusService.GetLastReport().ReportTime.ToString();
            return View(vm);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

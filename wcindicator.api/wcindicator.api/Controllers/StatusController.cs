using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wcindicator.api.Models;
using wcindicator.api.Services;

namespace wcindicator.api.Controllers
{
    public class StatusController : Controller
    {
        private readonly IWCStatusService _statusService;

        public StatusController(IWCStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpPost]
        [Route("/api/status")]
        public IActionResult UpdateStatus(UpdateStatusPost model)
        {
            _statusService.Add(model.Status, model.ChangeDate, model.LastStatusDuration);
            return Ok();
        }

        [HttpGet]
        [Route("/api/status")]
        public IActionResult GetLastStatus()
        {
            var vm = new StatusViewModel();
            var report = _statusService.GetLastReport();
            vm.Status = report.Status;
            vm.LastChange = report.ReportTime;
            return Ok(vm);
        }

        // TODO: move to own controller
        [HttpGet]
        [Route("/api/report")]
        public IActionResult GetLastReport()
        {
            return Ok(_statusService.GetLastReport());
        }
    }

    public class StatusViewModel
    {
        public StatusEnum Status { get; set; }

        public DateTime LastChange { get; set; }

        public string StatusString
        {
            get => Enum.GetName(typeof(StatusEnum), Status);
        }
    }

    public class UpdateStatusPost
    {
        public StatusEnum Status { get; set; }
        public DateTime ChangeDate { get; set; }
        public TimeSpan LastStatusDuration { get; set; }
    }
}

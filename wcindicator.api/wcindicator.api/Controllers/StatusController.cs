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
    }

    public class UpdateStatusPost
    {
        public StatusEnum Status { get; set; }
        public DateTime ChangeDate { get; set; }
        public TimeSpan LastStatusDuration { get; set; }
    }
}

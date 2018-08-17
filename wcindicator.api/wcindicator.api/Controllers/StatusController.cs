﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using wcindicator.api.Models;
using wcindicator.api.Services;

namespace wcindicator.api.Controllers
{
    public class StatusController : Controller
    {
        private readonly IWCStatusService _statusService;
        private readonly ILogger<StatusController> _logger;

        public StatusController(IWCStatusService statusService, ILogger<StatusController> logger)
        {
            _statusService = statusService;
            _logger = logger;
        }

        [HttpPost]
        [Route("/api/status")]
        public IActionResult UpdateStatus([FromBody] UpdateStatusPost model)
        {
            _statusService.Add(model.Status, model.ChangeDate, TimeSpan.FromSeconds(model.LastStatusDuration));
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
        public long LastStatusDuration { get; set; }
    }
}
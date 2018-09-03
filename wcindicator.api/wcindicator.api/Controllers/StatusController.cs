using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
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
        public static DateTime HeartbeatTime = DateTime.MinValue;

        public StatusController(IWCStatusService statusService, ILogger<StatusController> logger)
        {
            _statusService = statusService;
            _logger = logger;
        }

        [HttpPost]
        [Route("/api/status")]
        [ProducesResponseType(201)]
        public IActionResult UpdateStatus([FromBody] UpdateStatusPost model)
        {
            var createdObj = _statusService.Add(model.Status, model.ChangeDate, TimeSpan.FromSeconds(model.LastStatusDuration));

            var client = new RestClient("https://prod-28.westeurope.logic.azure.com:443/workflows/8dc2d212ad544ee189458624dcbddf96/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=_GtQEdrIOrJHEwX73HF-nw4L4LeFsO4WeWmlj16-hDQ");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(createdObj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return Created($"/api/status/{createdObj.Id}", createdObj);
        }

        [HttpGet]
        [Route("/api/status")]
        [ProducesResponseType(200, Type = typeof(StatusViewModel))]

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
        [ProducesResponseType(200, Type = typeof(StatusReport))]

        public IActionResult GetLastReport()
        {
            return Ok(_statusService.GetLastReport());
        }

        [HttpPost]
        [Route("/api/heartbeat")]
        [ProducesResponseType(204)]
        public IActionResult Heartbeat()
        {
            HeartbeatTime = DateTime.Now;
            return NoContent();
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

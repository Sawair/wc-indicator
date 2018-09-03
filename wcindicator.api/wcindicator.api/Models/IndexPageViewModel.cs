using System;
using Microsoft.AspNetCore.Mvc;

namespace wcindicator.api.Models
{
    public class IndexPageViewModel
    {
        public string StatusString { get; set; }
        public string Updated { get; internal set; }
        public DateTime Heartbeat { get; internal set; }
        public TimeSpan HeartbeatTimeSpan { get; internal set; }
        public StatusEnum Status { get; internal set; }
    }
}

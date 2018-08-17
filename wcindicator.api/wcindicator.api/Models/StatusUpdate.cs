using System;

namespace wcindicator.api.Models
{
    public class StatusReport
    {
        public long Id { get; set; }
        public DateTime ReportTime { get; set; }
        public StatusEnum Status { get; set; }
        public TimeSpan StatusDuration { get; set; }
    }
}
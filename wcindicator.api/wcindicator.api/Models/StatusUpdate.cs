using System;

namespace wcindicator.api.Models
{
    public class StatusUpdate
    {
        public long Id { get; set; }
        public DateTime ReportTime { get; set; }
        public StatusEnum Status { get; set; }
    }
}
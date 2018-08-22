using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wcindicator.api.Models;

namespace wcindicator.api.Services
{
    public class WCStatusService : IWCStatusService
    {
        private readonly WCIndicatorContext _db;

        public WCStatusService(WCIndicatorContext db)
        {
            _db = db;
        }

        public StatusReport Add(StatusEnum status, DateTime changeDate, TimeSpan statusDuration)
        {
            var report = new StatusReport()
            {
                ReportTime = changeDate,
                Status = status,
                StatusDuration = statusDuration
            };
            _db.StatusUpdates.Add(report);
            _db.SaveChanges();
            return report;
        }

        public StatusEnum GetCurrentWCStatus()
        {
            return _db.StatusUpdates
                .OrderByDescending(s => s.ReportTime)
                .Select(s => s.Status)
                .First();
        }

        public StatusReport GetLastReport()
        {
            return _db.StatusUpdates
                .OrderByDescending(s => s.ReportTime)
                .First();
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }
    }
}

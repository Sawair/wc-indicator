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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wcindicator.api.Models;

namespace wcindicator.api.Services
{
    public interface IWCStatusService
    {
        StatusEnum GetCurrentWCStatus();
        StatusReport GetLastReport();
    }
}

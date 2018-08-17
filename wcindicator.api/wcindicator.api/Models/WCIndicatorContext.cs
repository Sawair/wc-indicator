using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wcindicator.api.Models
{
    public class WCIndicatorContext : DbContext
    {
        public WCIndicatorContext(DbContextOptions<WCIndicatorContext> options)
            : base(options)
        { }

        public DbSet<StatusUpdate> StatusUpdates { get; set; }
    }
}

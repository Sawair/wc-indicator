using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace wcindicator.api.Models
{
    public class WCIndicatorContext : DbContext
    {
        public WCIndicatorContext(DbContextOptions<WCIndicatorContext> options)
            : base(options)
        { }

        public DbSet<StatusReport> StatusUpdates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}

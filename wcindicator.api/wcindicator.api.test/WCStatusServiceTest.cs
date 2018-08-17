using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using wcindicator.api.Models;
using wcindicator.api.Services;
using Xunit;

namespace wcindicator.api.test
{
    public class WCStatusServiceTest
    {
        private WCIndicatorContext CreateInMemoryCoontext()
        {
            var options = new DbContextOptionsBuilder<WCIndicatorContext>()
               .UseInMemoryDatabase("wcindicator" + Guid.NewGuid())
               .Options;
            return new WCIndicatorContext(options);
        }

        [Fact]
        public void GivenDatabaseWithTwoRecords_WhenGettingLastReport_ShouldGetTheLatestOne()
        {
            using (var context = CreateInMemoryCoontext())
            {
                var oldReport = new StatusReport() { ReportTime = new DateTime(2017, 1, 1) };
                var newReport = new StatusReport() { ReportTime = new DateTime(2018, 1, 1) };
                var oldestReport = new StatusReport() { ReportTime = new DateTime(2016, 1, 1) };
                context.StatusUpdates.AddRange(oldReport, newReport, oldestReport);
                context.SaveChanges();

                var service = new WCStatusService(context);
                var currentStatus = service.GetLastReport();
                currentStatus.Should().Be(newReport);
            }
        }

        [Fact]
        public void GivenDatabaseWithTwoRecords_WhenGettingCurrentStatusReport_ShouldGetTheLatestOne()
        {
            using (var context = CreateInMemoryCoontext())
            {
                var oldReport = new StatusReport() { ReportTime = new DateTime(2017, 1, 1), Status = StatusEnum.Wait };
                var newReport = new StatusReport() { ReportTime = new DateTime(2018, 1, 1), Status = StatusEnum.Occupied };
                var oldestReport = new StatusReport() { ReportTime = new DateTime(2016, 1, 1), Status = StatusEnum.Free };
                context.StatusUpdates.AddRange(oldReport, newReport, oldestReport);
                context.SaveChanges();

                var service = new WCStatusService(context);
                var currentStatus = service.GetCurrentWCStatus();
                currentStatus.Should().Be(newReport.Status);
            }
        }

        [Fact]
        public void GivenTwoDateTimesNotSorted_WhenRunningLinqOrderBy_()
        {
            var dates = new List<TestObj>
            {
                new TestObj() { Date = new DateTime(2019, 1, 1) },
                new TestObj() { Date = new DateTime(2018, 1, 1) },
                new TestObj() { Date = new DateTime(2017, 1, 1) }
            };

            var firstAsInCalendar = dates
                .OrderByDescending(d => d.Date)
                .First();

            firstAsInCalendar.Should().Be(dates.First());
        }

        [Fact]
        public void GivenTwoDateTimesSorted_WhenRunningLinqOrderBy_()
        {
            var dates = new List<TestObj>
            {
                new TestObj() { Date = new DateTime(2018, 1, 1) },
                new TestObj() { Date = new DateTime(2016, 1, 1) },
                new TestObj() { Date = new DateTime(2019, 1, 1) }
            };

            var firstAsInCalendar = dates
                .OrderByDescending(d => d.Date)
                .First();

            firstAsInCalendar.Should().Be(dates.Last());
        }

        private class TestObj
        {
            public DateTime Date { get; set; }
        }
    }
}

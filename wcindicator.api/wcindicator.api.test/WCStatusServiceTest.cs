using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
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

        [Fact(Skip = "Nie bedziemy za kazdym razem wysylac powiadomien do ms flow")]
        public void MSFlowIntegrationWorking()
        {
            var createdObj = new StatusReport() { Id = 0, ReportTime = DateTime.Now, Status = StatusEnum.Free, StatusDuration = TimeSpan.FromDays(1) };
            var client = new RestClient("https://prod-28.westeurope.logic.azure.com:443/workflows/8dc2d212ad544ee189458624dcbddf96/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=_GtQEdrIOrJHEwX73HF-nw4L4LeFsO4WeWmlj16-hDQ");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(createdObj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }


        [Fact]
        public void GivenEmptyDatabase_WhenGettingLastReport_ShouldGetTheDefaultOne()
        {
            using (var context = CreateInMemoryCoontext())
            {
                context.StatusUpdates.RemoveRange(context.StatusUpdates);
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
        public void GivenDatabaseWithTwoRecords_WhenGettingLastReport_ShouldGetTheLatestOne()
        {
            using (var context = CreateInMemoryCoontext())
            {
                context.StatusUpdates.RemoveRange(context.StatusUpdates);
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
                context.StatusUpdates.RemoveRange(context.StatusUpdates);
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

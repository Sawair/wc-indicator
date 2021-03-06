﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wcindicator.api.Controllers;
using wcindicator.api.Models;
using wcindicator.api.test.Extensions;
using Xunit;

namespace wcindicator.api.test
{
    public class ApiTest
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ApiTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/home/index")]
        public async Task HomePage_ShouldBeAccessibleOnTwoAddresses(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/api/status")]
        [InlineData("/api/report")]
        public async Task StatusApiShouldReturnApplicationJson(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task StatusApiShouldReturnApplicationJsonType()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/report");

            // Assert
            response.EnsureSuccessStatusCode();
            Func<Task> a = () => response.Content.ReadAsJsonStrictAsync<StatusReport>();
            a.Should().NotThrow<JsonSerializationException>();
        }

        [Fact]
        public async Task StatusApiShouldReturnApplicationJsonType2()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/report");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsJsonStrictAsync<StatusReport>();
            json.Id.Should().BeGreaterOrEqualTo(1);
        }
    }
}
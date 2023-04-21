using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using StockBot.IntegrationTests.Support;
using Xunit;
using Xunit.Abstractions;

namespace StockBot.IntegrationTests.API
{
    public class ProgramTest
    {
        private ITestOutputHelper _testOutputHelper;
        public ProgramTest(ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void GivenAppFactoryWhenCreateClientThenGetClient()
        {
            using var app = new CustomWebApplicationFactory<Program>(_testOutputHelper);
            using var client = app.CreateClient();

            client.Should().NotBeNull();
        }

        [Fact]
        public async void GivenSwaggerWhenOpenThenGetIndex()
        {
            
            //Arrange
            var uriSwagger = new Uri("swagger/index.html", UriKind.Relative);
            var app = new CustomWebApplicationFactory<Program>(_testOutputHelper);
            var client = app.CreateClient();

            //Act
            var response = await client.GetAsync(uriSwagger).ConfigureAwait(false);

            //Assert
            response.EnsureSuccessStatusCode();
            response
                .Should()
                .HaveStatusCode(HttpStatusCode.OK);
        }
    }
}

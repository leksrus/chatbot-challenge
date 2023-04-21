using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace StockBot.IntegrationTests.Support
{
    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
    {
        private ITestOutputHelper _output;
        public CustomWebApplicationFactory(ITestOutputHelper testOutputHelper)
        {
            this.ClientOptions.AllowAutoRedirect = false;
            this.ClientOptions.BaseAddress = new Uri("https://localhost");
            this._output = testOutputHelper;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace catalog.merger.api.tests.Infrastructure
{
    public class TestWebApplicationFactory : WebApplicationFactory<TestStartup>
    {
        public HttpClient Client => CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        protected override IHostBuilder CreateHostBuilder()
        {
            return TestHostBuilder.CreateHostBuilder();
        }
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace catalog.merger.api.tests.Infrastructure
{
    public class TestHostBuilder
    {
        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new DefaultServiceProviderFactory())
                .ConfigureWebHostDefaults(UpdateWebHostBuilder)
                .UseNLog()
                .UseEnvironment(Environments.Development);
        }

        public static void UpdateWebHostBuilder(IWebHostBuilder builder)
        {
            builder.UseStartup<TestStartup>();
        }
    }
}
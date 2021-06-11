using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace catalog.merger.api.tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void SetupTestDependencies(IServiceCollection services)
        {
            services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(typeof(Startup).Assembly));
        }
    }
}
using catalog.merger.api.Infrastructure.Proxies.Barcode;
using catalog.merger.api.Infrastructure.Proxies.Catalog;
using catalog.merger.api.Infrastructure.Proxies.Supplier;
using catalog.merger.api.tests.Fakes;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

            services.Replace(ServiceDescriptor.Transient<IBarcodeProxy, FakeCsvBarcodeProxy>());
            services.Replace(ServiceDescriptor.Transient<ICatalogProxy, FakeCsvCatalogProxy>());
            services.Replace(ServiceDescriptor.Transient<ISupplierProxy, FakeCsvSupplierProxy>());
        }
    }
}
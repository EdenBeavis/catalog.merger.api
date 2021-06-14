using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.Infrastructure.Proxies.Supplier;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.tests.Fakes
{
    public class FakeCsvSupplierProxy : ISupplierProxy
    {
        private readonly IDictionary<string, IEnumerable<SupplierItem>> _suppliers = new Dictionary<string, IEnumerable<SupplierItem>>
        {
            { TestConstants.CompanyNames.NoBarcodeResult, GetGenericResult() },
            { TestConstants.CompanyNames.NoCatalogResult, GetGenericResult() },
            { TestConstants.CompanyNames.NoSupplierResult, Enumerable.Empty<SupplierItem>() },
            { TestConstants.CompanyNames.SuccessResultCompanyA, GetSuccessSupplierA() },
            { TestConstants.CompanyNames.SuccessResultCompanyB, GetSuccessSupplierB() },
            { TestConstants.CompanyNames.OverlappingCompanyA, GetOverlapSupplier() },
            { TestConstants.CompanyNames.OverlappingCompanyB, GetOverlapSupplier() }
        };

        public async Task<IEnumerable<SupplierItem>> Get(string companyName, CancellationToken cancellationToken)
        {
            var retrieved = _suppliers.TryGetValue(companyName, out var suppliers);

            return retrieved ? suppliers : Enumerable.Empty<SupplierItem>();
        }

        private static IEnumerable<SupplierItem> GetOverlapSupplier() => new List<SupplierItem>
        {
            GetSupplierItem(1, "Supplier1"),
            GetSupplierItem(3, "Supplier3")
        };

        private static IEnumerable<SupplierItem> GetSuccessSupplierA() => new List<SupplierItem>
        {
            GetSupplierItem(1, "Supplier1"),
            GetSupplierItem(3, "Supplier3")
        };

        private static IEnumerable<SupplierItem> GetSuccessSupplierB() => new List<SupplierItem>
        {
            GetSupplierItem(1, "Supplier1"),
            GetSupplierItem(2, "Supplier2"),
            GetSupplierItem(3, "Supplier3")
        };

        private static IEnumerable<SupplierItem> GetGenericResult() => new List<SupplierItem>
        {
            GetSupplierItem(1, "Supplier1")
        };

        private static SupplierItem GetSupplierItem(int id, string name) => new SupplierItem
        {
            ID = id,
            Name = name
        };
    }
}
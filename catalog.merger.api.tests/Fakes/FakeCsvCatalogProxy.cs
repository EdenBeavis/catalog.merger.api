using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.Infrastructure.Proxies.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.tests.Fakes
{
    public class FakeCsvCatalogProxy : ICatalogProxy
    {
        private readonly IDictionary<string, IEnumerable<CatalogItem>> _catalogs = new Dictionary<string, IEnumerable<CatalogItem>>
        {
            { TestConstants.CompanyNames.NoBarcodeResult, GetGenericResult() },
            { TestConstants.CompanyNames.NoCatalogResult, Enumerable.Empty<CatalogItem>() },
            { TestConstants.CompanyNames.NoSupplierResult, GetGenericResult() },
            { TestConstants.CompanyNames.SuccessResultCompanyA, GetSuccessCatalogA() },
            { TestConstants.CompanyNames.SuccessResultCompanyB, GetSuccessCatalogB() },
            { TestConstants.CompanyNames.OverlappingCompanyA, GetOverlapCatalogA() },
            { TestConstants.CompanyNames.OverlappingCompanyB, GetOverlapCatalogB() }
        };

        public async Task<IEnumerable<CatalogItem>> Get(string companyName, CancellationToken cancellationToken)
        {
            var retrieved = _catalogs.TryGetValue(companyName, out var catalog);

            return retrieved ? catalog : Enumerable.Empty<CatalogItem>();
        }

        public async Task<IEnumerable<CatalogItemDto>> Add(IEnumerable<CatalogItemDto> catalog, CancellationToken cancellationToken)
        {
            return catalog;
        }

        private static IEnumerable<CatalogItem> GetOverlapCatalogA() => new List<CatalogItem>
        {
            GetCatalogItem("SKU-Company-A-1", "Potato cakes"),
            GetCatalogItem("SKU-Company-A-2", "Jerky"),
            GetCatalogItem("SKU-Company-A-3", "Cakes"),
            GetCatalogItem("SKU-Company-A-4", "Banana"),
            GetCatalogItem("SKU-Company-A-5", "Donut"),
            GetCatalogItem("SKU-Company-A-6", "Potato"),
        };

        private static IEnumerable<CatalogItem> GetOverlapCatalogB() => new List<CatalogItem>
        {
            GetCatalogItem("SKU-Company-B-1", "Potato cakes"),
            GetCatalogItem("SKU-Company-B-2", "Jerky"),
            GetCatalogItem("SKU-Company-B-3", "Cakes"),
            GetCatalogItem("SKU-Company-B-4", "Banana"),
            GetCatalogItem("SKU-Company-B-5", "Donut"),
            GetCatalogItem("SKU-Company-B-6", "Potato"),
        };

        private static IEnumerable<CatalogItem> GetSuccessCatalogA() => new List<CatalogItem>
        {
            GetCatalogItem("SKU-Company-A-1", "Potato cakes"),
            GetCatalogItem("SKU-Company-A-2", "Jerky"),
            GetCatalogItem("SKU-Company-A-3", "Cakes"),
            GetCatalogItem("SKU-Company-AB-1", "Banana"),
            GetCatalogItem("SKU-Company-A-4", "Donut"),
            GetCatalogItem("SKU-Company-AB-2", "Potato"),
        };

        private static IEnumerable<CatalogItem> GetSuccessCatalogB() => new List<CatalogItem>
        {
            GetCatalogItem("SKU-Company-B-1", "Potato cakes salted"),
            GetCatalogItem("SKU-Company-B-2", "Jerky Spicy"),
            GetCatalogItem("SKU-Company-B-3", "Cakes"),
            GetCatalogItem("SKU-Company-AB-1", "Banana"),
            GetCatalogItem("SKU-Company-A-4", "Grilled Cheese"),
            GetCatalogItem("SKU-Company-AB-2", "Potato b"),
        };

        private static IEnumerable<CatalogItem> GetGenericResult() => new List<CatalogItem>
        {
            GetCatalogItem("SKU-Company-G-1", "Generic-1"),
            GetCatalogItem("SKU-Company-G-2", "Generic-2"),
            GetCatalogItem("SKU-Company-G-3", "Generic-3"),
            GetCatalogItem("SKU-Company-G-4", "Generic-4"),
            GetCatalogItem("SKU-Company-G-5", "Generic-5"),
            GetCatalogItem("SKU-Company-G-6", "Generic-6"),
        };

        private static CatalogItem GetCatalogItem(string SKU, string Description) => new CatalogItem
        {
            SKU = SKU,
            Description = Description
        };
    }
}
using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.Infrastructure.Proxies.Barcode;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.tests.Fakes
{
    public class FakeCsvBarcodeProxy : IBarcodeProxy
    {
        private readonly IDictionary<string, IEnumerable<BarcodeItem>> _barcodes = new Dictionary<string, IEnumerable<BarcodeItem>>
        {
            { TestConstants.CompanyNames.NoBarcodeResult, Enumerable.Empty<BarcodeItem>() },
            { TestConstants.CompanyNames.NoCatalogResult, GetGenericResult() },
            { TestConstants.CompanyNames.NoSupplierResult, GetGenericResult() },
            { TestConstants.CompanyNames.SuccessResultCompanyA, GetSuccessBarcodesA() },
            { TestConstants.CompanyNames.SuccessResultCompanyB, GetSuccessBarcodesB() },
            { TestConstants.CompanyNames.OverlappingCompanyA, GetOverlapBarcodesA() },
            { TestConstants.CompanyNames.OverlappingCompanyB, GetOverlapBarcodesB() }
        };

        public async Task<IEnumerable<BarcodeItem>> Get(string companyName, CancellationToken cancellationToken)
        {
            var retrieved = _barcodes.TryGetValue(companyName, out var barcodes);

            return retrieved ? barcodes : Enumerable.Empty<BarcodeItem>();
        }

        private static IEnumerable<BarcodeItem> GetOverlapBarcodesA() => new List<BarcodeItem>
        {
            GetBarcodeItem(1, "SKU-Company-A-1", "A1"),
            GetBarcodeItem(1, "SKU-Company-A-2", "A2"),
            GetBarcodeItem(1, "SKU-Company-A-3", "A3"),
            GetBarcodeItem(2, "SKU-Company-A-4", "A4"),
            GetBarcodeItem(2, "SKU-Company-A-5", "A5"),
            GetBarcodeItem(2, "SKU-Company-A-6", "A6"),
        };

        private static IEnumerable<BarcodeItem> GetOverlapBarcodesB() => new List<BarcodeItem>
        {
            GetBarcodeItem(1, "SKU-Company-B-1", "A1"),
            GetBarcodeItem(1, "SKU-Company-B-2", "A2"),
            GetBarcodeItem(1, "SKU-Company-B-3", "A3"),
            GetBarcodeItem(2, "SKU-Company-B-4", "A4"),
            GetBarcodeItem(2, "SKU-Company-B-5", "A5"),
            GetBarcodeItem(2, "SKU-Company-B-6", "A6"),
        };

        private static IEnumerable<BarcodeItem> GetSuccessBarcodesA() => new List<BarcodeItem>
        {
            GetBarcodeItem(1, "SKU-Company-A-1", "A1"),
            GetBarcodeItem(1, "SKU-Company-A-2", "A2"),
            GetBarcodeItem(1, "SKU-Company-A-3", "A3"),
            GetBarcodeItem(3, "SKU-Company-AB-1", "AB1"),
            GetBarcodeItem(1, "SKU-Company-A-4", "A4"),
            GetBarcodeItem(3, "SKU-Company-AB-2", "AB2"),
        };

        private static IEnumerable<BarcodeItem> GetSuccessBarcodesB() => new List<BarcodeItem>
        {
            GetBarcodeItem(2, "SKU-Company-B-1", "B1"),
            GetBarcodeItem(2, "SKU-Company-B-2", "B2"),
            GetBarcodeItem(2, "SKU-Company-B-3", "B3"),
            GetBarcodeItem(3, "SKU-Company-AB-1", "AB1"),
            GetBarcodeItem(1, "SKU-Company-A-4", "A4"),
            GetBarcodeItem(3, "SKU-Company-AB-2", "AB2"),
        };

        private static IEnumerable<BarcodeItem> GetGenericResult() => new List<BarcodeItem>
        {
            GetBarcodeItem(1, "SKU-Company-G-1", "Generic-1"),
            GetBarcodeItem(1, "SKU-Company-G-2", "Generic-2"),
            GetBarcodeItem(1, "SKU-Company-G-3", "Generic-3"),
            GetBarcodeItem(1, "SKU-Company-G-4", "Generic-4"),
            GetBarcodeItem(1, "SKU-Company-G-5", "Generic-5"),
            GetBarcodeItem(1, "SKU-Company-G-6", "Generic-6"),
        };

        private static BarcodeItem GetBarcodeItem(int Id, string SKU, string barcode) => new BarcodeItem
        {
            SupplierID = Id,
            SKU = SKU,
            Barcode = barcode
        };
    }
}
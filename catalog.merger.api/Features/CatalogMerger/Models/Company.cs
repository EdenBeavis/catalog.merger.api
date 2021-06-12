using System.Collections.Generic;

namespace catalog.merger.api.Features.CatalogMerger.Models
{
    public class Company
    {
        public string CompanyName { get; set; }
        public IEnumerable<CatalogItem> Catalog { get; set; }
        public IEnumerable<SupplierItem> Suppliers { get; set; }
        public IEnumerable<BarcodeItem> Barcodes { get; set; }
    }
}
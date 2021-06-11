using System.Collections.Generic;

namespace catalog.merger.api.Features.CatalogMerger.Models
{
    public class Catalog
    {
        public IEnumerable<CatalogItem> CatalogItems { get; set; }
    }
}
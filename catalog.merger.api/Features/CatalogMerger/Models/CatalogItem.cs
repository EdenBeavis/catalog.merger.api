using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catalog.merger.api.Features.CatalogMerger.Models
{
    public class CatalogItem
    {
        string SKU { get; set; }
        string Description { get; set; }

        string Company { get; set; }
    }
}

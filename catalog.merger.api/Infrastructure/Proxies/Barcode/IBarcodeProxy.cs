using catalog.merger.api.Features.CatalogMerger.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.Infrastructure.Proxies.Barcode
{
    public interface IBarcodeProxy
    {
        Task<IEnumerable<BarcodeItem>> Get(string companyName, CancellationToken cancellationToken);
    }
}
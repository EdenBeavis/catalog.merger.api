using catalog.merger.api.Features.CatalogMerger.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.Infrastructure.Proxies.Catalog
{
    public interface ICatalogProxy
    {
        Task<IEnumerable<CatalogItem>> Get(string companyName, CancellationToken cancellationToken);

        Task<IEnumerable<CatalogItemDto>> Add(IEnumerable<CatalogItemDto> catalog, CancellationToken cancellationToken);
    }
}
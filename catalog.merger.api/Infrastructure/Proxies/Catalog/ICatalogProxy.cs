using Bearded.Monads;
using catalog.merger.api.Features.CatalogMerger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace catalog.merger.api.Infrastructure.Proxies.Catalog
{
    public interface ICatalogProxy
    {
        Task<IEnumerable<CatalogItem>> Get(string companyName);

        Task<Option<Void>> Add(IEnumerable<CatalogItemDto> catalog);
    }
}
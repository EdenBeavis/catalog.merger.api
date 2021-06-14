using catalog.merger.api.Features.CatalogMerger.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.Infrastructure.Proxies.Supplier
{
    public interface ISupplierProxy
    {
        Task<IEnumerable<SupplierItem>> Get(string companyName, CancellationToken cancellationToken);
    }
}
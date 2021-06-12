using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.Infrastructure.Helper;
using NetCore.AutoRegisterDi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catalog.merger.api.Infrastructure.Proxies.Supplier
{
    [RegisterAsScoped]
    public class CsvSupplierProxy : ISupplierProxy
    {
        public async Task<IEnumerable<SupplierItem>> Get(string companyName)
        {
            var filePath = $"{Constants.InputFilePath}/supplier{companyName}";
            return await CSVHelper.ReadFromCSV<SupplierItem>(filePath).ToListAsync();
        }
    }
}
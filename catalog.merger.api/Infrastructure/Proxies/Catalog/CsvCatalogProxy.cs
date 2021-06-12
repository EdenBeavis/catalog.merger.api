using Bearded.Monads;
using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.Infrastructure.Helper;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catalog.merger.api.Infrastructure.Proxies.Catalog
{
    [RegisterAsScoped]
    public class CsvCatalogProxy : ICatalogProxy
    {
        public Task<Option<Void>> Add(IEnumerable<CatalogItemDto> catalog)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CatalogItem>> Get(string companyName)
        {
            var filePath = $"{Constants.InputFilePath}/catalog{companyName}";
            return await CSVHelper.ReadFromCSV<CatalogItem>(filePath).ToListAsync();
        }
    }
}
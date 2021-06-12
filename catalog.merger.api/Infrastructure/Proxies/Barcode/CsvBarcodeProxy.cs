using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.Infrastructure.Helper;
using NetCore.AutoRegisterDi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catalog.merger.api.Infrastructure.Proxies.Barcode
{
    [RegisterAsScoped]
    public class CsvBarcodeProxy : IBarcodeProxy
    {
        public async Task<IEnumerable<BarcodeItem>> Get(string companyName)
        {
            var filePath = $"{Constants.InputFilePath}/barcode{companyName}";
            return await CSVHelper.ReadFromCSV<BarcodeItem>(filePath).ToListAsync();
        }
    }
}
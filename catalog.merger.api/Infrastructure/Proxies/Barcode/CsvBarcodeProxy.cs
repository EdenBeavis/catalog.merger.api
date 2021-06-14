using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.Infrastructure.Helper;
using Microsoft.Extensions.Logging;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.Infrastructure.Proxies.Barcode
{
    [RegisterAsScoped]
    public class CsvBarcodeProxy : IBarcodeProxy
    {
        private readonly ILogger<CsvBarcodeProxy> _logger;

        public CsvBarcodeProxy(ILogger<CsvBarcodeProxy> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<BarcodeItem>> Get(string companyName, CancellationToken cancellationToken)
        {
            try
            {
                var filePath = $"{Constants.InputFilePath}/barcodes{companyName}.csv";
                return await CSVHelper.ReadFromCSV<BarcodeItem>(filePath, cancellationToken).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting barcodes from csv for company {companyName}");
                return Enumerable.Empty<BarcodeItem>();
            }
        }
    }
}
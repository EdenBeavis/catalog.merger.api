using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.Infrastructure.Helper;
using Microsoft.Extensions.Logging;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.Infrastructure.Proxies.Catalog
{
    [RegisterAsScoped]
    public class CsvCatalogProxy : ICatalogProxy
    {
        private readonly ILogger<CsvCatalogProxy> _logger;

        public CsvCatalogProxy(ILogger<CsvCatalogProxy> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogItemDto>> Add(IEnumerable<CatalogItemDto> catalog, CancellationToken cancellationToken)
        {
            try
            {
                var filePath = $"{Constants.OutputFilePath}/result_output_{DateTime.Now:yyyy-M-dd-HH-mm}.csv";
                await CSVHelper.WriteToCsv(catalog, filePath, cancellationToken);

                return catalog;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error writing catalog to csv");
                return Enumerable.Empty<CatalogItemDto>();
            }
        }

        public async Task<IEnumerable<CatalogItem>> Get(string companyName, CancellationToken cancellationToken)
        {
            try
            {
                var filePath = $"{Constants.InputFilePath}/catalog{companyName}.csv";
                return await CSVHelper.ReadFromCSV<CatalogItem>(filePath, cancellationToken).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting catalog from csv with company name {companyName}");
                return Enumerable.Empty<CatalogItem>();
            }
        }
    }
}
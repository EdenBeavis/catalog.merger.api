using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.Infrastructure.Helper;
using Microsoft.Extensions.Logging;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.Infrastructure.Proxies.Supplier
{
    [RegisterAsScoped]
    public class CsvSupplierProxy : ISupplierProxy
    {
        private readonly ILogger<CsvSupplierProxy> _logger;

        public CsvSupplierProxy(ILogger<CsvSupplierProxy> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<SupplierItem>> Get(string companyName, CancellationToken cancellationToken)
        {
            try
            {
                var filePath = $"{Constants.InputFilePath}/suppliers{companyName}.csv";
                return await CSVHelper.ReadFromCSV<SupplierItem>(filePath, cancellationToken).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting catalog from csv with company name {companyName}");
                return Enumerable.Empty<SupplierItem>();
            }
        }
    }
}
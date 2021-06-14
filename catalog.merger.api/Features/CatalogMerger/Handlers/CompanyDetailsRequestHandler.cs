using Bearded.Monads;
using Bolt.Common.Extensions;
using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.Infrastructure.Proxies.Barcode;
using catalog.merger.api.Infrastructure.Proxies.Catalog;
using catalog.merger.api.Infrastructure.Proxies.Supplier;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.Features.CatalogMerger.Handlers
{
    public class CompanyDetailsRequest : IRequest<Option<Company>>
    {
        public string CompanyName { get; set; }
    }

    public class CompanyDetailsRequestHandler : IRequestHandler<CompanyDetailsRequest, Option<Company>>
    {
        private readonly IBarcodeProxy _barcodeProxy;
        private readonly ICatalogProxy _catalogProxy;
        private readonly ISupplierProxy _supplierProxy;
        private readonly ILogger<CompanyDetailsRequestHandler> _logger;

        public CompanyDetailsRequestHandler(
            IBarcodeProxy barcodeProxy,
            ICatalogProxy catalogProxy,
            ISupplierProxy supplierProxy,
            ILogger<CompanyDetailsRequestHandler> logger)
        {
            _barcodeProxy = barcodeProxy;
            _catalogProxy = catalogProxy;
            _supplierProxy = supplierProxy;
            _logger = logger;
        }

        public async Task<Option<Company>> Handle(CompanyDetailsRequest request, CancellationToken cancellationToken)
        {
            var barcodes = _barcodeProxy.Get(request.CompanyName, cancellationToken);
            var catalog = _catalogProxy.Get(request.CompanyName, cancellationToken);
            var suppliers = _supplierProxy.Get(request.CompanyName, cancellationToken);

            await Task.WhenAll(barcodes, catalog, suppliers);

            if ((await barcodes).IsEmpty() || (await catalog).IsEmpty() || (await suppliers).IsEmpty())
                return Option<Company>.None;

            return new Company
            {
                CompanyName = request.CompanyName,
                Barcodes = await barcodes,
                Catalog = await catalog,
                Suppliers = await suppliers
            };
        }
    }
}
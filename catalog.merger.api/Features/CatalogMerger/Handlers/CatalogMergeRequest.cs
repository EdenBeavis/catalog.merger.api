using Bearded.Monads;
using Bolt.Common.Extensions;
using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.Infrastructure.Proxies.Catalog;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.Features.CatalogMerger.Handlers
{
    public class CatalogMergeRequest : IRequest<IEnumerable<CatalogItemDto>>
    {
        public IEnumerable<string> CompanyNames { get; set; }
    }

    public class CatalogMergeRequestHandler : IRequestHandler<CatalogMergeRequest, IEnumerable<CatalogItemDto>>
    {
        private readonly IMediator _mediator;
        private readonly ICatalogProxy _catalogProxy;

        public CatalogMergeRequestHandler(
            IMediator mediator,
            ICatalogProxy catalogProxy)
        {
            _mediator = mediator;
            _catalogProxy = catalogProxy;
        }

        public async Task<IEnumerable<CatalogItemDto>> Handle(CatalogMergeRequest request, CancellationToken cancellationToken)
        {
            var companies = await GetCompanyDetails(request.CompanyNames, cancellationToken);

            if (companies.IsEmpty()) return new List<CatalogItemDto>();

            var mergedCatalogs = MergeCompanyCatalogs(companies);

            await _catalogProxy.Add(mergedCatalogs, cancellationToken);

            return mergedCatalogs;
        }

        private async Task<IEnumerable<Company>> GetCompanyDetails(IEnumerable<string> companyNames, CancellationToken cancellationToken)
        {
            var companies = new List<Company>();

            foreach (var companyName in companyNames)
            {
                // Bearded monads will unwrap the object and insert it into the list when there is a value
                (await _mediator.Send(new CompanyDetailsRequest { CompanyName = companyName }, cancellationToken))
                    .WhenSome(companies.Add);
            }

            return companies;
        }

        private static IEnumerable<CatalogItemDto> MergeCompanyCatalogs(IEnumerable<Company> companies)
        {
            var mergedBarcodes = new List<BarcodeItem>();
            var mergedCatalogItems = new List<CatalogItemDto>();

            var allBarcodes = companies.SelectMany(company => company.Barcodes);

            foreach (var catalogItem in GetAllCatalogs(companies))
            {
                var filteredBarcodes = GetBarcodesForSelectedSKU(allBarcodes, catalogItem.SKU);

                if (!CatalogItemWithSameBarcodeIsMerged(filteredBarcodes, mergedBarcodes))
                {
                    mergedCatalogItems.Add(catalogItem);
                    mergedBarcodes.AddRange(filteredBarcodes);
                }
            }

            return mergedCatalogItems;
        }

        private static IEnumerable<CatalogItemDto> GetAllCatalogs(IEnumerable<Company> companies)
            => companies.SelectMany(company => company.Catalog.Select(catalog => new CatalogItemDto
            {
                SKU = catalog.SKU,
                Description = catalog.Description,
                Source = company.CompanyName
            }));

        private static IEnumerable<BarcodeItem> GetBarcodesForSelectedSKU(IEnumerable<BarcodeItem> barcodes, string catalogItemSKU)
            => barcodes.Where(barcode => barcode.SKU.IsSame(catalogItemSKU));

        private static bool CatalogItemWithSameBarcodeIsMerged(IEnumerable<BarcodeItem> mergedBarcodes, IEnumerable<BarcodeItem> filteredBarcodes)
            => mergedBarcodes.Any(mergedBarcode => filteredBarcodes.Any(filteredBarcode => filteredBarcode.Barcode.IsSame(mergedBarcode.Barcode)));
    }
}
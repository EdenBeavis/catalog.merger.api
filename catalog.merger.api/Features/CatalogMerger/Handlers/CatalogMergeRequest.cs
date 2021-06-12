using Bearded.Monads;
using Bolt.Common.Extensions;
using catalog.merger.api.Features.CatalogMerger.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
        private readonly ILogger<CatalogMergeRequestHandler> _logger;

        public CatalogMergeRequestHandler(
            IMediator mediator,
            ILogger<CatalogMergeRequestHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogItemDto>> Handle(CatalogMergeRequest request, CancellationToken cancellationToken)
        {
            var companies = await GetCompanyDetails(request.CompanyNames);

            if (companies.IsEmpty()) return new List<CatalogItemDto>();

            foreach ()
        }

        private async Task<IEnumerable<Company>> GetCompanyDetails(IEnumerable<string> companyNames)
        {
            var companies = new List<Company>();

            foreach (var companyName in companyNames)
            {
                // Bearded monads will unwrap the object and insert it into the list when there is a value
                (await _mediator.Send(new CompanyDetailsRequest { CompanyName = companyName }))
                    .WhenSome(companies.Add);
            }

            return companies;
        }
    }
}
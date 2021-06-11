using Bearded.Monads;
using catalog.merger.api.Features.CatalogMerger.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.Features.CatalogMerger.Handlers
{
    public class CatalogMergeRequest : IRequest<Option<Catalog>>
    {
        public IEnumerable<string> Companies { get; set; }
    }

    public class CatalogMergeRequestHandler : IRequestHandler<CatalogMergeRequest, Option<Catalog>>
    {
        private readonly ILogger<CatalogMergeRequestHandler> _logger;

        public CatalogMergeRequestHandler(
            ILogger<CatalogMergeRequestHandler> logger)
        {
            _logger = logger;
        }

        public async Task<Option<Catalog>> Handle(CatalogMergeRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
using Bolt.Common.Extensions;
using catalog.merger.api.Features.CatalogMerger.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.Features.CatalogMerger
{
    [Route("api/v1/catalogmerger")]
    public class CatalogMergerController : Controller
    {
        private readonly IMediator _mediator;

        private readonly ILogger<CatalogMergerController> _logger;

        public CatalogMergerController(
            IMediator mediator,
            ILogger<CatalogMergerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("merge")]
        public async Task<IActionResult> Merge([FromBody] CatalogMergeRequest request, CancellationToken cancellationToken)
        {
            if (request.CompanyNames.IsEmpty())
                return BadRequest("Comapanies list is empty.");

            try
            {
                var response = await _mediator.Send(request, cancellationToken);

                if (!response.IsEmpty())
                    return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error merging company catalogs");
            }

            return BadRequest();
        }
    }
}
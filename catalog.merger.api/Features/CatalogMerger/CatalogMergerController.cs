using Bearded.Monads;
using Bolt.Common.Extensions;
using catalog.merger.api.Features.CatalogMerger.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace catalog.merger.api.Features.CatalogMerger
{
    [Route("api/v1/catalogmerger")]
    public class CatalogMergerController : Controller
    {
        private readonly IMediator _mediator;

        public CatalogMergerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Merge([FromBody] CatalogMergeRequest request)
        {
            if (request.Companies.IsEmpty())
                return BadRequest("Comapanies list is empty.");

            var response = await _mediator.Send(request);

            if (response)
                return Ok(response.ElseNew());

            return BadRequest();
        }
    }
}
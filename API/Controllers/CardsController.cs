using Application.QCRS.Queries.Cards;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CardsController : Controller
    {
        private readonly IMediator _mediator;

        public CardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetCardsBySetId/{setId}")]
        public async Task<IActionResult> GetCardsBySetId(Guid setId)
        {
            var result = await _mediator.Send(new GetCardsBySetIdQuery(setId));
            return Ok(result);
        }
    }
}

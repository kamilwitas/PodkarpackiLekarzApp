using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodkarpackiLekarz.Api.Requests;
using PodkarpackiLekarz.Api.Requests.Calendars;
using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AppoinmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppoinmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = Permissions.BookAppoinment)]
        public async Task<IActionResult> BookAppoinment([FromBody] BookAppoinmentRequest request)
        {
            var command = request.ToCommand();

            await _mediator.Send(command);

            return Ok();
        }


    }
}

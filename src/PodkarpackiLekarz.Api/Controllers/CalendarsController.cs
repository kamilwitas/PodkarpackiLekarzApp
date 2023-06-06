using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PodkarpackiLekarz.Api.Requests;
using PodkarpackiLekarz.Api.Requests.Calendars;
using PodkarpackiLekarz.Application.Attributes;
using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CalendarsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CalendarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("slot")]
        [CredibilityConfirmationRequirement]
        [Authorize(Policy = Permissions.ManageCalendar)]
        public async Task<ActionResult<Guid>>CreateSlot(
            [FromBody] CreateSlotRequest request)
        {
            var command = request.ToCommand();

            var slotId = await _mediator.Send(command);

            return Ok(slotId);
        }
    }
}

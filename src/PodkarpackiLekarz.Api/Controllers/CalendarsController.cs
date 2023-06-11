using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PodkarpackiLekarz.Api.Requests;
using PodkarpackiLekarz.Api.Requests.Calendars;
using PodkarpackiLekarz.Application.Attributes;
using PodkarpackiLekarz.Application.Calendar.Queries.GetDoctorsPublicCalendar;
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

        [HttpGet("public")]
        [Authorize(Policy = Permissions.ViewPublicCalendar)]
        public async Task<ActionResult<GetDoctorPublicCalendarResult>>GetDoctorPublicCalendar(
            [FromQuery] GetPublicDoctorCalendarRequest request)
        {
            var query = request.ToQuery();

            var result = await _mediator.Send(query);

            return result is null? NotFound() : Ok(result);
        }
    }
}

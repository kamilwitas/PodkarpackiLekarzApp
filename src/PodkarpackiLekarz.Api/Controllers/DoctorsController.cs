using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodkarpackiLekarz.Api.Requests;
using PodkarpackiLekarz.Api.Requests.Users;
using PodkarpackiLekarz.Application.Users.Doctors.CredibilityConfirmations;
using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DoctorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterDoctorRequest request)
    {
        var command = request.ToCommand();

        var doctorId = await _mediator.Send(command);

        return Ok(doctorId);
    }

    [HttpPost]
    [Route("{doctorId}/confirmCredibility")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(Policy = Permissions.ConfirmDoctorCredibility)]
    public async Task<ActionResult<bool>> ConfirmDoctorCredibility(Guid doctorId)
    {
        var command = new ConfirmDoctorCredibilityCommand(doctorId);

        var isConfirmed = await _mediator.Send(command);

        return Ok(isConfirmed);
    }
}

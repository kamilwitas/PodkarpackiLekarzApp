using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodkarpackiLekarz.Api.Requests;
using PodkarpackiLekarz.Api.Requests.Users;
using PodkarpackiLekarz.Application.Dtos.Users;
using PodkarpackiLekarz.Application.Users.Doctors.CredibilityConfirmations;
using PodkarpackiLekarz.Application.Users.Doctors.GetDoctorsToCredibilityConfirmation;
using PodkarpackiLekarz.Application.Users.Doctors.GetDoctorTypes;
using PodkarpackiLekarz.Shared.Identity;
using PodkarpackiLekarz.Shared.Models;

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
    
    [HttpPost]
    [Route("{doctorId}/rejectCredibility")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(Policy = Permissions.RejectDoctorCredibility)]
    public async Task<ActionResult<bool>> RejectDoctorCredibility(Guid doctorId)
    {
        var command = new RejectDoctorCredibilityCommand(doctorId);

        await _mediator.Send(command);

        return Ok();
    }

    [HttpGet]
    [Route("unconfirmed")]
    [Authorize(Policy = Permissions.ConfirmDoctorCredibility)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PagedResult<DoctorBasicDto>>> GetDoctorsWaitingForConfirmation(
        [FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 5)
    {
        var query = new GetDoctorsToCredibilityConfirmationQuery(pageNumber, pageSize);

        var result = await _mediator.Send(query);

        return result.Items.Any() ? Ok(result) : NotFound();        
    }

    [HttpGet]
    [Route("types")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<DoctorTypeDto>>> GetDoctorTypes()
    {
        var query = new GetDoctorTypesQuery();

        var doctorTypes = await _mediator.Send(query);

        return doctorTypes.Any() ? Ok(doctorTypes) : NotFound();
    }

    [HttpPost]
    [Route("types")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Policy = Permissions.ManageDoctors)]
    public async Task<ActionResult<Guid>> AddDoctorType([FromBody] AddDoctorTypeRequest request)
    {
        var command = request.ToCommand();

        var doctorTypeId = await _mediator.Send(command);

        return Ok(doctorTypeId);
    }
}

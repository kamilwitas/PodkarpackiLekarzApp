﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodkarpackiLekarz.Api.Requests;
using PodkarpackiLekarz.Api.Requests.Users;
using PodkarpackiLekarz.Application.Users.Administrators.CredibilityConfirmations;
using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class AdministrationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdministrationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Policy = Permissions.AddAdministrator)]
    public async Task<ActionResult<Guid>> AddAdministrator([FromBody]AddAdministratorRequest request)
    {
        var command = request.ToCommand();

        var adminId = await _mediator.Send(command);

        return Ok(adminId);
    }

    [HttpPost]
    [Route("doctor/{doctorId}/credibility")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(Policy = Permissions.ConfirmDoctorCredibility)]
    public async Task<ActionResult<bool>> ConfirmDoctorCredibility([FromQuery]Guid doctorId)
    {
        var command = new ConfirmDoctorCredibilityCommand(doctorId);

        var isConfirmed = await _mediator.Send(command);

        return Ok(isConfirmed);
    }
}

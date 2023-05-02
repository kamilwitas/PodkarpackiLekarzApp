using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PodkarpackiLekarz.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdministrationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdministrationController(IMediator mediator)
    {
        _mediator = mediator;
    }
}

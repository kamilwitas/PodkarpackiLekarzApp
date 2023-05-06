using MediatR;
using Microsoft.AspNetCore.Mvc;
using PodkarpackiLekarz.Api.Requests;
using PodkarpackiLekarz.Api.Requests.Users;
using PodkarpackiLekarz.Application.Auth;

namespace PodkarpackiLekarz.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("signIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthDto>> SignIn([FromBody]SignInRequest request)
        {
            var command = request.ToCommand();

            var authDto = await _mediator.Send(command);

            return Ok(authDto);
        }
    }
}

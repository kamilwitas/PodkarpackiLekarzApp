using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodkarpackiLekarz.Api.Requests;
using PodkarpackiLekarz.Api.Requests.Users;
using PodkarpackiLekarz.Application.Auth;
using PodkarpackiLekarz.Application.Dtos.Users;
using PodkarpackiLekarz.Application.Users.Common.GetAuthUser;
using PodkarpackiLekarz.Application.Users.Common.GetIdentityUsers;
using PodkarpackiLekarz.Application.Users.Common.SignOut;

namespace PodkarpackiLekarz.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationPrincipalService _applicationPrincipalService;

        public IdentityController(
            IMediator mediator,
            IApplicationPrincipalService applicationPrincipalService)
        {
            _mediator = mediator;
            _applicationPrincipalService = applicationPrincipalService;
        }

        [HttpPost]
        [Route("signIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn([FromBody]SignInRequest request)
        {
            var command = request.ToCommand();

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPost("signOut")]
        [Authorize]
        public async Task<IActionResult> SignOutAsync()
        {
            var command = new SignOutCommand();
            await _mediator.Send(command);
            return Ok();
        }


        [HttpGet]
        [Authorize]
        [Route("users")]
        public async Task<ActionResult<IEnumerable<IdentityUserDto>>> GetUsers()
        {
            var query = new GetIdentityUsersQuery();

            var users = await _mediator.Send(query);

            return Ok(users);
        }

        [HttpGet]
        [Authorize]
        [Route("users/logged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthUserDto>> GetAuthUser()
        {
            var loggedUserId = _applicationPrincipalService.GetUserId();

            var query = new GetAuthUserQuery(loggedUserId);

            var authUserDto = await _mediator.Send(query);

            return Ok(authUserDto);
        }
    }
}

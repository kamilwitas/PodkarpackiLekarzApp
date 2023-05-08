using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodkarpackiLekarz.Api.Requests;
using PodkarpackiLekarz.Api.Requests.Users;
using PodkarpackiLekarz.Application.Auth;
using PodkarpackiLekarz.Application.Dtos.Users;
using PodkarpackiLekarz.Application.Users.Common.GetAuthUser;
using PodkarpackiLekarz.Application.Users.Common.GetIdentityUsers;

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
        public async Task<ActionResult<AuthDto>> SignIn([FromBody]SignInRequest request)
        {
            var command = request.ToCommand();

            var authDto = await _mediator.Send(command);

            return Ok(authDto);
        }

        [HttpGet]
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

        [HttpPost]
        [AllowAnonymous]
        [Route("token/refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthDto>> RefreshAccessToken([FromBody] RefreshAccessTokenRequest request)
        {
            var command = request.ToCommand();

            var authDto = await _mediator.Send(command);

            return Ok(authDto);
        }
    }
}

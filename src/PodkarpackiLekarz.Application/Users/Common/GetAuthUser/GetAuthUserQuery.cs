using MediatR;
using PodkarpackiLekarz.Application.Dtos.Users;

namespace PodkarpackiLekarz.Application.Users.Common.GetAuthUser;

public class GetAuthUserQuery : IRequest<AuthUserDto>
{
    public Guid LoggedUserId { get; private set; }

    public GetAuthUserQuery(Guid loggedUserId)
    {
        LoggedUserId = loggedUserId;
    }
}
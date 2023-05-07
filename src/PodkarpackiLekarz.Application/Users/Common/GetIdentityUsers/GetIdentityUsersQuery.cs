using MediatR;
using PodkarpackiLekarz.Application.Dtos.Users;

namespace PodkarpackiLekarz.Application.Users.Common.GetIdentityUsers;
public class GetIdentityUsersQuery : IRequest<IEnumerable<IdentityUserDto>>
{
}

using Dapper;
using MediatR;
using PodkarpackiLekarz.Application.Dtos.Users;
using PodkarpackiLekarz.Shared.Persistence;

namespace PodkarpackiLekarz.Application.Users.Common.GetIdentityUsers;
public class GetIdentityUsersQueryHandler : IRequestHandler<GetIdentityUsersQuery, IEnumerable<IdentityUserDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetIdentityUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<IEnumerable<IdentityUserDto>> Handle(GetIdentityUsersQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sql = "SELECT " +
            $"Id AS {nameof(IdentityUserDto.Id)}, " +
            $"FirstName AS {nameof(IdentityUserDto.FirstName)}, " +
            $"LastName AS {nameof(IdentityUserDto.LastName)}, " +
            $"Email AS {nameof(IdentityUserDto.Email)}, " +
            $"Role AS {nameof(IdentityUserDto.Role)} " +
            $"FROM {AppSchema.Value}.IdentityUsers";

        var users = await connection.QueryAsync<IdentityUserDto>(sql);

        users.ToList().ForEach(x => x.TranslateEnums());

        return users;
    }
}

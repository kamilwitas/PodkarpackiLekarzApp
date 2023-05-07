using Dapper;
using MediatR;
using PodkarpackiLekarz.Application.Dtos.Users;
using PodkarpackiLekarz.Application.Exceptions.Users;
using PodkarpackiLekarz.Shared.Identity;
using PodkarpackiLekarz.Shared.Persistence;

namespace PodkarpackiLekarz.Application.Users.Common.GetAuthUser;

public class GetAuthUserQueryHandler : IRequestHandler<GetAuthUserQuery, AuthUserDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAuthUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<AuthUserDto> Handle(GetAuthUserQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sql = $"SELECT " +
                  $"Id AS {nameof(AuthUserDto.Id)}, " +
                  $"FirstName AS {nameof(AuthUserDto.FirstName)}, " +
                  $"LastName AS {nameof(AuthUserDto.LastName)}, " +
                  $"Role AS {nameof(AuthUserDto.Role)} " +
                  $"FROM {AppSchema.Value}.IdentityUsers " +
                  $"WHERE Id = @UserId";

        var loggedUser = await connection.QueryFirstAsync<AuthUserDto?>(sql, new { UserId = request.LoggedUserId});

        if (loggedUser is null)
            throw new UserNotFoundException(request.LoggedUserId);

        Enum.TryParse<Role>(loggedUser.Role, out var roleEnum);

        loggedUser.Permissions = Permissions.GetPermissions(roleEnum);
        
        loggedUser.TranslateEnums();

        return loggedUser;
    }
}
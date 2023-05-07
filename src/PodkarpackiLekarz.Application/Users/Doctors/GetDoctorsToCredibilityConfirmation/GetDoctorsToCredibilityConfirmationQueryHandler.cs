using Dapper;
using MediatR;
using PodkarpackiLekarz.Application.Dtos.Users;
using PodkarpackiLekarz.Shared.Models;
using PodkarpackiLekarz.Shared.Persistence;
using System.Text;

namespace PodkarpackiLekarz.Application.Users.Doctors.GetDoctorsToCredibilityConfirmation;
public class GetDoctorsToCredibilityConfirmationQueryHandler
    : IRequestHandler<GetDoctorsToCredibilityConfirmationQuery, PagedResult<DoctorBasicDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetDoctorsToCredibilityConfirmationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<PagedResult<DoctorBasicDto>> Handle(GetDoctorsToCredibilityConfirmationQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sqlQuery = new StringBuilder($"SELECT " +
            $"iu.Id AS {nameof(DoctorBasicDto.Id)}, " +
            $"iu.FirstName AS {nameof(DoctorBasicDto.FirstName)}, " +
            $"iu.LastName AS {nameof(DoctorBasicDto.LastName)}, " +
            $"iu.Email AS {nameof(DoctorBasicDto.Email)} " +
            $"FROM {AppSchema.Value}.IdentityUsers AS iu " +
            $"RIGHT JOIN {AppSchema.Value}.Doctors as d " +
            $"ON iu.Id = d.Id " +
            $"WHERE d.CredibilityConfirmed = 0 " +
            $"ORDER BY iu.Id DESC ");
        

        sqlQuery.AppendLine(QueryHelper.BuildPaginationSql(request.PageNumber, request.PageSize));             

        var doctorBasicDtos = await connection.QueryAsync<DoctorBasicDto>(sqlQuery.ToString());

        var countQuery = $"SELECT COUNT(iu.Id) " +
            $"FROM {AppSchema.Value}.IdentityUsers as iu " +
            $"RIGHT JOIN {AppSchema.Value}.Doctors as d " +
            $"ON iu.Id = d.id " +
            $"WHERE d.CredibilityConfirmed = 0";

        var count = await connection.QuerySingleAsync<int>(countQuery);

        var pagedResult = new PagedResult<DoctorBasicDto>(doctorBasicDtos.ToList(), count, request.PageSize, request.PageNumber);

        return pagedResult;
    }
}

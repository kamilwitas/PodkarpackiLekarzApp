using Dapper;
using MediatR;
using PodkarpackiLekarz.Application.Dtos.Users;
using PodkarpackiLekarz.Shared.Models;
using PodkarpackiLekarz.Shared.Persistence;
using System.Text;
using PodkarpackiLekarz.Core.Users.Doctors;

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

        var sql = $"select iu.id as {nameof(DoctorBasicDto.Id)}, " +
                  $"iu.FirstName as {nameof(DoctorBasicDto.FirstName)}, " +
                  $"iu.LastName as {nameof(DoctorBasicDto.LastName)}, " +
                  $"iu.Email as {nameof(DoctorBasicDto.Email)}, " +
                  $"d.CredibilityConfirmationStatus as {nameof(DoctorBasicDto.CredibilityConfirmationStatus)} " +
                  $"from {AppSchema.Value}.IdentityUsers as iu " +
                  $"right join {AppSchema.Value}.Doctors as d " +
                  $"on iu.id = d.id " +
                  $"where d.credibilityConfirmationStatus = @WaitingStatus or d.credibilityConfirmationStatus = @RejectedStatus " +
                  $"order by iu.id desc";

        var parameters = new
        {
            WaitingStatus = (int)CredibilityConfirmationStatus.Waiting,
            RejectedStatus = (int)CredibilityConfirmationStatus.Rejected
        };
        
        sql += QueryHelper.BuildPaginationSql(request.PageNumber, request.PageSize);

        var doctorBasicDtos = await connection.QueryAsync<DoctorBasicDto>(sql, parameters);

        var countQuery = $"SELECT COUNT(iu.Id) " +
            $"FROM {AppSchema.Value}.IdentityUsers as iu " +
            $"RIGHT JOIN {AppSchema.Value}.Doctors as d " +
            $"ON iu.Id = d.id " +
            $"WHERE d.CredibilityConfirmationStatus = @WaitingStatus or d.CredibilityConfirmationStatus = @RejectedStatus";

        var count = await connection.QuerySingleAsync<int>(countQuery, parameters);

        var pagedResult = new PagedResult<DoctorBasicDto>(doctorBasicDtos.ToList(), count, request.PageSize, request.PageNumber);

        return pagedResult;
    }
}

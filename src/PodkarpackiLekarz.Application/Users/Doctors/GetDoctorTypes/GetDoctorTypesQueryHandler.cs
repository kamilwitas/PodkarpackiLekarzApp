using Dapper;
using MediatR;
using PodkarpackiLekarz.Application.Dtos.Users;
using PodkarpackiLekarz.Shared.Persistence;

namespace PodkarpackiLekarz.Application.Users.Doctors.GetDoctorTypes;
public class GetDoctorTypesQueryHandler : IRequestHandler<GetDoctorTypesQuery, IEnumerable<DoctorTypeDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetDoctorTypesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<IEnumerable<DoctorTypeDto>> Handle(GetDoctorTypesQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sql = "SELECT " +
                  $"dt.Id AS {nameof(DoctorTypeDto.Id)}, " +
                  $"dt.Speciality AS {nameof(DoctorTypeDto.Speciality)} " +
                  $"FROM {AppSchema.Value}.DoctorTypes AS dt " +
                  $"ORDER BY dt.Speciality ASC";

        var doctorTypeDtos = await connection.QueryAsync<DoctorTypeDto>(sql);

        return doctorTypeDtos;
    }
}

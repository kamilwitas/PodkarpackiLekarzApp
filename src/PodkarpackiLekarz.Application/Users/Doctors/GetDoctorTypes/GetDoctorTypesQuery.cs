using MediatR;
using PodkarpackiLekarz.Application.Dtos.Users;

namespace PodkarpackiLekarz.Application.Users.Doctors.GetDoctorTypes;
public class GetDoctorTypesQuery : IRequest<IEnumerable<DoctorTypeDto>>
{
}

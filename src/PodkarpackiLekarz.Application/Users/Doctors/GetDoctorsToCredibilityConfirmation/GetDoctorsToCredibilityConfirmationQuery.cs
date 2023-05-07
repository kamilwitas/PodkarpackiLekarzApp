using MediatR;
using PodkarpackiLekarz.Application.Dtos.Users;
using PodkarpackiLekarz.Shared.Models;

namespace PodkarpackiLekarz.Application.Users.Doctors.GetDoctorsToCredibilityConfirmation;
public class GetDoctorsToCredibilityConfirmationQuery : IRequest<PagedResult<DoctorBasicDto>>
{
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }

    public GetDoctorsToCredibilityConfirmationQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

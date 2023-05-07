namespace PodkarpackiLekarz.Application.Dtos.Users;
public class DoctorDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Guid SpecialityId { get; set; }
    public string Speciality { get; set; }
    public string DoctorDescription { get; set; }
}

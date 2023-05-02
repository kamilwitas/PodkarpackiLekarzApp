namespace PodkarpackiLekarz.Core.Users.Doctors;
public interface IDoctorTypesRepository
{
    Task<DoctorType?> GetAsync(Guid id);
    Task AddAsync(DoctorType doctorType);
    Task SaveAsync();
}

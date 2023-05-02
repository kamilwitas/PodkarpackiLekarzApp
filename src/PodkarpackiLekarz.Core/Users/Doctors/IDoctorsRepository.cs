namespace PodkarpackiLekarz.Core.Users.Doctors;
public interface IDoctorsRepository
{
    Task AddAsync(Doctor doctor);
    void UpdateAsync(Doctor doctor);
    Task<Doctor?> GetAsync(Guid id);
    Task<Doctor?> GetAsync(string email);
    Task SaveAsync();
}

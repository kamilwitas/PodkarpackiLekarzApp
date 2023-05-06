namespace PodkarpackiLekarz.Core.Users.Doctors;
public interface IDoctorsRepository
{
    Task AddAsync(Doctor doctor);
    void Update(Doctor doctor);
    Task<Doctor?> GetAsync(Guid id);
    Task<Doctor?> GetAsync(string email);
    Task SaveAsync();
    Task<bool?> IsCredibilityConfirmed(Guid id);
}

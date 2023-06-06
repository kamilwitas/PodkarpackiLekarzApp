namespace PodkarpackiLekarz.Core.Users.Doctors;
public interface IDoctorsRepository
{
    Task AddAsync(Doctor doctor);
    void Update(Doctor doctor);
    Task<Doctor?> GetAsync(Guid id);
    Task<Doctor?> GetAsync(string email);
    Task<bool> IsExist(Guid doctorId);
    Task SaveAsync();
    Task<CredibilityConfirmationStatus?> GetCredibilityConfirmationStatus(Guid doctorId);
}

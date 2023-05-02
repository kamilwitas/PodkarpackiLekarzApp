namespace PodkarpackiLekarz.Core.Users.Patients;
public interface IPatientsRepository
{
    Task AddAsync(Patient patient);
    Task<Patient?> GetAsync(Guid id);
    Task<Patient?> GetAsync(string email);
    Task SaveAsync();
}

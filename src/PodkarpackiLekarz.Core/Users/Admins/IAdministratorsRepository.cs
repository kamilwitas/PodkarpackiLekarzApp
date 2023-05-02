namespace PodkarpackiLekarz.Core.Users.Admins;
public interface IAdministratorsRepository
{
    Task AddAsync(Administrator administrator);
    Task<Administrator?> GetAsync(Guid id);
    Task<Administrator?> GetAsync(string email);
    Task SaveAsync();
}

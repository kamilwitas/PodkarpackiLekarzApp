namespace PodkarpackiLekarz.Core.Users.Admins;
public interface IAdministratorsRepository
{
    Task AddAsync(Administrator administrator);
    void Add(Administrator administrator);
    Task<Administrator?> GetAsync(Guid id);
    Administrator? Get(Guid id);
    Task<Administrator?> GetAsync(string email);
    Task SaveAsync();
    void Save();
}

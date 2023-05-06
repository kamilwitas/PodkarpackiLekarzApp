using PodkarpackiLekarz.Core.Users.Base;

namespace PodkarpackiLekarz.Core.Users;
public interface IIdentityUsersRepository
{
    Task<IdentityUser?> GetAsync(Guid id);
    Task<IdentityUser?> GetAsync(string email);
    Task<bool> IsExist(Guid id);
    Task<bool> IsExist(string email);
    void UpdateAsync(IdentityUser user);
    Task SaveAsync();
}

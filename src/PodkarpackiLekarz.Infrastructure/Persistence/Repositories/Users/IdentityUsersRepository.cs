using Microsoft.EntityFrameworkCore;
using PodkarpackiLekarz.Core.Users;

namespace PodkarpackiLekarz.Infrastructure.Persistence.Repositories.Users;
public class IdentityUsersRepository : IIdentityUsersRepository
{
    private readonly AppDbContext _dbContext;

    public IdentityUsersRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IdentityUser?> GetAsync(Guid id)
        => await _dbContext.IdentityUsers.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IdentityUser?> GetAsync(string email)
        => await _dbContext.IdentityUsers.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

    public async Task<bool> IsExist(Guid id)
        => await _dbContext.IdentityUsers.AnyAsync(x => x.Id == id);

    public Task<bool> IsExist(string email)
        => _dbContext.IdentityUsers.AnyAsync(x => x.Email.ToLower() == email.ToLower());
}

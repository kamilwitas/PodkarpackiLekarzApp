using Microsoft.EntityFrameworkCore;
using PodkarpackiLekarz.Core.Users.Admins;

namespace PodkarpackiLekarz.Infrastructure.Persistence.Repositories.Users;
public class AdministratorsRepository : IAdministratorsRepository
{
    private readonly AppDbContext _dbContext;

    public AdministratorsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Administrator administrator)
    {
        await _dbContext.Administrators.AddAsync(administrator);
    }

    public async Task<Administrator?> GetAsync(Guid id)
        => await _dbContext.Administrators.FirstOrDefaultAsync(x => x.Id == id);    

    public async Task<Administrator?> GetAsync(string email)
        => await _dbContext.Administrators.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}

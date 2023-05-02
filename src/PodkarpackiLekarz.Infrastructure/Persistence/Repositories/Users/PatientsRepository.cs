using Microsoft.EntityFrameworkCore;
using PodkarpackiLekarz.Core.Users.Patients;

namespace PodkarpackiLekarz.Infrastructure.Persistence.Repositories.Users;
public class PatientsRepository : IPatientsRepository
{
    private readonly AppDbContext _dbContext;

    public PatientsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Patient patient)
    {
        await _dbContext.AddAsync(patient);
    }

    public async Task<Patient?> GetAsync(Guid id)
        => await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Patient?> GetAsync(string email)
        => await _dbContext.Patients.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}

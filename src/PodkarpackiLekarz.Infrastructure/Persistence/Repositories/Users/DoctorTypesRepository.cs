using Microsoft.EntityFrameworkCore;
using PodkarpackiLekarz.Core.Users.Doctors;

namespace PodkarpackiLekarz.Infrastructure.Persistence.Repositories.Users;
public class DoctorTypesRepository : IDoctorTypesRepository
{
    private readonly AppDbContext _dbContext;

    public DoctorTypesRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DoctorType?> GetAsync(string speciality)
        => await _dbContext.DoctorTypes
            .FirstOrDefaultAsync(x => x.Speciality.ToLower() == speciality.ToLower());

    public async Task AddAsync(DoctorType doctorType)
    {
        await _dbContext.DoctorTypes.AddAsync(doctorType);
    }

    public async Task<DoctorType?> GetAsync(Guid id)
        => await _dbContext.DoctorTypes.FirstOrDefaultAsync(x => x.Id == id);

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}

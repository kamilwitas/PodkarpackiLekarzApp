﻿using Microsoft.EntityFrameworkCore;
using PodkarpackiLekarz.Core.Users.Doctors;

namespace PodkarpackiLekarz.Infrastructure.Persistence.Repositories.Users;
public class DoctorsRepository : IDoctorsRepository
{
    private readonly AppDbContext _dbContext;

    public DoctorsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Doctor doctor)
    {
        await _dbContext.Doctors.AddAsync(doctor);
    }

    public Task<Doctor?> GetAsync(Guid id)
        => _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id); 

    public async Task<Doctor?> GetAsync(string email)
        => await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Email == email);

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void UpdateAsync(Doctor doctor)
    {
        _dbContext.Doctors.Update(doctor);
    }
}
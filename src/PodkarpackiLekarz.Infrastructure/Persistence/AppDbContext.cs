using Microsoft.EntityFrameworkCore;
using PodkarpackiLekarz.Core.Users;
using PodkarpackiLekarz.Core.Users.Doctors;
using PodkarpackiLekarz.Infrastructure.Persistence.EntityConfigurations.Users;

namespace PodkarpackiLekarz.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public DbSet<IdentityUser> IdentityUsers { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<DoctorType> DoctorTypes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("PLA");
        modelBuilder.ApplyConfiguration(new IdentityUserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorTypeEntityConfiguration());
    }
}

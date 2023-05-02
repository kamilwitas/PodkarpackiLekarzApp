using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PodkarpackiLekarz.Core.Users.Doctors;

namespace PodkarpackiLekarz.Infrastructure.Persistence.EntityConfigurations.Users;
internal class DoctorEntityConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");
        builder.OwnsOne(x => x.DoctorProfile)
            .ToTable("DoctorProfiles")
            .WithOwner()
            .HasForeignKey("identityUserId");
        builder.HasIndex(x => x.Email);
            
    }
}

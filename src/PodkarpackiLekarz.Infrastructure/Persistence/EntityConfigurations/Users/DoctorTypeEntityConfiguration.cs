using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PodkarpackiLekarz.Core.Users.Doctors;

namespace PodkarpackiLekarz.Infrastructure.Persistence.EntityConfigurations.Users;
public class DoctorTypeEntityConfiguration : IEntityTypeConfiguration<DoctorType>
{
    public void Configure(EntityTypeBuilder<DoctorType> builder)
    {
        builder.ToTable("DoctorTypes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();        
    }
}

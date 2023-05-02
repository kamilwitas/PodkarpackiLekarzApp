using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PodkarpackiLekarz.Core.Users.Patients;

namespace PodkarpackiLekarz.Infrastructure.Persistence.EntityConfigurations.Users;
internal class PatientEntityConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");
        builder.Property(x => x.Pesel)
            .IsRequired(true);

        builder.HasIndex(x => x.Pesel);
    }
}

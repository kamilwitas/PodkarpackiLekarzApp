using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PodkarpackiLekarz.Core.Users.Admins;

namespace PodkarpackiLekarz.Infrastructure.Persistence.EntityConfigurations.Users;
internal class AdministratorEntityConfiguration : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.ToTable("Administrators");

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasIndex(x => x.Email)
            .IsUnique(true);
    }
}

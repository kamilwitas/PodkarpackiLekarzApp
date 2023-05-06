using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PodkarpackiLekarz.Core.Users;

namespace PodkarpackiLekarz.Infrastructure.Persistence.EntityConfigurations.Users;
internal class UserSessionEntityConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.ToTable("UserSessions");
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
    }
}

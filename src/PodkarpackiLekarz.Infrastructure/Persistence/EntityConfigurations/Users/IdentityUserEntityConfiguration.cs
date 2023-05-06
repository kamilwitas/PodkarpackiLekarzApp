using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PodkarpackiLekarz.Core.Users;

namespace PodkarpackiLekarz.Infrastructure.Persistence.EntityConfigurations.Users;
internal class IdentityUserEntityConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.ToTable("IdentityUsers");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasIndex(x => x.Email);

        builder.HasOne(x => x.Session)
            .WithOne(x => x.IdentityUser)
            .HasForeignKey<UserSession>("UserId")
            .OnDelete(DeleteBehavior.Cascade);            
    }
    
}

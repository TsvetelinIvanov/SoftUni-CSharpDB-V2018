using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Username).IsUnicode().IsRequired();

            builder.Property(u => u.Password).IsUnicode().IsRequired();
            //builder.Property(u => u.Password).IsUnicode(false).IsRequired();

            builder.Property(u => u.Email).IsUnicode().IsRequired();
            //builder.Property(u => u.Email).IsUnicode(false).IsRequired();

            builder.Property(u => u.Name).IsUnicode().IsRequired();
        }
    }
}
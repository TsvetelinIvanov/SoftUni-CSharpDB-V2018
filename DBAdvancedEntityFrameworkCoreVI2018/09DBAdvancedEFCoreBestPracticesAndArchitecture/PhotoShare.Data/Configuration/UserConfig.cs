using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoShare.Models;

namespace PhotoShare.Data.Configuration
{
    class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasMaxLength(30);

            builder.HasIndex(u => u.Username)
                   .IsUnique();

            builder.Property(u => u.Password)
                   .IsRequired();

            builder.Property(u => u.Email)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(u => u.FirstName)
                   .IsRequired(false)
                   .IsUnicode()
                   .HasMaxLength(60);

            builder.Property(u => u.LastName)
                   .IsRequired(false)
                   .IsUnicode()
                   .HasMaxLength(60);

            builder.Ignore(u => u.FullName);            

            builder.Property(u => u.RegisteredOn)
                   .IsRequired(false)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.LastTimeLoggedIn)
                   .IsRequired(false);

            builder.Property(u => u.Age)
                   .IsRequired(false);

            builder.Property(u => u.IsDeleted)
                   .IsRequired(true);

            builder.HasOne(u => u.ProfilePicture)
                   .WithOne(p => p.UserProfile)
                   .HasForeignKey<User>(u => u.ProfilePictureId);

            builder.HasOne(u => u.BornTown)
                   .WithMany(t => t.UsersBornInTown)
                   .HasForeignKey(u => u.BornTownId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.CurrentTown)
                   .WithMany(t => t.UsersCurrentlyLivingInTown)
                   .HasForeignKey(u => u.CurrentTownId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
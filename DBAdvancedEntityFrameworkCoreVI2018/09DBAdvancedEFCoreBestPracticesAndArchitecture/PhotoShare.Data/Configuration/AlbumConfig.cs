using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoShare.Models;

namespace PhotoShare.Data.Configuration
{
    internal class AlbumConfig : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                   .IsRequired()
                   .IsUnicode()
                   .HasMaxLength(50);

            builder.Property(a => a.BackgroundColor)
                   .IsRequired(false);

            builder.Property(a => a.IsPublic)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
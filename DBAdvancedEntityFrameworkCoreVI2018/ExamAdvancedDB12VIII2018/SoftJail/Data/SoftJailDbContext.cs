using Microsoft.EntityFrameworkCore;
using SoftJail.Data.Models;

namespace SoftJail.Data
{
    public class SoftJailDbContext : DbContext
	{
		public SoftJailDbContext()
		{

		}

		public SoftJailDbContext(DbContextOptions options) : base(options)
		{

		}

        public DbSet<Prisoner> Prisoners { get; set; }

        public DbSet<Officer> Officers { get; set; }

        public DbSet<Cell> Cells { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Mail> Mails { get; set; }

        public DbSet<OfficerPrisoner> OfficersPrisoners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
            builder.Entity<OfficerPrisoner>().HasKey(po => new { po.OfficerId, po.PrisonerId });

            builder.Entity<OfficerPrisoner>().HasOne(po => po.Officer)
               .WithMany(o => o.OfficerPrisoners)
               .HasForeignKey(po => po.OfficerId);
            //.OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OfficerPrisoner>().HasOne(po => po.Prisoner)
                .WithMany(p => p.PrisonerOfficers)
                .HasForeignKey(po => po.PrisonerId);
            //.OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OfficerPrisoner>().ToTable("OfficersPrisoners");
        }
    }
}
using Microsoft.EntityFrameworkCore;
using PetClinic.Models;

namespace PetClinic.Data
{
    public class PetClinicContext : DbContext
    {
        public PetClinicContext()
        {

        }

        public PetClinicContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<AnimalAid> AnimalAids { get; set; }

        public DbSet<Passport> Passports { get; set; }

        public DbSet<Procedure> Procedures { get; set; }

        public DbSet<ProcedureAnimalAid> ProceduresAnimalAids { get; set; }

        public DbSet<Vet> Vets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProcedureAnimalAid>().HasKey(paa => new { paa.AnimalAidId, paa.ProcedureId });

            //builder.Entity<ProcedureAnimalAid>().HasOne(paa => paa.AnimalAid)
            //    .WithMany(aa => aa.AnimalAidProcedures)
            //    .HasForeignKey(paa => paa.AnimalAidId);
            //  //.OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<ProcedureAnimalAid>().HasOne(paa => paa.Procedure)
            //    .WithMany(p => p.ProcedureAnimalAids)
            //    .HasForeignKey(paa => paa.ProcedureId);
            //  //.OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AnimalAid>().HasIndex(aa => aa.Name).IsUnique();

            builder.Entity<Vet>().HasIndex(v => v.PhoneNumber).IsUnique();

            //builder.Entity<Animal>().HasOne(a => a.Passport)
            //    .WithOne(p => p.Animal)
            //    .HasForeignKey(a => a.PassportSerialNumber);
        }
    }
}

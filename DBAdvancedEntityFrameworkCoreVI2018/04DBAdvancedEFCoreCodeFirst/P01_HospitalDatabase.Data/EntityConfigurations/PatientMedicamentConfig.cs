using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.EntityConfigurations
{
    public class PatientMedicamentConfig : IEntityTypeConfiguration<PatientMedicament>
    {
        public void Configure(EntityTypeBuilder<PatientMedicament> builder)
        {
            builder.HasKey(pm => new { pm.MedicamentId, pm.PatientId });

            //builder.HasOne(pm => pm.Patient).WithMany(pm => pm.Prescriptions).HasForeignKey(pm => pm.PatientId);
        }
    }
}
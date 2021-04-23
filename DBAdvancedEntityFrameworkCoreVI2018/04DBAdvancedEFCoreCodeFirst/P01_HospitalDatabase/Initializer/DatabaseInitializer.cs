using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Generators;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Initializer
{
    public class DatabaseInitializer
    {        
        public static void ResetDatabase()
        {
            using (HospitalContext context = new HospitalContext())
            {
                context.Database.EnsureDeleted();

                context.Database.Migrate();

                InitialSeed(context);
            }
        }

        public static void InitialSeed(HospitalContext context)
        {
            SeedMedicaments(context);

            SeedPatients(context, 200);

            SeedPrescriptions(context);
        }

        private static void SeedMedicaments(HospitalContext context)
        {
            MedicamentGenerator.InitialMedicamentSeed(context);
        }

        public static void SeedPatients(HospitalContext context, int count)
        {
            for (int i = 0; i < count; i++)
            {
                context.Patients.Add(PatientGenerator.NewPatient(context));
            }

            context.SaveChanges();
        }

        private static void SeedPrescriptions(HospitalContext context)
        {
            PrescriptionGenerator.InitialPrescriptionSeed(context);
        }
    }
}
using System;
using System.IO;
using AutoMapper;
using PetClinic.Data;

namespace PetClinic.App
{
    public class StartUp
    {
        static void Main()
        {
            using (PetClinicContext context = new PetClinicContext())
            {
                Mapper.Initialize(config => config.AddProfile<PetClinicProfile>());

                ResetDatabase(context);

                ImportEntities(context);

                ExportEntities(context);
				
		BonusTask(context);
            }
        }

        private static void ResetDatabase(PetClinicContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Console.WriteLine("Database reset.");
        }

        private static void ImportEntities(PetClinicContext context, string baseDirectory = @"..\Datasets\")
        {
            //baseDirectory = @"Datasets\";
            //baseDirectory = @"PetClinic\App\Datasets\";
            //baseDirectory = @"ExamAdvancedDB5I2018\PetClinic\App\Datasets\";
            //baseDirectory = @"[Absolute Local Path]";

            const string exportDirectory = "./ExportResults/";
            //const string exportDirectory = @"ExportResults\";
            //const string exportDirectory = @"PetClinic\App\ExportResults\";
            //const string exportDirectory = @"ExamAdvancedDB5I2018\PetClinic\App\ExportResults\";
            //const string exportDirectory = @"[Absolute Local Path]";

            string animalAids = DataProcessor.Deserializer.ImportAnimalAids(context, File.ReadAllText(baseDirectory + "animalAids.json"));
            PrintAndExportEntityToFile(animalAids, exportDirectory + "AnimalAidsImport.txt");

            string animals = DataProcessor.Deserializer.ImportAnimals(context, File.ReadAllText(baseDirectory + "animals.json"));
            PrintAndExportEntityToFile(animals, exportDirectory + "AnimalsImport.txt");

            string vets = DataProcessor.Deserializer.ImportVets(context, File.ReadAllText(baseDirectory + "vets.xml"));
            PrintAndExportEntityToFile(vets, exportDirectory + "VetsImport.txt");

            string procedures = DataProcessor.Deserializer.ImportProcedures(context, File.ReadAllText(baseDirectory + "procedures.xml"));
            PrintAndExportEntityToFile(procedures, exportDirectory + "ProceduresImport.txt");
        }

        private static void ExportEntities(PetClinicContext context)
        {
            const string exportDirectory = "./ExportResults/";
            //const string exportDirectory = @"ExportResults\";
            //const string exportDirectory = @"PetClinic\App\ExportResults\";
            //const string exportDirectory = @"ExamAdvancedDB5I2018\PetClinic\App\ExportResults\";
            //const string exportDirectory = @"[Absolute Local Path]";

            string animalsExport = DataProcessor.Serializer.ExportAnimalsByOwnerPhoneNumber(context, "0887446123");
            PrintAndExportEntityToFile(animalsExport, exportDirectory + "AnimalsExport.json");

            string proceduresExport = DataProcessor.Serializer.ExportAllProcedures(context);
            PrintAndExportEntityToFile(proceduresExport, exportDirectory + "ProceduresExport.xml");
        }
		
	private static void BonusTask(PetClinicContext context)
        {
            string bonusOutput = DataProcessor.Bonus.UpdateVetProfession(context, "+359284566778", "Primary Care");
            Console.WriteLine(bonusOutput);
        }

        private static void PrintAndExportEntityToFile(string entityOutput, string outputPath)
        {
            Console.WriteLine(entityOutput);
            File.WriteAllText(outputPath, entityOutput.TrimEnd());
        }        
    }
}

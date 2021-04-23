using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;
using System;
//using System.IO;

namespace P01_HospitalDatabase.Generators
{
    public class PatientGenerator
    {
        private static Random random = new Random();

        public static Patient NewPatient(HospitalContext context)
        {
            string firstName = NameGenerator.FirstName();
            string lastName = NameGenerator.LastName();

            Patient patient = new Patient()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = EmailGenerator.NewEmail(firstName + lastName),
                Address = AddressGenerator.NewAddress(),
            };

            patient.Visitations = GenerateVisitations(patient);
            patient.Diagnoses = GenerateDiagnoses(patient);

            return patient;
        }

        private static Visitation[] GenerateVisitations(Patient patient)
        {
            int visitationsCount = random.Next(1, 5);
            Visitation[] visitations = new Visitation[visitationsCount];

            for (int i = 0; i < visitationsCount; i++)
            {
                DateTime visitationDate = RandomDay(2005);

                Visitation visitation = new Visitation()
                {
                    Date = visitationDate,
                    Patient = patient
                };

                visitations[i] = visitation;
            }

            return visitations;
        }

        private static DateTime RandomDay(int startYear)
        {
            DateTime start = new DateTime(startYear, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        private static Diagnose[] GenerateDiagnoses(Patient patient)
        {
            //var diagnoseNames = File.ReadAllLines("<INSERT DIR HERE>");
            string[] diagnoseNames = new string[] 
            {
                "Limp Scurvy",
                "Fading Infection",
                "Cow Feet",
                "Incurable Ebola",
                "Snake Blight",
                "Spider Asthma",
                "Sinister Body",
                "Spine Diptheria",
                "Pygmy Decay",
                "King's Arthritis",
                "Desert Rash",
                "Deteriorating Salmonella",
                "Shadow Anthrax",
                "Hiccup Meningitis",
                "Fading Depression",
                "Lion Infertility",
                "Wolf Delirium",
                "Humming Measles",
                "Incurable Stomach",
                "Grave Heart",
            };           

            int diagnoseCount = random.Next(1, 4);
            var diagnoses = new Diagnose[diagnoseCount];
            for (int i = 0; i < diagnoseCount; i++)
            {
                string diagnoseName = diagnoseNames[random.Next(diagnoseNames.Length)];

                var diagnose = new Diagnose()
                {
                    Name = diagnoseName,
                    Patient = patient
                };

                diagnoses[i] = diagnose;
            }

            return diagnoses;
        }        
    }
}
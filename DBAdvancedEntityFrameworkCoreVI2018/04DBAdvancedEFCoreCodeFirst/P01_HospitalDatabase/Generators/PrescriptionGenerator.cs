﻿using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace P01_HospitalDatabase.Generators
{
    public class PrescriptionGenerator
    {
        internal static void InitialPrescriptionSeed(HospitalContext context)
        {
            Random random = new Random();

            int[] allMedicamentIds = context.Medicaments.Select(d => d.MedicamentId).ToArray();

            int[] allPatientIds = context.Patients.Select(p => p.PatientId).ToArray();

            foreach (int patientId in allPatientIds)
            {
                int patientMedicamentsCount = random.Next(1, 4);

                int[] medicamentIds = new int[patientMedicamentsCount];

                for (int id = 0; id < patientMedicamentsCount; id++)
                {
                    int index = -1;

                    while (!allMedicamentIds.Contains(index) || medicamentIds.Contains(index))
                    {
                        index = random.Next(allMedicamentIds.Max());
                    }

                    medicamentIds[id] = index;
                }

                List<PatientMedicament> prescriptions = new List<PatientMedicament>();
                foreach (int medicamentId in medicamentIds)
                {
                    PatientMedicament prescription = new PatientMedicament()
                    {
                        PatientId = patientId,
                        MedicamentId = medicamentId
                    };

                    prescriptions.Add(prescription);
                }

                context.Patients.Find(patientId).Prescriptions = prescriptions;
            }

            context.SaveChanges();
        }

        public static void NewPrescription(int patientId, int medicamentId, HospitalContext context)
        {
            PatientMedicament prescription = new PatientMedicament()
            {
                PatientId = patientId,
                MedicamentId = medicamentId
            };

            context.Patients.Find(patientId).Prescriptions.Add(prescription);
            context.SaveChanges();
        }

        public static void NewPrescription(Patient patient, Medicament medicament, HospitalContext context)
        {
            PatientMedicament prescription = new PatientMedicament()
            {
                Patient = patient,
                Medicament = medicament
            };

            patient.Prescriptions.Add(prescription);
            context.SaveChanges();
        }
    }
}
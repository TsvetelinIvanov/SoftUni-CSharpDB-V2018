using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PetClinic.Data;
using PetClinic.DataProcessor.ImportDtos;
using PetClinic.Models;

namespace PetClinic.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Error: Invalid data.";        

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            AnimalAidDto[] deserializedAnimalAids = JsonConvert.DeserializeObject<AnimalAidDto[]>(jsonString);

            StringBuilder messageBuilder = new StringBuilder();
            List<AnimalAid> animalAids = new List<AnimalAid>();
            foreach (AnimalAidDto animalAidDto in deserializedAnimalAids)
            {
                if (!IsValid(animalAidDto) || animalAids.Any(aa => aa.Name == animalAidDto.Name))
                {
                    messageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                AnimalAid animalAid = new AnimalAid
                {
                    Name = animalAidDto.Name,
                    Price = animalAidDto.Price
                };

                animalAids.Add(animalAid);
                messageBuilder.AppendLine($"Record {animalAid.Name} successfully imported.");
            }

            context.AnimalAids.AddRange(animalAids);
            context.SaveChanges();

            return messageBuilder.ToString().TrimEnd();
        }        

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            AnimalDto[] deserializedAnimals = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString);

            StringBuilder messageBuilder = new StringBuilder();
            List<Animal> animals = new List<Animal>();
            foreach (AnimalDto animalDto in deserializedAnimals)
            {
                if (!IsValid(animalDto) || !IsValid(animalDto.Passport) || animals.Any(a => a.Passport.SerialNumber == animalDto.Passport.SerialNumber))
                {
                    messageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Animal animal = new Animal
                {
                    Name = animalDto.Name,
                    Type = animalDto.Type,
                    Age = animalDto.Age,
                    Passport = new Passport
                    {
                        SerialNumber = animalDto.Passport.SerialNumber,
                        OwnerName = animalDto.Passport.OwnerName,
                        OwnerPhoneNumber = animalDto.Passport.OwnerPhoneNumber,
                        RegistrationDate = DateTime.ParseExact(animalDto.Passport.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                    }
                };

                animals.Add(animal);
                messageBuilder.AppendLine($"Record {animal.Name} Passport №: {animal.Passport.SerialNumber} successfully imported.");
            }

            context.Animals.AddRange(animals);
            context.SaveChanges();

            return messageBuilder.ToString().TrimEnd();
        }
        
        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));
            VetDto[] deserializedVets = (VetDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            StringBuilder resultMessageBuilder = new StringBuilder();
            List<Vet> vets = new List<Vet>();
            foreach (VetDto vetDto in deserializedVets)
            {
                if (!IsValid(vetDto) || vets.Any(v => v.PhoneNumber == vetDto.PhoneNumber))
                {
                    resultMessageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Vet vet = new Vet
                {
                    Name = vetDto.Name,
                    Profession = vetDto.Profession,
                    Age = vetDto.Age,
                    PhoneNumber = vetDto.PhoneNumber
                };

                vets.Add(vet);
                resultMessageBuilder.AppendLine($"Record {vet.Name} successfully imported.");
            }

            context.Vets.AddRange(vets);
            context.SaveChanges();

            return resultMessageBuilder.ToString().TrimEnd();
        }        

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            ProcedureDto[] deserializedProcedures = (ProcedureDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            StringBuilder resultMessageBuilder = new StringBuilder();
            List<ProcedureAnimalAid> procedureAnimalAids = new List<ProcedureAnimalAid>();
            List<Procedure> procedures = new List<Procedure>();
            foreach (ProcedureDto procedureDto in deserializedProcedures)
            {
                if (!IsValid(procedureDto))
                {
                    resultMessageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                bool isValidAnimalAidName = true;
                foreach (ProcedureAnimalAidDto animalAidDto in procedureDto.ProcedureAnimalAidDtos)
                {
                    if (!IsValid(animalAidDto))
                    {
                        resultMessageBuilder.AppendLine(ErrorMessage);
                        isValidAnimalAidName = false;
                        break;
                    }
                }

                if (!isValidAnimalAidName)
                {
                    continue;
                }

                Vet vet = context.Vets.FirstOrDefault(v => v.Name == procedureDto.Vet);
                if (vet == null)
                {
                    resultMessageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Animal animal = context.Animals.FirstOrDefault(a => a.PassportSerialNumber == procedureDto.Animal);
                if (animal == null)
                {
                    resultMessageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                bool isExistingAndSingleAnimalAidName = true;
                List<string> animalAidNames = new List<string>();
                foreach (ProcedureAnimalAidDto animalAidDto in procedureDto.ProcedureAnimalAidDtos)
                {
                    bool animalAidExists = context.AnimalAids.Any(aa => aa.Name == animalAidDto.Name);
                    if (!animalAidExists || animalAidNames.Contains(animalAidDto.Name))
                    {
                        resultMessageBuilder.AppendLine(ErrorMessage);
                        isExistingAndSingleAnimalAidName = false;
                        break;
                    }

                    animalAidNames.Add(animalAidDto.Name);
                }

                if (!isExistingAndSingleAnimalAidName)
                {
                    continue;
                }

                DateTime dateTime = DateTime.ParseExact(procedureDto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                //bool isDateValid = DateTime.TryParseExact(procedureDto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime);
                //if (!isDateValid)
                //{
                //    resultMessageBuilder.AppendLine(ErrorMessage);
                //    continue;
                //}

                Procedure procedure = new Procedure
                {
                    Vet = vet,
                    Animal = animal, 
                    DateTime = dateTime
                };

                procedures.Add(procedure);

                foreach (ProcedureAnimalAidDto animalAidDto in procedureDto.ProcedureAnimalAidDtos)
                {
                    AnimalAid animalAid = context.AnimalAids.FirstOrDefault(aa => aa.Name == animalAidDto.Name);
                    ProcedureAnimalAid procedureAnimalAid = new ProcedureAnimalAid
                    {
                        AnimalAid = animalAid,
                        Procedure = procedure
                    };

                    procedureAnimalAids.Add(procedureAnimalAid);
                }

                resultMessageBuilder.AppendLine("Record successfully imported.");
            }

            context.Procedures.AddRange(procedures);
            context.SaveChanges();

            context.ProceduresAnimalAids.AddRange(procedureAnimalAids);
            context.SaveChanges();

            return resultMessageBuilder.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}
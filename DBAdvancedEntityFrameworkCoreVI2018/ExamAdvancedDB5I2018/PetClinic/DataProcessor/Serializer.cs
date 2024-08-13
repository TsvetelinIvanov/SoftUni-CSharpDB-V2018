using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PetClinic.Data;
using PetClinic.DataProcessor.ExportDtos;

namespace PetClinic.DataProcessor
{
    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context.Animals.Where(a => a.Passport.OwnerPhoneNumber == phoneNumber)
                .OrderBy(a => a.Age)
                .ThenBy(a => a.PassportSerialNumber)
                .Select(a => new
                {
                    a.Passport.OwnerName,
                    AnimalName = a.Name,
                    a.Age,
                    SerialNumber = a.PassportSerialNumber,
                    RegisteredOn = a.Passport.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                })
                .ToArray();

            string jsonString = JsonConvert.SerializeObject(animals, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
        }        

        public static string ExportAllProcedures(PetClinicContext context)
        {
            ExportProcedureDto[] exportProcedureDtos = context.Procedures
                .OrderBy(p => p.DateTime)
                .ThenBy(p => p.Animal.PassportSerialNumber)
                .Select(p => new ExportProcedureDto
                {
                    Passport = p.Animal.PassportSerialNumber,
                    OwnerNumber = p.Animal.Passport.OwnerPhoneNumber,
                    DateTime = p.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    AnimalAids = p.ProcedureAnimalAids.Select(paa => new ExportAnimalAidDto
                    {
                        Name = paa.AnimalAid.Name,
                        Price = paa.AnimalAid.Price
                    })
                    .ToArray(),
                    //TotalPrice = p.Cost
                    TotalPrice = p.ProcedureAnimalAids.Sum(paa => paa.AnimalAid.Price)
                })
                .ToArray();

            StringBuilder exportProceduresBuilder = new StringBuilder();
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty});
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportProcedureDto[]), new XmlRootAttribute("Procedures"));
            xmlSerializer.Serialize(new StringWriter(exportProceduresBuilder), exportProcedureDtos, xmlSerializerNamespaces);

            return exportProceduresBuilder.ToString();
        }
    }
}

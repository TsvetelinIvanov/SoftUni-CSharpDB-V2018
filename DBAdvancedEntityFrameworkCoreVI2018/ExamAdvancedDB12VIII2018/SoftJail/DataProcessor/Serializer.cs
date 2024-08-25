using SoftJail.Data;
using SoftJail.DataProcessor.ExportDto;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor
{
    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context.Prisoners.Where(p => ids.Any(i => i == p.Id))
                .OrderBy(p => p.FullName)
                .ThenBy(p => p.Id)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers
                    .OrderBy(po => po.Officer.FullName)
                    .ThenBy(po => po.Officer.Id)
                    .Select(po => new
                    {
                        OfficerName = po.Officer.FullName,
                        Department = po.Officer.Department.Name
                    })                   
                    .ToArray(),
                    TotalOfficerSalary =  p.PrisonerOfficers.Sum(po => po.Officer.Salary)
                })
                .ToArray();

            string jsonString = JsonConvert.SerializeObject(prisoners, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            string[] prisonersNamesArray = prisonersNames.Split(',');
            ExportPrisonerDto[] exportPrisonerDtos = context.Prisoners
                .Where(p => prisonersNamesArray.Contains(p.FullName))
                .OrderBy(p => p.FullName)
                .ThenBy(p => p.Id)
                .Select(p => new ExportPrisonerDto
                {
                    Id = p.Id,
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    EncryptedMessages = p.Mails.Select(m => new MessageDto
                    {
                        Description = ReverseDescription(m.Description)
                    })
                    .ToArray()
                })
                .ToArray();

            StringBuilder exportPrisonerBuilder = new StringBuilder();
            XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty});
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportPrisonerDto[]), new XmlRootAttribute("Prisoners"));
            xmlSerializer.Serialize(new StringWriter(exportPrisonerBuilder), exportPrisonerDtos, serializerNamespaces);

            return exportPrisonerBuilder.ToString();
        }

        //private static string ReverseDescription(string description)
        //{
        //    string reversedDescription = string.Empty;
        //    for (int i = description.Length - 1; i >= 0; i--)
        //    {
        //        reversedDescription += description[i];
        //    }

        //    return reversedDescription;
        //}

        private static string ReverseDescription(string description)
        {
            char[] descriptionCharArray = description.ToCharArray();
            Array.Reverse(descriptionCharArray);
            string reversedDescription = new string(descriptionCharArray);

            return reversedDescription;
        }
    }
}

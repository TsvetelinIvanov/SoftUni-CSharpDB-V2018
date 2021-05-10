using SoftJail.Data;
using Newtonsoft.Json;
using SoftJail.Data.Models;
using SoftJail.Data.Models.Enums;
using SoftJail.DataProcessor.ImportDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            DepartmentDto[] deserializedDepartments = JsonConvert.DeserializeObject<DepartmentDto[]>(jsonString);

            StringBuilder messageBuilder = new StringBuilder();
            List<Department> departments = new List<Department>();
            //List<Cell> cells = new List<Cell>();
            foreach (DepartmentDto departmentDto in deserializedDepartments)
            {
                if (!IsValid(departmentDto))
                {
                    messageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                bool isCellValid = true;
                foreach (CellDto cellDto in departmentDto.Cells)
                {
                    if (!IsValid(cellDto))
                    {
                        messageBuilder.AppendLine(ErrorMessage);
                        isCellValid = false;
                    }
                }

                if (!isCellValid)
                {
                    continue;
                }

                Department department = new Department
                {
                    Name = departmentDto.Name
                };

                foreach (CellDto cellDto in departmentDto.Cells)
                {
                    Cell cell = new Cell
                    {
                        CellNumber = cellDto.CellNumber,
                        HasWindow = cellDto.HasWindow,
                        Department = department
                    };
                    
                    //cells.Add(cell);
                    department.Cells.Add(cell);
                }

                departments.Add(department);
                messageBuilder.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();

            //context.Cells.AddRange(cells);
            //context.SaveChanges();

            return messageBuilder.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            PrisonerDto[] deserializedPrisoners = JsonConvert.DeserializeObject<PrisonerDto[]>(jsonString);

            StringBuilder messageBuilder = new StringBuilder();
            List<Prisoner> prisoners = new List<Prisoner>();
            //List<Mail> mails = new List<Mail>();
            foreach (PrisonerDto prisonerDto in deserializedPrisoners)
            {
                if(!IsValid(prisonerDto))
                {
                    messageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                bool isMailValid = true;
                foreach (MailDto mailDto in prisonerDto.Mails)
                {
                    if(!IsValid(mailDto))
                    {
                        messageBuilder.AppendLine(ErrorMessage);
                        isMailValid = false;
                        break;
                    }
                }

                if(!isMailValid)
                {
                    continue;
                }

                bool isIncarcerationDateValid = DateTime.TryParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime incarcerationDate);
                if(!isIncarcerationDateValid)
                {
                    messageBuilder.AppendLine(ErrorMessage);
                    continue;
                }                

                DateTime? releaseDate = null;                
                if (prisonerDto.ReleaseDate != null)
                {
                    releaseDate = TryParseDate(prisonerDto.ReleaseDate, "dd/MM/yyyy");
                    if (releaseDate == null)
                    {
                        messageBuilder.AppendLine(ErrorMessage);
                        continue;
                    }
                }                                

                Prisoner prisoner = new Prisoner
                {
                    FullName = prisonerDto.FullName,
                    Nickname = prisonerDto.Nickname,
                    Age = prisonerDto.Age,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = releaseDate,
                    Bail = prisonerDto.Bail,
                    CellId = prisonerDto.CellId
                };

                foreach (MailDto mailDto in prisonerDto.Mails)
                {
                    Mail mail = new Mail
                    {
                        Description = mailDto.Description,
                        Sender = mailDto.Sender,
                        Address = mailDto.Address,
                        Prisoner = prisoner
                    };

                    //mails.Add(mail);
                    prisoner.Mails.Add(mail);
                }

                prisoners.Add(prisoner);
                messageBuilder.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            //context.Mails.AddRange(mails);
            //context.SaveChanges();

            return messageBuilder.ToString().TrimEnd();
        }

        //This code pass in Judge, but it is possible to throw an exception like this below: 
        //Unhandled Exception: System.InvalidOperationException: The property 'PrisonerId' on entity type 'OfficerPrisoner' has a temporary value. Either set a permanent value explicitly or ensure that the database is configured to generate values for this property.
        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(OfficerDto[]), new XmlRootAttribute("Officers"));
            OfficerDto[] deserializedOfficers = (OfficerDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder resultMessageBuilder = new StringBuilder();
            List<Officer> officers = new List<Officer>();
            List<OfficerPrisoner> officerPrisoners = new List<OfficerPrisoner>();
            foreach (OfficerDto officerDto in deserializedOfficers)
            {
                if (!IsValid(officerDto))
                {
                    resultMessageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                List<OfficerPrisoner> prisoners = new List<OfficerPrisoner>();
                bool isValidPrisonerId = true;
                foreach (PrisonerIdDto prisonerIdDto in officerDto.Prisoners)
                {
                    if(!IsValid(prisonerIdDto))
                    {
                        resultMessageBuilder.AppendLine(ErrorMessage);
                        isValidPrisonerId = false;
                        break;
                    }

                    OfficerPrisoner prisoner = new OfficerPrisoner
                    {
                        PrisonerId = prisonerIdDto.Id
                    };

                    prisoners.Add(prisoner);
                }

                if (!isValidPrisonerId)
                {
                    continue;
                }

                bool isPositionValid = Enum.TryParse(officerDto.Position, out Position position);                
                bool isWeaponValid = Enum.TryParse(officerDto.Weapon, out Weapon weapon);
                if (!isPositionValid  || !isWeaponValid)
                {
                    resultMessageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                //Department department = context.Departments.Find(officerDto.DepartmentId);
                //if (department == null)
                //{
                //    resultMessageBuilder.AppendLine(ErrorMessage);
                //    continue;
                //}

                Officer officer = new Officer
                {
                    FullName = officerDto.Name,
                    Salary = officerDto.Money,
                    Position = position,
                    Weapon = weapon,
                    //Department = department,
                    DepartmentId = officerDto.DepartmentId,
                    OfficerPrisoners = prisoners
                };


                //foreach (PrisonerIdDto prisonerIdDto in officerDto.Prisoners)
                //{
                //    Prisoner prisoner = context.Prisoners.Find(prisonerIdDto.Id);

                //    OfficerPrisoner officerPrisoner = new OfficerPrisoner
                //    {
                //        //PrisonerId = prisonerIdDto.Id,
                //        //OfficerId = officer.Id

                //        Officer = officer,
                //        Prisoner = prisoner
                //    };

                //    officer.OfficerPrisoners.Add(officerPrisoner);
                //    //officerPrisoners.Add(officerPrisoner);
                //}

                officers.Add(officer);
                resultMessageBuilder.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(officers);
            context.SaveChanges();

            //context.OfficersPrisoners.AddRange(officerPrisoners);
            //context.SaveChanges();

            return resultMessageBuilder.ToString().TrimEnd();           
        }

        private static DateTime? TryParseDate(string dateString, string formatString)
        {
            if (DateTime.TryParseExact(dateString, formatString, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            else
            {
                return null;
            }
        }

        //private static DateTime? TryParse(string dateString)
        //{
        //    if (DateTime.TryParse(dateString, out DateTime date))
        //    {
        //        return date;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


        //private static DateTime? TryParse(string dateString)
        //{
        //    return DateTime.TryParse(dateString, out DateTime date) ? date : (DateTime?)null;
        //}

        private static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}
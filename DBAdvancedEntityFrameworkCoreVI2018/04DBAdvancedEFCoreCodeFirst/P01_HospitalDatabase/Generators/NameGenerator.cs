using System;
//using System.IO;

namespace P01_HospitalDatabase.Generators
{
    public class NameGenerator
    {
        //private static string[] firstNames = File.ReadAllLines("<INSERT DIR HERE>");
        private static string[] firstNames = { "Petur", "Ivan", "Georgi", "Alexander", "Stefan", "Vladimir", "Svetoslav", "Kaloyan", "Mihail", "Stamat" };

        //private static string[] lastNames = File.ReadAllLines("<INSERT DIR HERE>");
        private static string[] lastNames = { "Ivanov", "Georgiev", "Stefanov", "Alexandrov", "Petrov", "Stamatkov", };        

        public static string FirstName() => GenerateName(firstNames);

        public static string LastName() => GenerateName(lastNames);

        private static string GenerateName(string[] names)
        {
            Random random = new Random();

            int index = random.Next(0, names.Length);

            string name = names[index];

            return name;
        }
    }
}
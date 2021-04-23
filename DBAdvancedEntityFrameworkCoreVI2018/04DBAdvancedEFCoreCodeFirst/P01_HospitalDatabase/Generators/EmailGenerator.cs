using System;
//using System.IO;

namespace P01_HospitalDatabase.Generators
{
    class EmailGenerator
    {
        private static Random random = new Random();

        //private static string[] domains = File.ReadAllLines("<INSERT DIR HERE>");
        private static string[] domains = { "mail.bg", "abv.bg", "gmail.com", "hotmail.com", "softuni.bg", "students.softuni.bg" };        

        internal static string NewEmail(string name)
        {
            string domain = domains[random.Next(domains.Length)];
            int number = random.Next(1, 2000);

            return $"{name.ToLower()}{number}@{domain}";
        }
    }
}
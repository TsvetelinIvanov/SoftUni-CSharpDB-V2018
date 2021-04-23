using System;
//using System.IO;

namespace P01_HospitalDatabase.Generators
{
    public class AddressGenerator
    {
        private static Random random = new Random();

        //private static string[] townNames = File.ReadAllLines("<INSERT DIR HERE>");
        private static string[] townNames =
        {
            "Suntown",
            "Nigthvill ",
            "Bridgetown",
            "Newport",
            "Tombstone",
            "Oldtown",
            "Darkvill",
            "Sincity",
            "Rootplace",
            "Mountvill",
        };

        //private static string[] streetNames = File.ReadAllLines("<INSERT DIR HERE>");
        private static string[] streetNames = 
        {
            "Somerset Drive",
            "Prospect Street",
            "Highland Avenue",
            "Windsor Court",
            "College Avenue",
            "Green Street",
            "Colonial Avenue",
            "Elm Avenue",
            "Durham Road",
            "Rose Street",
            "6th Street North",
            "Brandywine Drive",
            "Madison Avenue",
            "Route 10",
            "Main Street East",
        };        

        internal static string NewAddress()
        {
            string townName = townNames[random.Next(townNames.Length)];
            string streetName = streetNames[random.Next(streetNames.Length)];
            int number = random.Next(1, 100);

            return $"{townName}, {streetName} {number}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _05ChangeTownNamesCasing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string countryName = Console.ReadLine();

            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                int countryId = GetCountryId(countryName, connection);
                if (countryId == 0)
                {
                    Console.WriteLine("No town names were affected.");
                }
                else
                {
                    int affectedTownNamesCount = MakeTownNamesUpperCaseAndReturnNumberOfAffected(countryId, connection);
                    List<string> upperCaseTownNames = GetUpperCaseTownNames(countryId, connection);
                    
                    Console.WriteLine($"{affectedTownNamesCount} town names were affected.");
                    Console.WriteLine($"[{String.Join(", ", upperCaseTownNames)}]");
                }

                connection.Close();
            }
        }

        private static int GetCountryId(string countryName, SqlConnection connection)
        {
            string stringSqlSelectCountryId = "SELECT TOP(1) c.Id FROM Towns AS t JOIN Countries AS c ON c.Id = t.CountryCode WHERE c.[Name] = @countryName";
            using (SqlCommand command = new SqlCommand(stringSqlSelectCountryId, connection))
            {
                command.Parameters.AddWithValue("@countryName", countryName);
                if (command.ExecuteScalar() == null)
                {
                    return 0;
                }

                int countryId = (int)command.ExecuteScalar();

                return countryId;
            }
        }

        private static int MakeTownNamesUpperCaseAndReturnNumberOfAffected(int countryId, SqlConnection connection)
        {
            string stringSqlUpdateTownNamesToUpperCase = "UPDATE Towns SET [Name] = UPPER([Name]) WHERE CountryCode = @countryId";
            using (SqlCommand command = new SqlCommand(stringSqlUpdateTownNamesToUpperCase, connection))
            {
                command.Parameters.AddWithValue("@countryId", countryId);
                int affectedTownNamesCount = command.ExecuteNonQuery();

                return affectedTownNamesCount;
            }
        }

        private static List<string> GetUpperCaseTownNames(int countryId, SqlConnection connection)
        {
            List<string> upperCaseTownNames = new List<string>();
            string stringSqlSelectUpperCaseTownNames = "SELECT [Name] FROM  Towns WHERE CountryCode = @countryId";
            using (SqlCommand command = new SqlCommand(stringSqlSelectUpperCaseTownNames, connection))
            {
                command.Parameters.AddWithValue("@countryId", countryId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        upperCaseTownNames.Add((string)reader[0]);
                    }
                }
            }

            return upperCaseTownNames;
        }                
    }
}

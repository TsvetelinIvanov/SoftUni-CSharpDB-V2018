using System;
using System.Data.SqlClient;

namespace _03MinionNames
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                string villainName = GetVillainName(villainId, connection);
                if (villainName == null)
                {
                    Console.WriteLine($"No villain with ID {villainId} exist in the database.");
                }
                else
                {
                    Console.WriteLine($"Villain: {villainName}");
                    PrintMinionNames(villainId, connection);
                }

                connection.Close();
            }
        }

        private static string GetVillainName(int villainId, SqlConnection connection)
        {
            string stringSqlSelectVillainName = $"SELECT [Name] FROM Villains WHERE Id = @id";
            using (SqlCommand command = new SqlCommand(stringSqlSelectVillainName, connection))
            {
                command.Parameters.AddWithValue("@id", villainId);

                return (string)command.ExecuteScalar();
            }
        }

        private static void PrintMinionNames(int villainId, SqlConnection connection)
        {
            string stringSqlSelectMinionsNames = "SELECT [Name], Age FROM Minions AS m JOIN MinionsVillains AS mv ON mv.MinionId = m.Id WHERE mv.VillainId = @id ORDER BY [Name]";
            using (SqlCommand command = new SqlCommand(stringSqlSelectMinionsNames, connection))
            {
                command.Parameters.AddWithValue("@id", villainId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("(no minions)");
                    }
                    else
                    {
                        int rowNumber = 1;
                        while (reader.Read())
                        {
                            Console.WriteLine($"{rowNumber++}. {reader[0]} {reader[1]}");
                        }
                    }
                }
            }
        }        
    }
}
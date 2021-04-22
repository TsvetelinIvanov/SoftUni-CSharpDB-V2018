using System;
using System.Data.SqlClient;

namespace _02VillainNames
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                string stringSqlSelectVillainsAndCountOfTheirMinions = "SELECT v.[Name], COUNT(m.Id) AS [Number of Minions] FROM Minions AS m JOIN MinionsVillains AS mv ON mv.MinionId = m.Id JOIN Villains AS v ON v.Id = mv.VillainId GROUP BY v.[Name] HAVING COUNT(m.Id) > 3 ORDER BY[Number of Minions] DESC";
                using (SqlCommand command = new SqlCommand(stringSqlSelectVillainsAndCountOfTheirMinions, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} - {reader[1]}");
                        }
                    }
                }

                connection.Close();
            }
        }
    }
}
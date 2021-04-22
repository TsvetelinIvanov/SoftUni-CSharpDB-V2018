using System;
using System.Data.SqlClient;

namespace _09IncreaseAgeStoredProcedure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int minionId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                string stringSqlExecuteProcedure_usp_GetOlder = "EXECUTE usp_GetOlder @minionId";
                using (SqlCommand command = new SqlCommand(stringSqlExecuteProcedure_usp_GetOlder, connection))
                {
                    command.Parameters.AddWithValue("@minionId", minionId);
                    command.ExecuteNonQuery();
                }

                string stringSqlSelectMinionNameAndAge = "SELECT [Name], Age FROM Minions WHERE Id = @minionId";
                using (SqlCommand command = new SqlCommand(stringSqlSelectMinionNameAndAge, connection))
                {
                    command.Parameters.AddWithValue("@minionId", minionId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} - {reader[1]} years old");
                        }
                    }
                }

                connection.Close();
            }
        }
    }
}
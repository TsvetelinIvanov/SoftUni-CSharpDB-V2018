using _09IncreaseAgeStoredProcedure;
using System.Data.SqlClient;

namespace _09_Create_usp_GetOlderFor09IncreseAgeStoredProcedure
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                string stringSqlCreateStoredProcedure_usp_GetOlder = "CREATE PROCEDURE usp_GetOlder @minionId INT AS BEGIN UPDATE Minions SET Age += 1 WHERE Id = @minionId END";
                using (SqlCommand command = new SqlCommand(stringSqlCreateStoredProcedure_usp_GetOlder, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
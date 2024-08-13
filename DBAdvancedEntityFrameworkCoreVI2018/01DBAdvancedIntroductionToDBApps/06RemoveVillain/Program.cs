using System;
using System.Data.SqlClient;

namespace _06RemoveVillain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int inputVillainId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                int villainId = GetVillainId(inputVillainId, connection, transaction);
                if (villainId == 0)
                {
                    Console.WriteLine("No such villain was found.");
                }
                else
                {
                    try
                    {
                        int releasedMinionsCount = ReleaseMinions(villainId, connection, transaction);
                        string villainName = GetVillainName(villainId, connection, transaction);
                        DeleteVillain(villainId, connection, transaction);
                        
                        Console.WriteLine($"{villainName} was deleted.");
                        Console.WriteLine($"{releasedMinionsCount} minions were released.");
                    }
                    catch(SqlException e)
                    {
                        Console.WriteLine(e.Message);
                        transaction.Rollback();
                    }
                }

                connection.Close();
            }
        }

        private static int GetVillainId(int inputVillainId, SqlConnection connection, SqlTransaction transaction)
        {
            string stringSqlSelectVillainId = "SELECT Id FROM Villains WHERE Id = @inputVillainId";
            using (SqlCommand command = new SqlCommand(stringSqlSelectVillainId, connection, transaction))
            {
                command.Parameters.AddWithValue("@inputVillainId", inputVillainId);
                if (command.ExecuteScalar() == null)
                {
                    return 0;
                }

                int villainId = (int)command.ExecuteScalar();

                return villainId;
            }
        }

        private static int ReleaseMinions(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            string stringSqlDeleteFromMinionsVillains = "DELETE FROM MinionsVillains WHERE VillainId = @villainId";
            using (SqlCommand command = new SqlCommand(stringSqlDeleteFromMinionsVillains, connection, transaction))
            {
                command.Parameters.AddWithValue("@villainId", villainId);
                int releasedMinionsCount = command.ExecuteNonQuery();

                return releasedMinionsCount;
            }
        }

        private static string GetVillainName(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            string stringSqlSelectVillainName = "SELECT [Name] FROM Villains WHERE Id = @villainId";
            using (SqlCommand command = new SqlCommand(stringSqlSelectVillainName, connection, transaction))
            {
                command.Parameters.AddWithValue("@villainId", villainId);
                string villainName = (string)command.ExecuteScalar();

                return villainName;
            }
        }

        private static void DeleteVillain(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            string stringSqlDeleteVillain = "DELETE FROM Villains WHERE Id = @villainId";
            using (SqlCommand command = new SqlCommand(stringSqlDeleteVillain, connection, transaction))
            {
                command.Parameters.AddWithValue("@villainId", villainId);
                command.ExecuteNonQuery();
            }
        }                       
    }
}

using System;
using System.Data.SqlClient;

namespace _04AddMinion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] minionsInfo = Console.ReadLine().Split();
            string[] villainInfo = Console.ReadLine().Split();

            string minionName = minionsInfo[1];
            int minionAge = int.Parse(minionsInfo[2]);
            string minionTownName = minionsInfo[3];

            string villainName = villainInfo[1];

            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                int townId = GetTownId(minionTownName, connection);
                int villainId = GetVillainId(villainName, connection);
                int minionId = InsertMinionAndGetId(minionName, minionAge, townId, connection);
                AssignMinionToVillain(villainId, minionId, connection);
                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");

                connection.Close();
            }
        }

        private static int GetTownId(string minionTownName, SqlConnection connection)
        {
            string stringSqlSelectTownId = "SELECT Id FROM Towns WHERE [Name] = @minionTownName";
            using (SqlCommand command = new SqlCommand(stringSqlSelectTownId, connection))
            {
                command.Parameters.AddWithValue("@minionTownName", minionTownName);

                if (command.ExecuteScalar() == null)
                {
                    InsertIntoTowns(minionTownName, connection);
                    Console.WriteLine($"Town {minionTownName} was added to the database.");
                }

                int townId = (int)command.ExecuteScalar();

                return townId;
            }
        }

        private static void InsertIntoTowns(string minionTownName, SqlConnection connection)
        {
            string stringSqlInsertTown = "INSERT INTO Towns ([Name]) VALUES (@townName)";
            using (SqlCommand command = new SqlCommand(stringSqlInsertTown, connection))
            {
                command.Parameters.AddWithValue("@townName", minionTownName);
                command.ExecuteNonQuery();
            }
        }

        private static int GetVillainId(string villainName, SqlConnection connection)
        {
            string stringSqlSelectVillainId = "SELECT Id FROM Villains WHERE [Name] = @villainName";
            using (SqlCommand command = new SqlCommand(stringSqlSelectVillainId, connection))
            {
                command.Parameters.AddWithValue("@villainName", villainName);

                if (command.ExecuteScalar() == null)
                {
                    InsertIntoVillaians(villainName, connection);
                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }

                int villainId = (int)command.ExecuteScalar();

                return villainId;
            }
        }

        private static void InsertIntoVillaians(string villainName, SqlConnection connection)
        {
            string stringSqlInsertVillain = "INSERT INTO Villains ([Name]) VALUES (@villainName)";
            using (SqlCommand command = new SqlCommand(stringSqlInsertVillain, connection))
            {
                command.Parameters.AddWithValue("@villainName", villainName);
                command.ExecuteNonQuery();
            }
        }

        private static int InsertMinionAndGetId(string minionName, int minionAge, int townId, SqlConnection connection)
        {
            string stringSqlInsertMinion = "INSERT INTO Minions ([Name], Age, TownId) VALUES (@minionName, @minionAge, @townId)";
            using (SqlCommand command = new SqlCommand(stringSqlInsertMinion, connection))
            {
                command.Parameters.AddWithValue("@minionName", minionName);
                command.Parameters.AddWithValue("@minionAge", minionAge);
                command.Parameters.AddWithValue("@townId", townId);

                command.ExecuteNonQuery();
            }

            string stringSqlSelectMinionId = "SELECT Id FROM Minions WHERE [Name] = @minionName";
            using (SqlCommand command = new SqlCommand(stringSqlSelectMinionId, connection))
            {
                command.Parameters.AddWithValue("@minionName", minionName);
                int minionId = (int)command.ExecuteScalar();

                return minionId;
            }
        }

        private static void AssignMinionToVillain(int villainId, int minionId, SqlConnection connection)
        {
            string stringSqlInsertIntoMinionsVillains = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";
            using (SqlCommand command = new SqlCommand(stringSqlInsertIntoMinionsVillains, connection))
            {
                command.Parameters.AddWithValue("@minionId", minionId);
                command.Parameters.AddWithValue("@villainId", villainId);
                command.ExecuteNonQuery();
            }
        }                        
    }
}

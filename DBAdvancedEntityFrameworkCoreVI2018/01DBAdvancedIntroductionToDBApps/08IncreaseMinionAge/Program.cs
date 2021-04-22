using System;
using System.Data.SqlClient;
using System.Linq;

namespace _08IncreaseMinionAge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] minionIds = Console.ReadLine().Split().Select(int.Parse).ToArray();
            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                for (int i = 0; i < minionIds.Length; i++)
                {
                    int minionId = minionIds[i];
                    IncreaseAge(minionId, connection);
                    MakeNameTitleCase(minionId, connection);
                }

                PrintMinionNamesAndAges(connection);

                connection.Close();
            }
        }

        private static void IncreaseAge(int minionId, SqlConnection connection)
        {
            string stringSqlUpdateMinionsAge = "UPDATE Minions SET Age += 1 WHERE Id = @minionId";
            using (SqlCommand command = new SqlCommand(stringSqlUpdateMinionsAge, connection))
            {
                command.Parameters.AddWithValue("@minionId", minionId);
                command.ExecuteNonQuery();
            }
        }

        private static void MakeNameTitleCase(int minionId, SqlConnection connection)
        {
            string minionName = string.Empty;
            string stringSqlSelectMinionName = "SELECT [Name] FROM Minions WHERE Id = @minionId";
            using (SqlCommand command = new SqlCommand(stringSqlSelectMinionName, connection))
            {
                command.Parameters.AddWithValue("@minionId", minionId);
                minionName = (string)command.ExecuteScalar();
            }

            string[] separatedMinionName = minionName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < separatedMinionName.Length; i++)
            {
                separatedMinionName[i] = char.ToUpper(separatedMinionName[i][0]) + separatedMinionName[i].Substring(1);
            }

            minionName = string.Join(" ", separatedMinionName);
            string stringSqlUpdateNameTitleCase = "UPDATE Minions SET [Name] = @minionName WHERE Id = @minionId";
            using (SqlCommand command = new SqlCommand(stringSqlUpdateNameTitleCase, connection))
            {
                command.Parameters.AddWithValue("@minionName", minionName);
                command.Parameters.AddWithValue("@minionId", minionId);
                command.ExecuteNonQuery();
            }
        }

        private static void PrintMinionNamesAndAges(SqlConnection connection)
        {
            string stringSqlSelectMinionNamesAndAges = "SELECT [Name], Age FROM Minions";
            using (SqlCommand command = new SqlCommand(stringSqlSelectMinionNamesAndAges, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]} {reader[1]}");
                    }
                }
            }
        }                
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _07PrintAllMinionNames
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> minionNames = GetMinionNames();
            for (int i = 0; i < minionNames.Count / 2; i++)
            {
                Console.WriteLine(minionNames[i]);
                Console.WriteLine(minionNames[minionNames.Count - 1 - i]);
            }

            if (minionNames.Count % 2 != 0)
            {
                Console.WriteLine(minionNames[minionNames.Count / 2]);
            }
        }

        private static List<string> GetMinionNames()
        {
            List<string> minionNames = new List<string>();
            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                string stringSqlSelectNamesFromMinions = "SELECT [Name] FROM Minions";
                using (SqlCommand command = new SqlCommand(stringSqlSelectNamesFromMinions, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            minionNames.Add((string)reader[0]);
                        }
                    }
                }

                connection.Close();

                return minionNames;
            }
        }
    }
}
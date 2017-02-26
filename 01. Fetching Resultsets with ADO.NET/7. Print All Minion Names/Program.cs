using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7.Print_All_Minion_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"Server=.; " +
                                                   "Database=MinionsDB; " +
                                                   "Integrated Security=true");
            connection.Open();

            List<string> MinionNames = new List<string>();

            string NamesQuery = "select Name from Minions";
            SqlCommand GetNames = new SqlCommand(NamesQuery, connection);
            SqlDataReader reader = GetNames.ExecuteReader();
            while (reader.Read())
            {
                MinionNames.Add((string)reader["Name"]);
            }
            reader.Close();

            // Console.WriteLine(String.Join(", ", MinionNames));
            
            List<string> Result = new List<string>();
            for (int i = 0; i < MinionNames.Count/2 + 1; i++)
            {
                Result.Add(MinionNames[i]);
                Result.Add(MinionNames[(MinionNames.Count - 1) - i]);
            }
            for (int i = 0; i < Result.Count - 1; i++)
            {
                Console.WriteLine(Result[i]);
            }
        }
    }
}

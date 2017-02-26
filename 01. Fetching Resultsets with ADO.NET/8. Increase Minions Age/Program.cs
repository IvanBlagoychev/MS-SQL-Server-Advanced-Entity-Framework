using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8.Increase_Minions_Age
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"Server=.; " +
                                                   "Database=MinionsDB; " +
                                                   "Integrated Security=true");
            connection.Open();

            Console.Write("Id's: ");
            int[] Ids = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            foreach (var num in Ids)
            {
                string query = @"update Minions 
                                            set Age = Age + 1, Name = UPPER(LEFT(Name, 1)) + LOWER(RIGHT(Name, LEN(Name) - 1))
                                            where Id = @num";
                SqlCommand update = new SqlCommand(query, connection);
                update.Parameters.AddWithValue("@num", num);
                update.ExecuteNonQuery();
            }

            foreach (var num in Ids)
            {
                string OutputQuery = "select * from Minions where Id = @num";
                SqlCommand Output = new SqlCommand(OutputQuery, connection);
                Output.Parameters.AddWithValue("@num", num);
                SqlDataReader reader = Output.ExecuteReader();
                while (reader.Read())
                {
                    string Name = (string)reader["Name"];
                    int Age = (int)reader["Age"];
                    Console.WriteLine($"{Name} {Age}");
                }
                reader.Close();
            }

        }
    }
}

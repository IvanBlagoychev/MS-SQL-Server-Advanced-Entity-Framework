using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9.Increase_Age_Stored_Procedure
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"Server=.; " +
                                                   "Database=MinionsDB; " +
                                                   "Integrated Security=true");
            connection.Open();

            Console.Write("Enter Minion Id: ");
            int Id = int.Parse(Console.ReadLine());

            string query = "exec usp_Getolder @id";
            SqlCommand update = new SqlCommand(query, connection);
            update.Parameters.AddWithValue("@id", Id);
            update.ExecuteNonQuery();

            string GetDataQuery = "select * from Minions where Id = @id";
            SqlCommand GetData = new SqlCommand(GetDataQuery, connection);
            GetData.Parameters.AddWithValue("@id", Id);
            SqlDataReader reader = GetData.ExecuteReader();

            while(reader.Read())
            {
                string Name = (string)reader["Name"];
                int Age = (int)reader["Age"];
                Console.WriteLine($"{Name} {Age}");
            }

        }
    }
}

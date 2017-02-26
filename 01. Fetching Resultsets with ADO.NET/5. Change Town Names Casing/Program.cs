using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5.Change_Town_Names_Casing
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"Server=.; " +
                                                   "Database=MinionsDB; " +
                                                   "Integrated Security=true");
            connection.Open();

            string Country = Console.ReadLine();
            string TownsNames = "select TownName from Towns where CountryName = @Country";
            List<string> Towns = new List<string>();
            SqlCommand findTownsNames = new SqlCommand(TownsNames, connection);
            findTownsNames.Parameters.AddWithValue("@Country", Country);
            SqlDataReader reader = findTownsNames.ExecuteReader();

            while(reader.Read())
            {
                Towns.Add(((string)  reader["TownName"]).ToUpper());
            }
            reader.Close();

            string ChangeTownsCasingQuery = "update Towns set TownName = upper(TownName) where CountryName = @countryName";
            SqlCommand ChangeCasing = new SqlCommand(ChangeTownsCasingQuery, connection);
            ChangeCasing.Parameters.AddWithValue("@countryName", Country);
            ChangeCasing.ExecuteNonQuery();
            Console.WriteLine($"{Towns.Count} town names were affected.");
            Console.WriteLine("[" + String.Join(", ", Towns) + "]");
        }
    }
}

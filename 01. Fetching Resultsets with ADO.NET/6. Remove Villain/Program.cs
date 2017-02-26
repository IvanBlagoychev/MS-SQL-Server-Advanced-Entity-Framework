using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.Remove_Villain
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"Server=.; " +
                                                   "Database=MinionsDB; " +
                                                   "Integrated Security=true");
            connection.Open();

            Console.Write("Id: ");
            int VillainId = int.Parse(Console.ReadLine());

            string ChekIdQuery = "select * from Villains where Id = @VillainId";
            SqlCommand chekId = new SqlCommand(ChekIdQuery, connection);
            chekId.Parameters.AddWithValue("@VillainId", VillainId);
            SqlDataReader reader = chekId.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();

                string countMinionsQuery = @"select count(MinionId) from VillainsMinions 
                                            where VillainId = @VillainId
                                            group by VillainId ";

                SqlCommand getCount = new SqlCommand(countMinionsQuery, connection);
                getCount.Parameters.AddWithValue("@VillainId", VillainId);
                int MinionsCount = (int)getCount.ExecuteScalar();
                reader.Close();

                if (MinionsCount > 0)
                {
                    string deleteVillainRelations = "delete from VillainsMinions where VillainId = @VillainId";
                    SqlCommand DeleteVillainRelation = new SqlCommand(deleteVillainRelations, connection);
                    DeleteVillainRelation.Parameters.AddWithValue("@VillainId", VillainId);
                    DeleteVillainRelation.ExecuteNonQuery();

                    string getVillainNameQuery = "select Name from Villains where Id = @VillainId";
                    SqlCommand GetVillainName = new SqlCommand(getVillainNameQuery, connection);
                    GetVillainName.Parameters.AddWithValue("@VillainId", VillainId);
                    string Name = (string)GetVillainName.ExecuteScalar();
                    reader.Close();

                    string deleteVillainQuery = "delete from Villains where Id = @VillainId";
                    SqlCommand DeleteVillain = new SqlCommand(deleteVillainQuery, connection);
                    DeleteVillain.Parameters.AddWithValue("@VillainId", VillainId);
                    DeleteVillain.ExecuteNonQuery();

                    Console.WriteLine($"{Name} was deleted.");
                    Console.WriteLine($"{MinionsCount} minions released.");
                }
            }
            else
            {
                reader.Close();
                Console.WriteLine("No such villain was found");
            }
        }
    }
}

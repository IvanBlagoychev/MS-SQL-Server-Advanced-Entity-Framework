using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Add_Minion
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlConnection connection = new SqlConnection(@"Server=.; " +
                                                   "Database=MinionsDB; " +
                                                   "Integrated Security=true");
            connection.Open();

            Console.Write("Minion: ");
            string[] minionData = Console.ReadLine().Split(' ').ToArray();
            Console.Write("Villain: ");
            string VillainName = Console.ReadLine();
            string MinionName = minionData[0];
            string MinionAge = minionData[1];
            string MinionTown = minionData[2];
           
            string TownQuery = "select Id from Towns where TownName = @TownName";
            SqlCommand findTown = new SqlCommand(TownQuery, connection);
            findTown.Parameters.AddWithValue("@TownName", MinionTown);
            SqlDataReader reader = findTown.ExecuteReader();

            if (!reader.HasRows)
            {
                //add town to db
                reader.Close();
                string AddTownQuery = "insert into Towns (TownName, CountryName) values (@townName, NULL)";
                SqlCommand AddTown = new SqlCommand(AddTownQuery, connection);
                AddTown.Parameters.AddWithValue("@townName", MinionTown);
                AddTown.ExecuteNonQuery();
                Console.WriteLine($"Town {MinionTown} was added to the database");
            }
            reader.Close();

             int MinionTownId = (int) findTown.ExecuteScalar();
             reader.Close();

            
            string VillainQuery = "select Id from Villains where Name = @VillainName";
            SqlCommand findVillain = new SqlCommand(VillainQuery, connection);
            findVillain.Parameters.AddWithValue("@VillainName", VillainName);
            reader = findVillain.ExecuteReader();
            if (!reader.HasRows)
            {
                // add Villain
                reader.Close();
                string AddVillainQuery = "insert into Villains (name, EvilnessFactor) values(@VillainName, 'evil')";
                SqlCommand AddVillain = new SqlCommand(AddVillainQuery, connection);
                AddVillain.Parameters.AddWithValue("@VillainName", VillainName);
                AddVillain.ExecuteNonQuery();
                Console.WriteLine($"Villain {VillainName} was added to the database");
            }
            reader.Close();
         
            int VillainId = (int)findVillain.ExecuteScalar();
            reader.Close();

                  
            // add minion
            
            string addMinionQuery = "insert into Minions (Name, Age, TownId) values (@Name, @Age, @TownId)";
            SqlCommand addMinion = new SqlCommand(addMinionQuery, connection);
            addMinion.Parameters.AddWithValue("@Name", MinionName);
            addMinion.Parameters.AddWithValue("@Age", MinionAge);
            addMinion.Parameters.AddWithValue("@TownId", MinionTownId);
            addMinion.ExecuteNonQuery();


            string FindMinionIdQuery = "select Id from Minions where Name = @MinionName";
            SqlCommand findMinionId = new SqlCommand(FindMinionIdQuery, connection);
            findMinionId.Parameters.AddWithValue("@MinionName", MinionName);           
            int MinionId = (int)findMinionId.ExecuteScalar();      

            //add minion to villain

            string addMinionToVillainQuery = "insert into VillainsMinions (VillainId,MinionId) values (@VillainId, @MinionId)";
            SqlCommand addMinionToVillain = new SqlCommand(addMinionToVillainQuery, connection);
            addMinionToVillain.Parameters.AddWithValue("@VillainId", VillainId);
            addMinionToVillain.Parameters.AddWithValue("@MinionId", MinionId);
            addMinionToVillain.ExecuteNonQuery();
            Console.WriteLine($"Successfully added {MinionName} to be minion of {VillainName}");
        }
    }
}


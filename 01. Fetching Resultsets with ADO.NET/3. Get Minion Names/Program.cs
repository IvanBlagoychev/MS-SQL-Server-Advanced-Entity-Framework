using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Get_Minion_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Server=.; Database=MinionsDB; Integrated Security=true");
            Console.Write("Enter villainId: ");
            
            sqlConnection.Open();
            

            using (sqlConnection)
            {
                string villainId = Console.ReadLine();
                string queryVillain = $"select Name from Villains where Id = {villainId}"; 
                SqlCommand getVillain = new SqlCommand(queryVillain, sqlConnection);    
                SqlDataReader reader = getVillain.ExecuteReader();
                if(reader.Read())
                {
                    string villainName = (string)reader["Name"];
                    Console.WriteLine($"Villain: {villainName}");

                    
                    string queryMinions = $@"select v.Name as VillainName, m.Name as MinionName, m.Age from Minions as m
                                                join VillainsMinions as vm on m.Id = vm.MinionId
                                                join Villains as v on vm.VillainId = v.Id
                                                where v.Id = {villainId}";
                    reader.Close();
                    SqlCommand getMinions = new SqlCommand(queryMinions, sqlConnection);
                    SqlDataReader minionsReader = getMinions.ExecuteReader();
                    int i = 1;
                    if (minionsReader.Read())
                    {
                        while (minionsReader.Read())
                        {
                            string minionName = (string)minionsReader["MinionName"];
                            int age = (int)minionsReader["Age"];
                            Console.WriteLine($"{i}. {minionName} {age}");
                            i++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("(no minions)");
                    }
                    
                }
                else
                {
                    Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                }

               
            }

        }
    }
}

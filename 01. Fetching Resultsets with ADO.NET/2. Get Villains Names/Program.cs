using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Get_Villains_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Server=.; Database=MinionsDB; Integrated Security=true");
           
            string query = @"select v.Name, count(vm.MinionId) as MinionsCount from Villains as v 
                                            join VillainsMinions as vm on v.Id = vm.VillainId
                                            group by v.Name
                                            having count(vm.MinionId) > 3
                                            order by MinionsCount desc";

            SqlCommand getNames = new SqlCommand(query, sqlConnection);            

            sqlConnection.Open();
            using (sqlConnection)
            {
                SqlDataReader reader = getNames.ExecuteReader();
               while(reader.Read())
                {
                    string villainName =(string) reader["Name"];
                    int countSubordnates = (int) reader["MinionsCount"];
                    Console.WriteLine($"{villainName} {countSubordnates}");
                }
            }
        }
    }
}

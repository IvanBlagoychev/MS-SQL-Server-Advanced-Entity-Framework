

namespace AdoDemo
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            // Create DataBase MinionsDB

            //SqlConnection connection = new SqlConnection(@"Server=.; Database=master; Integrated Security=true");
            //connection.Open();

            //string CreateDbQuery = "create database MinionsDB";
            //SqlCommand CreateDb = new SqlCommand(CreateDbQuery, connection);
            //CreateDb.ExecuteNonQuery();
            //connection.Close();

            //Create Tables And Insert Data
            SqlConnection connect = new SqlConnection(@"Server=.; Database=MinionsDB; Integrated Security=true");
            connect.Open();

            string sqlCreateTables = File.ReadAllText(@"c: \users\hristina\documents\visual studio 2015\Projects\AdoDemo\AdoDemo\MinionsDB.sql");     
            SqlCommand createTables = new SqlCommand(sqlCreateTables, connect);
            createTables.ExecuteNonQuery();
        }
    }
}

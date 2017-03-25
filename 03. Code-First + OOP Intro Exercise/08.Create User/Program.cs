using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08.Create_User
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new CreateUser();
            context.Database.Initialize(true);

            AddUsers(context);
            context.SaveChanges();
            Console.WriteLine("Success");

        }

        private static void AddUsers(CreateUser context)
        {
            context.Users.Add(new User
            {
                Username = "IvanIvan",
                Password = "BloodyComplexP455w0rd##*",
                Email = "ivan@ivan.com",
                Age = 24,
                IsDeleted = false
            });

            User ivan = new User()
            {
                Username = "bai kolio",
                Password = "DaIma78*",
                Email = "VarnaMaika@softuni.bg",
                Age = 20,
                IsDeleted = true
            };

            context.Users.Add(ivan);

            context.Users.Add(new User()
            {
                Username = "Kolio Mamata",
                Password = "Kolio12$%",
                Email = "PeshoBurkalkata@ddz.ko",
                Age = 22,
                IsDeleted = true
            });
        }
    }
}

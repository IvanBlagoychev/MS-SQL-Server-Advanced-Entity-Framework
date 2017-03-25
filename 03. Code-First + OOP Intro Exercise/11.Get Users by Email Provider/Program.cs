using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11.Get_Users_by_Email_Provider
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new GetUsersContext();
            //context.Database.Initialize(true);

            Console.Write("Enter email provider: ");
            var provider = Console.ReadLine();

            var users = context.Users.Select(x => new { x.Username, x.Email }).Where(x => x.Email.Contains(provider)).ToList();

            foreach(var user in users)
            {
                Console.WriteLine($"{user.Username} {user.Email}");
            }
        }
    }
}

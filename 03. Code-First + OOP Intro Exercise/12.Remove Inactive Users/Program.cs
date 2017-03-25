using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12.Remove_Inactive_Users
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            DateTime date = new DateTime();
            //string input = "01 Apr 2020";
            date = DateTime.ParseExact(input, "dd MMM yyyy", null);

            using (var context = new UsersToDeleteContext())
            {
                // Match users
                IQueryable<User> users;
                int affUsers;

                // Mark as deleted
                MarkForDelete(date, context, out users, out affUsers);

                context.Users.RemoveRange(users);

                // Deletes users 
                context.SaveChanges();


                //Prints result
                Console.WriteLine(affUsers > 0 ? $"{affUsers} users have been deleted" : "No users have been deleted");
            }

        }

        private static void MarkForDelete(DateTime date, UsersToDeleteContext context, out IQueryable<User> users, out int affUsers)
        {
            users = context.Users.Where(x => x.LastTimeLoggedIn < date);
            // Get users count
            affUsers = context.Users.Count(x => x.LastTimeLoggedIn < date);
            foreach (var user in users)
            {
                user.IsDeleted = true;
            }
            // Mark as deleted
            context.SaveChanges();
        }
    }
    
}

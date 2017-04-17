using PhotoShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Client.Utilities
{
    class UserOptions
    {
        private static User loggedUser;
        private static bool isLoggedIn = false;
        public static void Login(string username, string password)
        {
            using (PhotoShareContext ctx = new PhotoShareContext())
            {
                var user = ctx.Users.FirstOrDefault(n => n.Username == username);
                if (user == null || user.Password != password)
                    throw new ArgumentException("Invalid username or password!");
                loggedUser = user;
                isLoggedIn = true;
            }
        }

        public static void Logout()
        {
            loggedUser = null;
            isLoggedIn = false;
        }

        public static bool isAuthenticated()
        {
            return isLoggedIn;
        }

        public static User GetCurrentUser()
        {
            return loggedUser;
        }
    }
}

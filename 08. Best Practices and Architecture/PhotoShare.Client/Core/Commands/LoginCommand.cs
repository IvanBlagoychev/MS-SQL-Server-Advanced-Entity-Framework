using PhotoShare.Client.Utilities;
using PhotoShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand
    {
        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];

            if(UserOptions.isAuthenticated())
            {
                throw new InvalidOperationException("You should logout first!");
            }
            try
            {
                UserOptions.Login(username, password);
            }
            catch (Exception)
            {

                throw new ArgumentException("Invalid username or password!");
            }

            return $"User {username} successfully logged in!";
        }
    }
}

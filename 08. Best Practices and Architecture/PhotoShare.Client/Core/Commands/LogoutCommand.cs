using PhotoShare.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Client.Core.Commands
{
    public class LogoutCommand
    {
        public string Execute()
        {   
            if (!UserOptions.isAuthenticated())
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }
            else
            {
                UserOptions.Logout();
                return "User successfully logged out!";
            }
        }
    }
}

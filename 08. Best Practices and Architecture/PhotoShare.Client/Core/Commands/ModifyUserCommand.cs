namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;
    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            string username = data[0];
            string property = data[1];
            string value = data[2];
            PhotoShareContext context = new PhotoShareContext();

            if (!UserOptions.isAuthenticated())
                throw new InvalidOperationException("Invalid Credentials!");
            if (username != UserOptions.GetCurrentUser().Username)
                throw new InvalidOperationException("Invalid Credentials!");

            if (context.Users.Where(f => f.Username == username).Select(u => u.Username).ToList().Contains(username))
            {
                User user = context.Users.Where(n => n.Username == username).FirstOrDefault();
                switch (property)
                {
                    case "Password":
                        if (value.Any(char.IsDigit) && value.Any(char.IsLower))
                            user.Password = value;
                        else
                            throw new ArgumentException("Invalid Password!");
                        break;

                    case "BornTown":
                        var newBornTown = context.Towns.FirstOrDefault(n => n.Name == value);
                        if (newBornTown != null)
                            user.BornTown = newBornTown;
                        else
                            throw new ArgumentException("Town " + newBornTown + " not found!");
                        break;

                    case "CurrentTown":
                        var newCurrentTown = context.Towns.FirstOrDefault(n => n.Name == value);
                        if (newCurrentTown != null)
                            user.CurrentTown = newCurrentTown;
                        else
                            throw new ArgumentException("Town " + newCurrentTown + " not found!");
                        break;
                    default: throw new ArgumentException("Invalid format!");
                }
            }
            else
                throw new ArgumentException("User " + username + " not found!");
            context.SaveChanges();
            return $"User {username}'s new {property} is {value}.";
        }
    }
}

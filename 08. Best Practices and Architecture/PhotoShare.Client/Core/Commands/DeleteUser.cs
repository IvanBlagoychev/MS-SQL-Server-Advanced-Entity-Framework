namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Utilities;
    public class DeleteUser
    {
        // DeleteUser <username>
        public string Execute(string[] data)
        {
            string username = data[0];

            if (!UserOptions.isAuthenticated())
                throw new InvalidOperationException("Invalid Credentials!");
            if (username != UserOptions.GetCurrentUser().Username)
                throw new InvalidOperationException("Invalid Credentials!");

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new InvalidOperationException($"User with {username} was not found!");
                }
                else
                {
                    if(user.IsDeleted == true)
                    {
                        throw new InvalidOperationException($"User {username} is already deleted!");
                    }
                    else
                    {
                        user.IsDeleted = true;
                        context.SaveChanges();
                        return $"User {username} was deleted from the database!";
                    }
                }
            }
        }
    }
}

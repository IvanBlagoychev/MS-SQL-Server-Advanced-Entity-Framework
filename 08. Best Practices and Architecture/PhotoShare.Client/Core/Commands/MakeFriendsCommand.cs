namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;
    public class MakeFriendsCommand
    {
        // MakeFriends <username1> <username2>
        public string Execute(string[] data)
        {
            string user1 = data[0];
            string user2 = data[1];
            using (PhotoShareContext ctx = new PhotoShareContext())
            {
                User firstUser = ctx.Users.FirstOrDefault(n => n.Username == user1);
                if (!UserOptions.isAuthenticated())
                    throw new InvalidOperationException("Invalid Credentials!");

                if (user1 != UserOptions.GetCurrentUser().Username)
                    throw new InvalidOperationException("Invalid Credentials!");

                User secondUser = ctx.Users.FirstOrDefault(n => n.Username == user2);

                if(firstUser == null)
                    throw new ArgumentException($"User {user1} not found!");

                if (secondUser == null)
                    throw new ArgumentException($"User {user2} not found!");

                if (firstUser.Friends.Contains(secondUser))
                    throw new InvalidOperationException($"{user2} is already a friend to {user1}");

                firstUser.Friends.Add(secondUser);
                ctx.SaveChanges();
            }
            return $"Friend {user2} added to {user1}";
        }
    }
}

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    public class PrintFriendsListCommand 
    {
        // PrintFriendsList <username>
        public string Execute(string[] data)
        {
            string user = data[0];
            string result = string.Empty;
            using (PhotoShareContext ctx = new PhotoShareContext())
            {
                if(!ctx.Users.Where(n=>n.Username == user).Select(f=>f.Username).ToList().Contains(user))
                {
                    throw new ArgumentException($"User {user} not found!");
                }
                
                var users = ctx.Users.Where(u => u.Username == user).ToList();
                result += "Friends: ";
                foreach (var u in users)
                {
                    if(!u.Friends.Any())
                    {
                        return "No friends for this user. :(";
                    }
                    foreach (var friend in u.Friends)
                    {
                        result += "\n\r" + "-" + friend.Username;
                    }
                }
            }
            return result;
        }
    }
}

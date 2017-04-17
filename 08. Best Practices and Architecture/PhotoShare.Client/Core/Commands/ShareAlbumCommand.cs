namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;
    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            int albumId = int.Parse(data[0]);
            string username = data[1];
            string permission = data[2];

            if (!UserOptions.isAuthenticated())
                throw new InvalidOperationException("Invalid Credentials!");

            

            using (PhotoShareContext ctx = new PhotoShareContext())
            {
                User user = ctx.Users.FirstOrDefault(n => n.Username == username);
                Album album = ctx.Albums.FirstOrDefault(n => n.Id == albumId);
                AlbumRole role = ctx.AlbumRoles.FirstOrDefault(u => u.User.Username == username);

                if (username != UserOptions.GetCurrentUser().Username && role.Role.ToString() != "Owner")
                    throw new InvalidOperationException("Invalid Credentials!");

                if (user == null)
                    throw new ArgumentException($"User {username} not found");
                if (album == null)
                    throw new ArgumentException($"Album {albumId} not found!");
                if (!Enum.IsDefined(typeof(Role), permission))
                    throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
                
                AlbumRole role = new AlbumRole()
                {
                    User = user,
                    Album = album,
                    Role = (Role)Enum.Parse(typeof(Role),permission)
                };
                ctx.AlbumRoles.Add(role);
                ctx.SaveChanges();
                return $"Username {username} added to album {album.Name} ({permission})";
            }
        }
    }
}

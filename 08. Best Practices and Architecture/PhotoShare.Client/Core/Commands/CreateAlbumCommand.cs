namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;
    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] input)
        {
            string user = input[0];
            string albumTitle = input[1];
            string color = input[2];
            List<string> tags = new List<string>();
            using (PhotoShareContext ctx = new PhotoShareContext())
            {
                if (!ctx.Users.Where(u => u.Username == user).Select(u => u.Username).ToList().Contains(user))
                {
                    throw new ArgumentException($"User {user} not found!");
                }
                if (ctx.Albums.Where(u => u.Name == albumTitle).Select(u => u.Name).ToList().Contains(albumTitle))
                {
                    throw new ArgumentException($"Album {albumTitle} exists!");
                }
                if (!Enum.IsDefined(typeof(Color), color))
                {
                    throw new ArgumentException($"Color {color} not found!");
                }
                tags = input.Skip(3).ToList();
                
                foreach (var t in tags)
                {
                    string validateTag = t.ValidateOrTransform();
                    if (!ctx.Tags.Where(n => n.Name == validateTag).Select(n => n.Name).ToList().Contains(validateTag))
                    {
                        throw new ArgumentException("Invalid tags!");
                    }
                }
                Album newAlbum = new Album()
                {
                    Name = albumTitle,
                    BackgroundColor = (Color)Enum.Parse(typeof(Color), color),
                    IsPublic = true
                };
                foreach (var t in tags)
                {
                    Tag tag = new Tag()
                    {
                        Name = t.ValidateOrTransform()
                    };
                    newAlbum.Tags.Add(tag);
                }
                ctx.Albums.Add(newAlbum);
                ctx.SaveChanges();
            }
            return $"Album {albumTitle} successfully created!";
        }
    }
}

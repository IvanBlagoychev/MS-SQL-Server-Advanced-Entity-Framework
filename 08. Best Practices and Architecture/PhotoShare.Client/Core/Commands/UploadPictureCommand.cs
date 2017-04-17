namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;
    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            string albumName = data[0];
            string pictureTitle = data[1];
            string pictrePath = data[2];

            if (!UserOptions.isAuthenticated())
                throw new InvalidOperationException("Invalid Credentials!");

            using (PhotoShareContext ctx = new PhotoShareContext())
            {
                Album album = ctx.Albums.FirstOrDefault(n => n.Name == albumName);
                if (album == null)
                    throw new ArgumentException($"Album {albumName} not found!");
                Picture p = new Picture()
                {
                    Title = pictureTitle,
                    Path = pictrePath
                };
                ctx.Pictures.Add(p);
                album.Pictures.Add(p);
                ctx.SaveChanges();
            }
            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}

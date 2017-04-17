namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;
    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public string Execute(string[] data)
        {
            string albumName = data[0];
            string tag = data[1].ValidateOrTransform();
            using (PhotoShareContext ctx = new PhotoShareContext())
            {
                Album album = ctx.Albums.FirstOrDefault(n => n.Name == albumName);
                Tag newtag = ctx.Tags.FirstOrDefault(n => n.Name == tag);                
                if (album == null || newtag == null)
                {
                    throw new ArgumentException("Either tag or album do not exist!");
                }               
                album.Tags.Add(newtag);
                ctx.SaveChanges();
                return $"Tag {tag} added to {albumName}!";
            }
                
        }
    }
}

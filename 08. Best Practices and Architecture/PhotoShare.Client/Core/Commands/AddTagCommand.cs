namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;

    public class AddTagCommand
    {
        // AddTag <tag>
        public string Execute(string[] data)
        {
            string tagName = data[0].ValidateOrTransform();

            if (!UserOptions.isAuthenticated())
                throw new InvalidOperationException("Invalid Credentials!");

            using (PhotoShareContext context = new PhotoShareContext())
            {
                Tag tag = context.Tags.FirstOrDefault(n => n.Name == tagName);
                if (tag != null)
                {
                    throw new ArgumentException($"Tag {tagName} allready exists!");
                }
                else
                {
                    context.Tags.Add(new Tag
                    {
                        Name = tagName
                    });
                }
                context.SaveChanges();
            }
            return "Tag " + tagName + " was added successfully!";
        }
    }
}

namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public string Execute(string[] data)
        {
            if (!UserOptions.isAuthenticated())
                throw new InvalidOperationException("Invalid Credentials!");

            string townName = data[0];
            string country = data[1];

            Town town = new Town
            {
                Name = townName,
                Country = country
            };

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (!context.Towns.Where(t => t.Name == townName).Select(f => f.Name).ToList().Contains(townName))
                {
                    context.Towns.Add(town);
                    context.SaveChanges();
                    return "Town " + townName + " was added successfully!";
                }
                else
                {
                    throw new ArgumentException("Town " + townName + " was already added!");
                }               
            }
        }
    }
}

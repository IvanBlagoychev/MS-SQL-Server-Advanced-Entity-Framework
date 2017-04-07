namespace Tasks5to10.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Tasks5to10.Data.PhotographerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Tasks5to10.Data.PhotographerContext";
        }

        protected override void Seed(Tasks5to10.Data.PhotographerContext context)
        {
            Photographer ivan = new Photographer("Ivan", "ivan@softuni.bg", new DateTime(1992, 11, 17), new DateTime(2016, 04, 09));

            Photographer pesho = new Photographer("peshoUnito", "pesho@SoftUniLegends.bg", new DateTime(2005, 01, 01), new DateTime(1992, 11, 17));

            Photographer gosho = new Photographer("gosho11", "gosho@mail.bg", new DateTime(2007, 07, 20), new DateTime(1997, 10, 03));

            Photographer dqdkov = new Photographer("IvanDqdkov", "dyakov@neshto.bg", new DateTime(1999, 04, 14), new DateTime(1994, 11, 08));

            Photographer sasho = new Photographer("SashoBurkalkata", "sashe@abv.bg", new DateTime(2003, 12, 30), new DateTime(1995, 12, 31));

            Photographer conko = new Photographer("ConkoPogachata", "cici@gmail.com", new DateTime(2009, 08, 13), new DateTime(2000, 06, 22));


            Album album1 = new Album("spring", "blue", true);
            Album album2 = new Album("summer", "red", true);
            Album album3 = new Album("winter", "green", false);
            Album album4 = new Album("authum", "yellow", true);
            Album album5 = new Album("rivver", "purple", false);
            Album album6 = new Album("forest", "brown", true);
            Album album7 = new Album("bridge", "black", false);
                                     

            Picture picture1 = new Picture("spring", "caption1", "path: D://");
            Picture picture2 = new Picture("summer", "caption2", "path: C://");
            Picture picture3 = new Picture("winter", "caption3", "path: E://");
            Picture picture4 = new Picture("authum", "caption4", "path: F://");
            Picture picture5 = new Picture("rivver", "caption5", "path: J://");
            Picture picture6 = new Picture("forest", "caption6", "path: D://");
            Picture picture7 = new Picture("bridge", "caption7", "path: C://");
            Picture picture8 = new Picture("cityview", "caption8", "path: M://");

            Tag tag1 = new Tag("#EntityFramework");
            Tag tag2 = new Tag("#MSSQL");
            Tag tag3 = new Tag("#SoftUniLegends");
            Tag tag4 = new Tag("#OOP");
            Tag tag5 = new Tag("#CSharp");


            pesho.Albums.Add(album2);
            sasho.Albums.Add(album3);
            gosho.Albums.Add(album1);
            dqdkov.Albums.Add(album6);
            conko.Albums.Add(album7);

            album1.Pictures.Add(picture5);
            album1.Pictures.Add(picture3);
            album1.Pictures.Add(picture8);
            album2.Pictures.Add(picture5);
            album2.Pictures.Add(picture1);
            album3.Pictures.Add(picture6);
            album3.Pictures.Add(picture7);
            album4.Pictures.Add(picture1);
            album5.Pictures.Add(picture2);
            album6.Pictures.Add(picture1);
            album6.Pictures.Add(picture4);

            album1.Tags.Add(tag5);
            album1.Tags.Add(tag4);
            album1.Tags.Add(tag3);
            album2.Tags.Add(tag2);
            album2.Tags.Add(tag1);
            album3.Tags.Add(tag2);
            album3.Tags.Add(tag3);
            album4.Tags.Add(tag4);
            album5.Tags.Add(tag5);
            album6.Tags.Add(tag4);
            album6.Tags.Add(tag3);

            context.Photographers.AddOrUpdate(x => x.Username, pesho);
            context.Photographers.AddOrUpdate(x => x.Username, gosho);
            context.Photographers.AddOrUpdate(x => x.Username, dqdkov);
            context.Photographers.AddOrUpdate(x => x.Username, sasho);
            context.Photographers.AddOrUpdate(x => x.Username, conko);

            context.Albums.AddOrUpdate(x => x.BackgroundColor, album1);
            context.Albums.AddOrUpdate(x => x.BackgroundColor, album2);
            context.Albums.AddOrUpdate(x => x.BackgroundColor, album3);
            context.Albums.AddOrUpdate(x => x.BackgroundColor, album4);
            context.Albums.AddOrUpdate(x => x.BackgroundColor, album5);
            context.Albums.AddOrUpdate(x => x.BackgroundColor, album6);
            context.Albums.AddOrUpdate(x => x.BackgroundColor, album7);

            context.Pictures.AddOrUpdate(x => x.Caption, picture1);
            context.Pictures.AddOrUpdate(x => x.Caption, picture2);
            context.Pictures.AddOrUpdate(x => x.Caption, picture3);
            context.Pictures.AddOrUpdate(x => x.Caption, picture4);
            context.Pictures.AddOrUpdate(x => x.Caption, picture5);
            context.Pictures.AddOrUpdate(x => x.Caption, picture6);
            context.Pictures.AddOrUpdate(x => x.Caption, picture7);
            context.Pictures.AddOrUpdate(x => x.Caption, picture8);

            context.Tags.AddOrUpdate(x => x.Content, tag1);
            context.Tags.AddOrUpdate(x => x.Content, tag2);
            context.Tags.AddOrUpdate(x => x.Content, tag3);
            context.Tags.AddOrUpdate(x => x.Content, tag4);
            context.Tags.AddOrUpdate(x => x.Content, tag5);
            //context.Photographers.Add(ivan);
            context.SaveChanges();
        }
    }
}

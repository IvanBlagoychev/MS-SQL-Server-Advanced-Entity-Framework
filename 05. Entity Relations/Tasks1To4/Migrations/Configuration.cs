namespace Tasks1To4.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Tasks1To4.Data.StudentContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Tasks1To4.Data.StudentContext";
        }

        protected override void Seed(Tasks1To4.Data.StudentContext context)
        {
            Student pesho = new Student()
            {
                Name = "Pesho",
                RegisterDate = new DateTime(2001, 06, 15)
            };
            Student ivan = new Student()
            {
                Name = "Ivan",
                RegisterDate = new DateTime(2003, 08, 23)
            };

            Student gosho = new Student()
            {
                Name = "Gosho",
                RegisterDate = new DateTime(1995, 09, 07)
            };

            Student vihar = new Student()
            {
                Name = "Vihar",
                RegisterDate = new DateTime(2009, 01, 10)
            };

            Course pyton = new Course()
            {
                Name = "Pyton Fundamentals",
                StartDate = new DateTime(1999, 08, 15),
                EndDate = new DateTime(1999, 11, 15),
                Price = 100.00m
            };
            Course CSharp = new Course()
            {
                Name = "C# OOP",
                StartDate = new DateTime(2001, 08, 15),
                EndDate = new DateTime(2001, 11, 15),
                Price = 120.00m
            };

            Course JavaScript = new Course()
            {
                Name = "JavaScript Apps",
                StartDate = new DateTime(2002, 08, 15),
                EndDate = new DateTime(2003, 11, 15),
                Price = 100.00m
            };
            Course ProgrmingBasics = new Course()
            {
                Name = "ProgramingBasics",
                StartDate = new DateTime(2002, 08, 15),
                EndDate = new DateTime(2003, 07, 12),
                Price = 50.00m
            };

            Resource r1 = new Resource()
            {
                Name = "C# intro",
                ResourceType = (Resource.ResType)Enum.Parse(typeof(Resource.ResType), "document"),
                URL = "www.atextbook.com"
            };

            Resource r2 = new Resource()
            {
                Name = ".NET intro",
                ResourceType = (Resource.ResType)Enum.Parse(typeof(Resource.ResType), "document"),
                URL = "www.dotnetbook.com"
            };
            Resource r3 = new Resource()
            {
                Name = "Judge",
                ResourceType = (Resource.ResType)Enum.Parse(typeof(Resource.ResType), "presentation"),
                URL = "www.judge.softuni.bg"
            };
            Resource r4 = new Resource()
            {
                Name = "JavaScript for dummies",
                ResourceType = (Resource.ResType)Enum.Parse(typeof(Resource.ResType), "video"),
                URL = "www.OnlineBooks.bg"
            };

            Homework h1 = new Homework()
            {
                Content = "for loops",
                ContentType = (Homework.HomeType)Enum.Parse(typeof(Homework.HomeType), "application"),
                SubmissionDate = new DateTime(2017, 06, 05)
            };
            Homework h2 = new Homework()
            {
                Content = "Arrays",
                ContentType = (Homework.HomeType)Enum.Parse(typeof(Homework.HomeType), "pdf"),
                SubmissionDate = new DateTime(2012, 07, 06)
            };
            Homework h3 = new Homework()
            {
                Content = "Strings",
                ContentType = (Homework.HomeType)Enum.Parse(typeof(Homework.HomeType), "zip"),
                SubmissionDate = new DateTime(2010, 08, 07)
            };

            License l1 = new License()
            {
                Name = "License1"
            };
            License l2 = new License()
            {
                Name = "License2"
            };
            License l3 = new License()
            {
                Name = "License3"
            };




            //adding Courses to Students:
            pesho.Courses.Add(pyton);
            vihar.Courses.Add(pyton);
            ivan.Courses.Add(CSharp);
            pesho.Courses.Add(JavaScript);
            vihar.Courses.Add(ProgrmingBasics);
            pesho.Courses.Add(ProgrmingBasics);
            gosho.Courses.Add(CSharp);
            gosho.Courses.Add(JavaScript);


            //Adding resources to courses:
            CSharp.Resources.Add(r2);
            ProgrmingBasics.Resources.Add(r1);
            JavaScript.Resources.Add(r4);
            pyton.Resources.Add(r3);

            //    //Adding homeworks to students
            pesho.Homeworks.Add(h1);
            vihar.Homeworks.Add(h2);
            ivan.Homeworks.Add(h3);
            gosho.Homeworks.Add(h1);
            pesho.Homeworks.Add(h2);
            vihar.Homeworks.Add(h3);
            ivan.Homeworks.Add(h1);


            //    //Add Licenses to Resourses
            r1.Licenses.Add(l1);
            r2.Licenses.Add(l1);
            r3.Licenses.Add(l1);
            r2.Licenses.Add(l2);
            r3.Licenses.Add(l3);


            //Add data to DB:
            context.Students.AddOrUpdate(x => x.Name, pesho);
            context.Students.AddOrUpdate(x => x.Name, ivan);
            context.Students.AddOrUpdate(x => x.Name, pesho);
            context.Students.AddOrUpdate(x => x.Name, gosho);

            context.Courses.AddOrUpdate(x => x.Name, pyton);
            context.Courses.AddOrUpdate(x => x.Name, CSharp);
            context.Courses.AddOrUpdate(x => x.Name, JavaScript);
            context.Courses.AddOrUpdate(x => x.Name, ProgrmingBasics);

            context.Resources.AddOrUpdate(x => x.Name, r1);
            context.Resources.AddOrUpdate(x => x.Name, r2);
            context.Resources.AddOrUpdate(x => x.Name, r3);
            context.Resources.AddOrUpdate(x => x.Name, r4);

            context.Homeworks.AddOrUpdate(x => x.Content, h1);
            context.Homeworks.AddOrUpdate(x => x.Content, h2);
            context.Homeworks.AddOrUpdate(x => x.Content, h3);



            //Add licenses to Db
            context.Licenses.AddOrUpdate(x => x.Name, l1);
            context.Licenses.AddOrUpdate(x => x.Name, l2);
            context.Licenses.AddOrUpdate(x => x.Name, l3);

            context.SaveChanges();
        }
    }
}

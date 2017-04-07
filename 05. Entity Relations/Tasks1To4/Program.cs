using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks1To4.Data;

namespace Tasks1To4
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new StudentContext();
            ctx.Database.Initialize(true);

            // PROBLEM 03 Tasks: 

            //Problem3FirstTask(ctx);
            //Problem3SecondTask(ctx);
            //Problem3ThirdTask(ctx);
            //Problem3FourthTask(ctx);
            //Problem3FifthTask(ctx);
        }

        private static void Problem3FifthTask(StudentContext ctx)
        {
            var students = ctx.Students
                            .OrderByDescending(e => e.Courses.Sum(t => t.Price))
                            .ThenByDescending(p => p.Courses.Count)
                            .ThenBy(e => e.Name)
                            .ToList();
            List<decimal> prices = new List<decimal>();
            foreach (var s in students)
            {
                Console.WriteLine($"Student name: {s.Name} \n\rNumber of courses: {s.Courses.Count}");
                Console.WriteLine($"Total price: {s.Courses.Sum(e => e.Price)} \n\rAverage price: {s.Courses.Average(r => r.Price):f2}");
                Console.WriteLine("------------------------------------------------------------");
            }
        }

        private static void Problem3FourthTask(StudentContext ctx)
        {
            DateTime givenDate = new DateTime(2003, 01, 01);
            var courses = ctx.Courses
                .Where(d => d.StartDate <= givenDate && d.EndDate >= givenDate)
                .OrderByDescending(c => c.Students.Count)
                .ThenByDescending(x => SqlFunctions.DateDiff("day", x.StartDate, x.EndDate))
                .ToList();
            int i = 1;
            foreach (var c in courses)
            {
                Console.WriteLine($"Course number: {i}");
                Console.WriteLine($"Course name: {c.Name}");
                Console.WriteLine($"Course StartDate: {c.StartDate}");
                Console.WriteLine($"Course EndDate: {c.EndDate}");
                Console.WriteLine($"Course Duration: {(c.EndDate - c.StartDate).TotalDays}");
                Console.WriteLine($"Number of students enrolled: {c.Students.Count}");
                Console.WriteLine("-------------------------------------------------");
                i++;
            }
        }

        private static void Problem3ThirdTask(StudentContext ctx)
        {
            var courses = ctx.Courses.Where(c => c.Resources.Count == 5).OrderByDescending(c => c.Resources.Count).ThenByDescending(s => s.StartDate).ToList();
            foreach (var c in courses)
            {
                Console.WriteLine($"{c.Name} {c.Resources.Count}");
            }
        }

        private static void Problem3SecondTask(StudentContext ctx)
        {
            var courses = ctx.Courses.OrderBy(c => c.StartDate).ThenByDescending(e => e.EndDate);
            foreach (var c in courses)
            {
                Console.WriteLine(c.Name + " " + c.Description);
                foreach (var r in c.Resources)
                {
                    Console.WriteLine(r.Name + " " + r.ResourceType + " " + r.URL);
                }
            }
        }

        private static void Problem3FirstTask(StudentContext ctx)
        {
            var students = ctx.Students.ToList();
            foreach (var s in students)
            {
                Console.WriteLine($"{s.Name}");
                foreach (var homework in s.Homeworks)
                {
                    Console.WriteLine($"{homework.Content} {homework.ContentType}");
                }
            }
        }
    }
}

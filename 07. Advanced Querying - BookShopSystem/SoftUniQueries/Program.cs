using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniQueries
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new SoftUniContext();
            //CallStoredProcedure(ctx);
            //EmployeesMaximumSalaries(ctx);
        }

        private static void EmployeesMaximumSalaries(SoftUniContext ctx)
        {
            foreach (var d in ctx.Departments)
            {
                decimal MaxSalary = ctx.Employees
                    .Where(e => e.Department.Name == d.Name)
                    .ToList()
                    .Max(x => x.Salary);
                if (MaxSalary < 30000 || MaxSalary > 70000)
                {
                    Console.WriteLine($"{d.Name} - {MaxSalary:f2}");
                }
            }
        }

        private static void CallStoredProcedure(SoftUniContext ctx)
        {
            string[] names = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            SqlParameter fName = new SqlParameter("@firstName", names[0]);
            SqlParameter lName = new SqlParameter("@lastName", names[1]);

            var projects = ctx.Database.SqlQuery<Project>("exec FindAllProjects @firstName, @lastName", fName, lName)
                .Select(s=> new
                {
                    s.Name, s.Description, s.StartDate
                }).ToList();

            foreach (var p in projects)
            {
                Console.WriteLine($"{p.Name} - {p.Description.Substring(0, 30)}..., {p.StartDate}");
            }
        }
    }
}

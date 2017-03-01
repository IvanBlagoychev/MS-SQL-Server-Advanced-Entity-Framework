namespace EntityFrameworkExercise
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Program
    {
        static void Main(string[] args)
        {
            SoftuniContext context = new SoftuniContext();

            // -- Problem 3 -- 
            //EmployeesFullInformation(context);

            // -- Problem 4 -- 
            //EmployeesWithSalaryOver50000(context);

            // -- Problem 5 -- 
            //EmployessFromSeattle(context);

            // -- Problem 6 -- 
            //AddingNewAddresessAndUpdatingEmployee(context);

            // -- Problem 7 -- 
            //FindEmployeesInPeriod(context);

            // -- Problem 8 -- 
            //AdressesByTownName(context);

            // -- Problem 9 -- 
            //EmployeeWithId147(context);

            // -- Problem 10 -- 
            //DepartmentsWithMoreThan5Employees(context);

            // -- Problem 11 -- 
            //FindLatest10Projects(context);

            // -- Problem 12 -- 
            //IncreaseSalaries(context);

            // -- Problem 13 -- 
            //FindEmployeesByFirstNameStartingWithSA(context);

            // -- Problem 14 -- 
            //FirstLetter();

            // -- Problem 15 -- 
            //DeleteProjectById(context);

            // -- Problem 16 -- 
            //RemoveTowns(context);

            // -- Problem 17 -- 
            //NativeSQLQuery(context);

        }

        private static void NativeSQLQuery(SoftuniContext context)
        {
            var timer = new Stopwatch();
            timer.Start();
            PrintNamesWithLINQ(context);
            timer.Stop();
            Console.WriteLine($"With LINQ - {timer.Elapsed}");
            timer.Restart();
            PrintNamesWithNativeSQL(context);
            timer.Stop();
            Console.WriteLine($"With native SQL - {timer.Elapsed}");
        }

        private static void PrintNamesWithNativeSQL(SoftuniContext context)
        {
            var projects = context.Projects.SqlQuery("SELECT * FROM Projects WHERE YEAR(StartDate) = 2002").ToList();
            foreach (var p in projects)
            {
                foreach (var emp in p.Employees)
                {
                    Console.WriteLine(emp.FirstName);
                }
            }
        }

        private static void PrintNamesWithLINQ(SoftuniContext context)
        {
            var projects = context.Projects.Where(s => s.StartDate.Year == 2002).ToList();
            foreach (Project p in projects)
            {
                foreach (var emp in p.Employees)
                {
                    Console.WriteLine(emp.FirstName);
                }
            }
        }

        private static void RemoveTowns(SoftuniContext context)
        {
            Console.Write("Enter a TownName: ");
            var TownName = Console.ReadLine();
            try
            {
                Town town = context.Towns.Where(p => p.Name == TownName).FirstOrDefault();
                var addr = context.Addresses.Where(a => a.Town.Name == TownName).ToList();
                int count = addr.Count;
                foreach (Address a in addr)
                {
                    var employees = context.Employees.Where(e => e.AddressID == a.AddressID).ToList();
                    foreach (Employee e in employees)
                    {
                        e.AddressID = null;
                    }
                    context.Addresses.Remove(a);
                }
                context.Towns.Remove(town);

                context.SaveChanges();

                if (addr.Count == 1)
                {
                    Console.WriteLine($"{count} address in {TownName} was deleted");
                }
                else
                {
                    Console.WriteLine($"{addr.Count} address in {TownName} were deleted");
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Something went worng");

            }
        }

        private static void DeleteProjectById(SoftuniContext context)
        {
            var project = context.Projects.Find(2);
            foreach (Employee e in project.Employees)
            {
                e.Projects.Remove(project);
            }
            context.Projects.Remove(project);
            context.SaveChanges();

            var projects = context.Projects.Take(10).ToList();
            foreach (Project p in projects)
            {
                Console.WriteLine(p.Name);
            }
        }

        private static void FirstLetter()
        {
            GringottsContext gcontext = new GringottsContext();
            var names = gcontext.WizzardDeposits
                .Where(d => d.DepositGroup == "Troll Chest")
                .Select(wn => wn.FirstName)
                .ToList()
                .Select(fn => fn[0])
                .Distinct()
                .OrderBy(c => c);

            foreach (var letter in names)
            {
                Console.WriteLine(letter);
            }
        }

        private static void FindEmployeesByFirstNameStartingWithSA(SoftuniContext context)
        {
            List<Employee> employees = context.Employees
                            .Where(e => e.FirstName.StartsWith("SA")).ToList();

            foreach (Employee e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f4})");
            }
        }

        private static void IncreaseSalaries(SoftuniContext context)
        {
            List<Employee> employees = context.Employees
                            .Where(p => p.Department.Name == "Engineering"
                            || p.Department.Name == "Tool Design"
                            || p.Department.Name == "Marketing"
                            || p.Department.Name == "Information Services").ToList();
            foreach (Employee e in employees)
            {
                e.Salary = e.Salary + (e.Salary * 12 / 100);
            }
            context.SaveChanges();
            foreach (Employee e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f6})");
            }
        }

        private static void FindLatest10Projects(SoftuniContext context)
        {
            List<Project> projects = context.Projects.OrderByDescending(p => p.StartDate).Take(10).OrderBy(p => p.Name).ToList();
            foreach (Project p in projects)
            {
                Console.WriteLine($"{p.Name} {p.Description} {p.StartDate} {p.EndDate}");
            }
        }

        private static void DepartmentsWithMoreThan5Employees(SoftuniContext context)
        {
            var departments = context.Departments
                .Where(c => c.Employees.Count > 5)
                .OrderBy(c => c.Employees.Count);
            foreach (Department d in departments)
            {
                Console.WriteLine($"{d.Name} {d.Manager.FirstName}");

                foreach (Employee e in d.Employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} {e.JobTitle}");
                }
            }
        }

        private static void EmployeeWithId147(SoftuniContext context)
        {
            Employee employee = context.Employees
                            .Where(e => e.EmployeeID == 147).FirstOrDefault();

            var projects = new List<Project>();
            foreach (Project p in employee.Projects)
            {
                projects.Add(p);
            }
            var proj = projects.OrderBy(p => p.Name);
            Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
            foreach (Project p in proj)
            {
                Console.WriteLine(p.Name);
            }
        }

        private static void AdressesByTownName(SoftuniContext context)
        {
            List<Address> adresses = context.Addresses.OrderByDescending(a => a.Employees.Count)
                            .ThenBy(t => t.Town.Name)
                            .Take(10)
                            .ToList();

            foreach (Address a in adresses)
            {
                Console.WriteLine($"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees");
            }
        }

        private static void FindEmployeesInPeriod(SoftuniContext context)
        {
            List<Employee> employees = context.Employees
                            .Where(p => p.Projects.Count(s => s.StartDate.Year >= 2001 && s.StartDate.Year <= 2003) > 0)
                            .Take(30).ToList();

            foreach (Employee e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} {e.Manager.FirstName}");
                foreach (Project p in e.Projects)
                {
                    Console.WriteLine($"--{p.Name} {p.StartDate:M'/'d'/'yyyy h:mm:ss tt} {p.EndDate:M'/'d'/'yyyy h:mm:ss tt}");
                }
            }
        }

        private static void AddingNewAddresessAndUpdatingEmployee(SoftuniContext context)
        {
            var adress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownID = 4
            };

            context.Addresses.Add(adress);
            Employee employee = context.Employees
                .Where(f => f.FirstName == "Nakov").FirstOrDefault();
            employee.Address = adress;
            context.SaveChanges();

            List<string> Adresses = context.Addresses
                .OrderByDescending(a => a.AddressID)
                .Take(10)
                .Select(a => a.AddressText).ToList();

            foreach (var addr in Adresses)
            {
                Console.WriteLine(addr);
            }
        }

        private static void EmployessFromSeattle(SoftuniContext context)
        {
            List<Employee> Employees = context.Employees
                .Where(d => d.Department.Name == "Research and Development")
                .OrderBy(f => f.Salary).ThenByDescending(e => e.FirstName).ToList();

            foreach (Employee e in Employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:f2}");
            }
        }

        private static void EmployeesWithSalaryOver50000(SoftuniContext context)
        {
            var employees = context.Employees.Where(s => s.Salary > 50000).Select(s => s.FirstName).ToList();
            foreach (var emp in employees)
            {
                Console.WriteLine(emp);
            }
        }

        private static void EmployeesFullInformation(SoftuniContext context)
        {
            List<Employee> employees = context.Employees.ToList();
            foreach (Employee e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f4}");
                
            }
        }
    }
}

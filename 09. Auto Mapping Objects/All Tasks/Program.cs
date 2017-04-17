using All_Tasks.DTO;
using All_Tasks.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace All_Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            //---------------Task 1-------------\\
            //SimpleMapping();

            //---------------Task2--------------\\
            //AdvancedMapping();


            //context.Database.Initialize(true);
            //IEnumerable<Employee> employees = CreateManagers();
            //SeedDatabase(employees);


            //--------------Task3---------------\\
            //Projection();
        }

        private static void Projection()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>()
                .ForMember(emp => emp.ManagerLastName, config => config.MapFrom(r => r.Manager.LastName));
                cfg.CreateMap<Employee, ManagerDto>()
                .ForMember(dto => dto.SubordinatesCount, configExpr => configExpr.MapFrom(e => e.Subordinates.Count));
            });

            EmployeesContext dbContext = new EmployeesContext();
            Employee kolio = new Employee()
            {
                FirstName = "Kolio",
                LastName = "Koliov",
                Salary = 420.00m,
                Adress = "Veliko Turnovo",
                Birthday = new DateTime(1989, 03, 04),
                IsOnHoliday = false,
                Subordinates = new List<Employee>(),
                Manager = new Employee() { FirstName = "manqk", LastName = "Ivanov" }
            };
            dbContext.Employees.Add(kolio);
            dbContext.SaveChanges();

            using (dbContext)
            {
                var employees = dbContext.Employees
                    .Where(a => a.Birthday.Value.Year < 1990)
                    .OrderByDescending(s => s.Salary)
                    .ProjectTo<EmployeeDto>();

                foreach (EmployeeDto e in employees)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private static void SeedDatabase(IEnumerable<Employee> employees)
        {

            EmployeesContext dbContext = new EmployeesContext();
            dbContext.Employees.AddRange(employees);
            dbContext.SaveChanges();
        }

        private static void AdvancedMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>();
                cfg.CreateMap<Employee, ManagerDto>()
                .ForMember(dto => dto.SubordinatesCount, configExpr => configExpr.MapFrom(e => e.Subordinates.Count));
            });

            IEnumerable<Employee> managers = CreateManagers();
            IEnumerable<ManagerDto> managerDtos = Mapper.Map<IEnumerable<Employee>, IEnumerable<ManagerDto>>(managers);
            foreach (ManagerDto managerDto in managerDtos)
            {
                Console.WriteLine(managerDto.ToString());
            }
        }
        private static IEnumerable<Employee> CreateManagers()
        {
            var managers = new List<Employee>();
            for (int i = 0; i < 3; i++)
            {
                var manager = new Employee()
                {
                    FirstName = "Pesho",
                    LastName = "Barkalkata",
                    Adress = "Sofia tintqva",
                    Birthday = new DateTime(1997, 11, 11),
                    Salary = 3000.00m,
                    IsOnHoliday = false,
                    Manager = new Employee() { FirstName = "Ivan", LastName = "Ivanov" }
                };

                var employee1 = new Employee()
                {
                    FirstName = "Pesho",
                    LastName = "Barkalkata",
                    Salary = 4000.00m,
                    Manager = manager
                };

                var employee2 = new Employee()
                {
                    FirstName = "Mitio",
                    LastName = "Pogachata",
                    Salary = 8000.00m,
                    Manager = manager
                };

                var employee3 = new Employee()
                {
                    FirstName = "Toto",
                    LastName = "Vrachanski",
                    Salary = 9000.00m,
                    Manager = manager
                };

                manager.Subordinates.Add(employee1);
                manager.Subordinates.Add(employee2);
                manager.Subordinates.Add(employee3);
                managers.Add(manager);
            }
            return managers;
        }
        private static void SimpleMapping()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeDto>());

            Employee e = new Employee()
            {
                FirstName = "Ivan",
                LastName = "IvanIvan",
                Salary = 5500.00m,
                Birthday = DateTime.Now,
                Adress = "Varna"
            };

            EmployeeDto dto = Mapper.Map<EmployeeDto>(e);
            Console.WriteLine(dto.Salary);
        }
    }
}

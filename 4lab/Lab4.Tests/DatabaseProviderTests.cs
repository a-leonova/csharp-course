using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using lab4;
namespace Lab4.Tests
{
    [TestFixture]
    public class DatabaseProviderTests
    {
        [Test]
        public void AddEmploye_GoodData_ReturnOk()
        {
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var dbProvider = new DatabaseProvider(options);
            dbProvider.EnsureDeleted();

            Employe employe1 = new Employe { FirstName = "Anton", MiddleName = "Antonovich", LastName = "Antonov" };
            dbProvider.AddEmploye(employe1);
            
            Assert.AreEqual(employe1.FirstName, (dbProvider.GetEmploye(employe1.Id)).FirstName);
            Assert.AreEqual(employe1.MiddleName, (dbProvider.GetEmploye(employe1.Id)).MiddleName);
            Assert.AreEqual(employe1.LastName, (dbProvider.GetEmploye(employe1.Id)).LastName);
        }

        [Test]
        public void AddProject_GoodData_ReturnOk()
        {
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var dbProvider = new DatabaseProvider(options);
            Employe employe1 = new Employe { FirstName = "Anton", MiddleName = "Antonovich", LastName = "Antonov" };
            dbProvider.AddEmploye(employe1);

            Project project = new Project{Name = "Ololo", Premium = 2500, SupplierEmploye = employe1};
            dbProvider.AddProject(project);
            Assert.AreEqual(project.Name, dbProvider.GetProject(project.Id).Name);
            Assert.AreEqual(project.Premium, dbProvider.GetProject(project.Id).Premium);
            Assert.AreEqual(project.SupplierEmploye.Id, dbProvider.GetProject(project.Id).SupplierEmploye.Id);
            Assert.AreEqual(project.Name, dbProvider.GetEmploye(employe1.Id).Projects.Find(e => e.Id == project.Id).Name);
        }

        [Test]
        public void ShowEmployeInfo()
        {
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var dbProvider = new DatabaseProvider(options);
            Employe employe1 = new Employe { FirstName = "Anton", MiddleName = "Antonovich", LastName = "Antonov" };
            Employe employe2 = new Employe { FirstName = "Boris", MiddleName = "Borisovich", LastName = "Boris" };
            dbProvider.AddEmploye(employe1);
            dbProvider.AddEmploye(employe2);

            Project project1 = new Project { Name = "Ololo", Premium = 2500, SupplierEmploye = employe1};
            Project project2 = new Project { Name = "Ololo1", Premium = 500, SupplierEmploye = employe1};
            Project project3 = new Project { Name = "Ololo2", Premium = 150, SupplierEmploye = employe2};
            dbProvider.AddProject(project1);
            dbProvider.AddProject(project2);
            dbProvider.AddProject(project3);
            List<Employe> employes = dbProvider.GetEmployesFromDB();
            foreach (Employe employe in employes)
            {
                TestContext.WriteLine($"{employe.Id}.{employe.FirstName} {employe.MiddleName} {employe.LastName}");
                TestContext.WriteLine("Projects: ");
                foreach (Project project in employe.Projects)
                {
                    TestContext.WriteLine(
                        $"{project.Id}.{project.Name}: {project.Premium} - {project.SupplierEmploye}");
                }
            }
        }

        [Test]
        public void EditEmploye()
        {
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var dbProvider = new DatabaseProvider(options);
            Employe employe1 = new Employe { FirstName = "Anton", MiddleName = "Antonovich", LastName = "Antonov" };
            dbProvider.AddEmploye(employe1);

            Employe employe2 = new Employe { FirstName = "Antonina", MiddleName = "Antonovna", LastName = "Antonova" };
            dbProvider.EditEmploye(employe2, employe1.Id);
            Assert.AreEqual(employe2.FirstName, (dbProvider.GetEmploye(employe1.Id)).FirstName);
            Assert.AreEqual(employe2.MiddleName, (dbProvider.GetEmploye(employe1.Id)).MiddleName);
            Assert.AreEqual(employe2.LastName, (dbProvider.GetEmploye(employe1.Id)).LastName);
        }

        [Test]
        public void EditProject()
        {
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var dbProvider = new DatabaseProvider(options);
            Employe employe1 = new Employe { FirstName = "Anton", MiddleName = "Antonovich", LastName = "Antonov" };
            Employe employe2 = new Employe { FirstName = "Boris", MiddleName = "Borisovich", LastName = "Borisov" };
            dbProvider.AddEmploye(employe1);

            Project project1 = new Project { Name = "Ololo", Premium = 2500, SupplierEmploye = employe1 };
            dbProvider.AddProject(project1);
            Project project2 = new Project() {Name = "Ololo2", Premium = 1000, SupplierEmploye = employe2};
            dbProvider.EditProject(project2, project1.Id);
            Assert.AreEqual(project2.Name, dbProvider.GetProject(project1.Id).Name);
            Assert.AreEqual(project2.Premium, dbProvider.GetProject(project1.Id).Premium);
            Assert.AreEqual(project2.SupplierEmploye.Id, dbProvider.GetProject(project1.Id).SupplierEmploye.Id);

        }

        [Test]
        public void RemoveEmploye()
        {
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var dbProvider = new DatabaseProvider(options);
            Employe employe1 = new Employe { FirstName = "Anton", MiddleName = "Antonovich", LastName = "Antonov" };
            dbProvider.AddEmploye(employe1);
            dbProvider.DeleteEmploye(employe1.Id);
            Assert.Throws(typeof(InvalidOperationException), 
                new TestDelegate(() => dbProvider.GetEmploye(employe1.Id)));
        }

        [Test]
        public void RemoveProject()
        {
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var dbProvider = new DatabaseProvider(options);
            Employe employe1 = new Employe { FirstName = "Anton", MiddleName = "Antonovich", LastName = "Antonov" };
            dbProvider.AddEmploye(employe1);

            var project = new Project{ Name = "Ololo", Premium = 2500, SupplierEmploye = employe1 };
            dbProvider.AddProject(project);
            dbProvider.DeleteProject(project.Id);
            Assert.Throws(typeof(InvalidOperationException),
                new TestDelegate(() => dbProvider.GetProject(project.Id)));
        }

        [Test]
        public void SortByPremium()
        {
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var dbProvider = new DatabaseProvider(options);

            Employe employe1 = new Employe { FirstName = "Anton", MiddleName = "Antonovich", LastName = "Antonov" };
            Employe employe2 = new Employe { FirstName = "Boris", MiddleName = "Borisovich", LastName = "Borisov" };
            Employe employe3 = new Employe { FirstName = "Valeria", MiddleName = "Valerevna", LastName = "Valerova" };

            Project project1 = new Project { Name = "Fix bug #245", Premium = 2000, SupplierEmploye = employe1 };
            Project project2 = new Project { Name = "Review Boris' code", Premium = 1000, SupplierEmploye = employe1 };
            Project project3 = new Project { Name = "Fix bug #246", Premium = 3000, SupplierEmploye = employe3 };
            Project project4 = new Project { Name = "Fix bug #247", Premium = 2500, SupplierEmploye = employe2 };
            Project project5 = new Project { Name = "Review Anton's code", Premium = 1000, SupplierEmploye = employe3 };

            dbProvider.AddEmploye(employe1);
            dbProvider.AddEmploye(employe2);
            dbProvider.AddEmploye(employe3);
            dbProvider.AddProject(project1);
            dbProvider.AddProject(project2);
            dbProvider.AddProject(project3);
            dbProvider.AddProject(project4);
            dbProvider.AddProject(project5);

            IQueryable<EmployeWithTotalPremium> employes = dbProvider.SortByPremium();
            foreach (EmployeWithTotalPremium employe in employes)
            {
                TestContext.WriteLine($"{employe.FirstName} {employe.MiddleName} {employe.LastName}");
                TestContext.WriteLine($"Total premium: {employe.TotalPremium}");
            }
        }
    }
}
﻿using System;
using System.Linq;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddInfo();
            //EditEmployeInfo(1);
            //EditProjectInfo(2,1);
            //DeleteProject(1);
            //DeleteEmploye(13);
            Sort();
        }

        static void AddInfo()
        {
            using (var dbProvider = new DatabaseProvider())
            {
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

                Console.WriteLine("Done!");
                Console.ReadLine();
            }
        }

        static void EditEmployeInfo(int id)
        {
            using (var dbProvider = new DatabaseProvider())
            {
                Employe employe2 = new Employe { FirstName = "Antonina", MiddleName = "Antonovna", LastName = "Antonova" };
                try
                {
                    dbProvider.EditEmploye(employe2, id);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine($"Exception while edit employe info: {e.Message}");
                }
            };
        }

        static void EditProjectInfo(int empId, int projId)
        {
            using (var dbProvider = new DatabaseProvider())
            {
                try
                {
                    var employe = dbProvider.GetEmploye(empId);
                    Project project2 = new Project() {Name = "Ololo2", Premium = 1000, SupplierEmploye = employe};
                    dbProvider.EditProject(project2, projId);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine($"Exception while edit project info: {e.Message}");
                }
            }
        }

        static void DeleteEmploye(int id)
        {
            using (var dbProvider = new DatabaseProvider())
            {
                try
                {
                    dbProvider.DeleteEmploye(id);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine($"Exception while delete employe: {e.Message}");
                }
            };
        }

        static void DeleteProject(int id)
        {
            using (var dbProvider = new DatabaseProvider())
            {
                try
                {
                    dbProvider.DeleteProject(id);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine($"Exception while delete project: {e.Message}");
                }
            };
        }

        static void Sort()
        {
            using (var dbProvider = new DatabaseProvider())
            {
                IQueryable<EmployeWithTotalPremium> employes = dbProvider.SortByPremium();
                foreach (EmployeWithTotalPremium employe in employes)
                {
                    Console.WriteLine($"{employe.FirstName} {employe.MiddleName} {employe.LastName}");
                    Console.WriteLine($"Total premium: {employe.TotalPremium}");
                }
            }
            Console.ReadLine();
        }
    }
}

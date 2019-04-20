using System;

namespace lab5
{
    class Program
    {
        static void Main(string[] args)
        {
           //AddInfo();
           using (DatabaseProvider dbDatabaseProvider = new DatabaseProvider())
           {
               var employes = dbDatabaseProvider.GetEmployesFromDB();
               foreach (Employe employe in employes)
               {
                    Console.WriteLine($"{employe.FirstName} {employe.MiddleName} {employe.LastName}");   
               }

               var projects = dbDatabaseProvider.GetProjectsFromDB();
               foreach (Project project in projects)
               {
                   Console.WriteLine($"{project.Name} {project.Premium} {project.Deadline} {project.SupplierEmploye.LastName}");
               }
           }
           Console.ReadLine();
        }

        static void AddInfo()
        {
            var dbProvider = new DatabaseProvider();
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

}

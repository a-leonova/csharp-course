using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace lab5
{
    public class DatabaseProvider : IDisposable
    {
        private ApplicationContext db;

        public DatabaseProvider()
        {
            db = new ApplicationContext();
        }

        public DatabaseProvider(DbContextOptions<ApplicationContext> options)
        {
            db = new ApplicationContext(options);
        }

        public void AddEmploye(Employe employe)
        {
            db.Employes.Add(employe);
            db.SaveChanges();
        }

        public void AddProject(Project project)
        {
            db.Projects.Add(project);
            db.SaveChanges();
        }

        public Project GetProject(int id)
        {
            var project = db.Projects.Single(p => p.Id == id);
            return project;
        }

        public Employe GetEmploye(int id)
        {
            var employe = db.Employes.Single(e => e.Id == id);
            return employe;
        }

        public void EditEmploye(Employe newEmployeInfo)
        {
            var employe = db.Employes.First();
            EditEmploye(newEmployeInfo, employe.Id);
        }

        public void EditEmploye(Employe newEmployeInfo, int id)
        {
            var employe = db.Employes.Single(e => e.Id == id);
            employe.FirstName = newEmployeInfo.FirstName;
            employe.MiddleName = newEmployeInfo.MiddleName;
            employe.LastName = newEmployeInfo.LastName;
            db.SaveChanges();
        }

        public void EditProject(Project newProjectInfo)
        {
            var project = db.Projects.First();
            EditProject(newProjectInfo, project.Id);
        }

        public void EditProject(Project newProjectInfo, int id)
        {
            var project = db.Projects.Single(p => p.Id == id);
            project.Name = newProjectInfo.Name;
            project.Premium = newProjectInfo.Premium;
            project.SupplierEmploye = newProjectInfo.SupplierEmploye;
            db.SaveChanges();
        }

        public List<Project> GetProjectsFromDB()
        {
            return db.Projects.ToList();
        }

        public List<Employe> GetEmployesFromDB()
        {
            return db.Employes.ToList();
        }

        public void DeleteEmploye(int id)
        {
            Employe employe = db.Employes.First(e => e.Id == id);
            db.Employes.Remove(employe);
            db.SaveChanges();
        }

        public void SaveDB()
        {
            db.SaveChanges();
        }

        public void DeleteProject(int id)
        {
            Project project = db.Projects.First(e => e.Id == id);
            db.Projects.Remove(project);
            db.SaveChanges();
        }

        public IQueryable<EmployeWithTotalPremium> SortByPremium()
        {
            var summed = db.Employes.Select(e => new EmployeWithTotalPremium
            {
                FirstName = e.FirstName, MiddleName = e.MiddleName, LastName = e.LastName,
                TotalPremium = e.Projects.Sum(p => p.Premium)
            });
            return summed.OrderBy(s => s.TotalPremium);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void EnsureDeleted()
        {
            db.Database.EnsureDeleted();
        }
    }
}

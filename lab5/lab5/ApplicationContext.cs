using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace lab5
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employe> Employes{ get; set; }
        public DbSet<Project> Projects{ get; set; }

        public ApplicationContext():base(new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Db5lab;Trusted_Connection=True;")
            .Options)
        {
       //     Database.EnsureCreated();
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }
    }
}

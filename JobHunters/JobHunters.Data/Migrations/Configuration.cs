namespace JobHunters.Data.Migrations
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using JobHunters.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //This method will be called after migrating to the latest version.

            //You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //to avoid creating duplicate seed data. E.g.
            if (!context.Cities.Any())
            {
                context.Cities.AddOrUpdate(
                                           new City { Name = "Sofia" },
                                           new City { Name = "Plovdiv" },
                                           new City { Name = "Burgas" },
                                           new City { Name = "Varna" },
                                           new City { Name = "Ruse" });
            }
            if (!context.Categories.Any())
            {
                context.Categories.AddOrUpdate(
                                               new Category { Name = "ASP.NET WebForms" },
                                               new Category { Name = "ASP.NET MVC" },
                                               new Category { Name = "Java" },
                                               new Category { Name = "C#" },
                                               new Category { Name = "Front-End" },
                                               new Category { Name = "NodeJS" });
            }
        }
    }
}
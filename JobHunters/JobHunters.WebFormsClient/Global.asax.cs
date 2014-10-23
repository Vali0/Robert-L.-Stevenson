using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.AspNet.Identity.Owin;
using JobHunters.Models;

namespace JobHunters.WebFormsClient
{
    using JobHunters.Data;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.IO;
    using System.Data.Entity.Validation;

    public class Global : HttpApplication
    {
        private readonly ApplicationDbContext context = new ApplicationDbContext();
        private const string Admin = "Admin";
        private const string Employer = "Employer";
        private const int EmployersCount = 5;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            AddUserRoles();
            SeedUsers();
            SeedJobPosts();
        }

        private void AddUserRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(Admin))
            {
                var roleResult = roleManager.Create(new IdentityRole(Admin));
            }

            if (!roleManager.RoleExists(Employer))
            {
                var roleResult = roleManager.Create(new IdentityRole(Employer));
            }
        }

        private void SeedUsers()
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var un = "user";
            var empUn = "emp";
            var adminUn = "admin";
            var domain = "@jobhunters.bg";
            var pwd = "123123";

            if (!context.Users.Any())
            {
                var admin = new ApplicationUser() { UserName = adminUn + domain, Email = adminUn + domain };
                manager.Create(admin, pwd);
                manager.AddToRole(admin.Id, Admin);

                for (int i = 0; i < EmployersCount; i++)
                {
                    var testUser = new ApplicationUser() { UserName = empUn + i + domain, Email = empUn + i + domain };
                    manager.Create(testUser, pwd);
                    manager.AddToRole(testUser.Id, Employer);
                }

                for (int i = 0; i < 10; i++)
                {
                    var testUser = new ApplicationUser() { UserName = un + i + domain, Email = un + i + domain };
                    manager.Create(testUser, pwd);
                }
            }
        }

        private void SeedJobPosts()
        {
            const int createdOnIntervalMinutes = 30 * 24 * 60; // a month
            var rand = new Random();
            var users = context.Users.ToArray();
            var employers = context.Users.Where(u => u.UserName.StartsWith("emp")).ToArray();
            var categories = context.Categories.ToArray();
            var cities = context.Cities.ToArray();
            var separator = new string[] { "\r\n!@#\r\n" };
            var titles = File.ReadAllText(Server.MapPath("/SeedData/Titles.txt")).Split(separator, StringSplitOptions.RemoveEmptyEntries);
            var descriptions = File.ReadAllText(Server.MapPath("/SeedData/Descriptions.txt")).Split(separator, StringSplitOptions.RemoveEmptyEntries);

            if (!context.JobPosts.Any())
            {
                for (int i = 0; i < EmployersCount; i++)
                {
                    var author = employers[i];
                    for (int j = 0, len = rand.Next(1, 5); j < len; j++)
                    {
                        var randJob = rand.Next(0, titles.Length);
                        var title = titles[randJob];
                        var createdOn = DateTime.Now.AddMinutes(rand.Next(-createdOnIntervalMinutes, createdOnIntervalMinutes));
                        var description = descriptions[randJob];
                        var views = rand.Next(0, 1000);
                        var category = categories[rand.Next(0, categories.Length - 1)];
                        var city = cities[rand.Next(0, cities.Length - 1)];
                        var newJob = new JobPost {
                            Title = title,
                            CreatedOn = createdOn,
                            Description = description,
                            Author = author,
                            Views = views,
                            City = city,
                            Category = category,
                            OfferType = (OfferType)rand.Next(1, Enum.GetNames(typeof(OfferType)).Length),
                            HierarchyLevel = (HierarchyLevel)rand.Next(1, Enum.GetNames(typeof(HierarchyLevel)).Length),
                            WorkEmployement = (WorkEmployment)rand.Next(1, Enum.GetNames(typeof(WorkEmployment)).Length),
                        };

                        context.JobPosts.Add(newJob);
                    }
                }

                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    Console.Write(ex.EntityValidationErrors);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace JobHunters.WebFormsClient
{
    using JobHunters.Data;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class Global : HttpApplication
    {
        private const string Admin = "Admin";
        private const string Employer = "Employer";
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!roleManager.RoleExists(Admin))
            {
                var roleResult = roleManager.Create(new IdentityRole(Admin));
            }

            if (!roleManager.RoleExists(Employer))
            {
                var roleResult = roleManager.Create(new IdentityRole(Employer));
            }
        }
    }
}
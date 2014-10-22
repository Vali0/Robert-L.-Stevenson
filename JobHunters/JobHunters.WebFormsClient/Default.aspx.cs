using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobHunters.WebFormsClient
{
    using JobHunters.Data;
    using JobHunters.Data.UnitOfWork;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class _Default : Page
    {
        private static IJobHuntersData data;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            data = new ApplicationData(new ApplicationDbContext());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            this.statEmployers.Text = roleManager.Roles.First(r => r.Name == "Employer").Users.Count().ToString();
            this.statUsers.Text = data.Users.All().Count(u => u.Roles.Count == 0).ToString();
            this.statOffers.Text = data.JobPosts.All().Count().ToString();
        }
    }
}
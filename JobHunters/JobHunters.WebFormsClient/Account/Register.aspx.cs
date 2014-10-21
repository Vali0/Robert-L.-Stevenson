using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using JobHunters.WebFormsClient.Models;
using JobHunters.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using JobHunters.Data;

namespace JobHunters.WebFormsClient.Account
{
    public partial class Register : Page
    {
        private const string Admin = "Admin";
        private const string Employer = "Employer";
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));



            if (!roleManager.RoleExists(Admin))
            {
                var roleResult = roleManager.Create(new IdentityRole(Admin));
            }

            if (!roleManager.RoleExists(Employer))
            {
                var roleResult = roleManager.Create(new IdentityRole(Employer));
            }



            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                if (isEmployer.Checked)
                {
                    var employerResult = manager.AddToRole(user.Id, Employer);
                }

                //var adminResult = manager.AddToRole(user.Id, admin);
                IdentityHelper.SignIn(manager, user, isPersistent: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}
namespace JobHunters.WebFormsClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web;

    using JobHunters.Data;
    using JobHunters.Data.UnitOfWork;
    using JobHunters.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class OfferDetails : System.Web.UI.Page
    {
        private static IJobHuntersData data;

        private static ApplicationDbContext context;

        private JobPost offerItem;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            context = new ApplicationDbContext();
            data = new ApplicationData(context);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                this.Response.Redirect("/Account/Login.aspx");
            }
            var urlParams = ((List<string>)this.RouteData.DataTokens["FriendlyUrlSegments"]);
            if (urlParams.Any())
            {
                var index = urlParams[0];
                int indexParsed;
                if (!int.TryParse(index, out indexParsed))
                {
                    this.errContainer.Visible = true;
                    this.errContainer.InnerText = "Invalid Item ID !";
                    return;
                }

                var item = data.JobPosts.All().FirstOrDefault(x => x.Id == indexParsed);
                if (item == null)
                {
                    this.errContainer.Visible = true;
                    this.errContainer.InnerText = "There is no Job Offer with that ID !";
                    return;
                }
                this.offerItem = item;
            }
        }

        public JobPost Select()
        {
            var usrManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var currentUser = usrManager.FindById(HttpContext.Current.User.Identity.GetUserId());

            if (this.offerItem == null)
            {
                this.errContainer.Visible = true;
                this.errContainer.InnerText = "There is no Job Offer with that ID !";
                return null;
            }
            if (!this.IsPostBack
                && this.offerItem.Viewers.All(x => x.Id != HttpContext.Current.User.Identity.GetUserId())
                && !HttpContext.Current.User.IsInRole("Admin")
                && !HttpContext.Current.User.IsInRole("Employer"))
            {
                this.offerItem.Views += 1;
                
                this.offerItem.Viewers.Add(currentUser);

                if (this.offerItem.Author == null)
                {
                }
                data.JobPosts.Update(this.offerItem);
                data.SaveChanges();
            }
            if (!this.offerItem.Applicants.Contains(currentUser) 
                && !HttpContext.Current.User.IsInRole("Admin")
                && !HttpContext.Current.User.IsInRole("Employer"))
            {
                this.applyBtn.Visible = true;
            }
            else
            {
                if (this.offerItem.Applicants.Contains(currentUser))
                {
                    this.applyBtn.CssClass += " disabled";
                    this.applyBtn.Text = "Your application is being considered!";
                }
            }
            return this.offerItem;
        }
    }
}
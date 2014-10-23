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
    using JobHunters.Models;

    public partial class OfferDetails : System.Web.UI.Page
    {
        private static IJobHuntersData data;

        private JobPost offerItem;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            data = new ApplicationData(new ApplicationDbContext());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Account/Login.aspx");
            }
            var urlParams = ((List<string>)RouteData.DataTokens["FriendlyUrlSegments"]);
            if (urlParams.Any())
            {
                var index = urlParams[0];
                int indexParsed;
                if (!int.TryParse(index, out indexParsed))
                {
                    errContainer.Visible = true;
                    errContainer.InnerText = "Invalid Item ID !";
                    return;
                }

                var item = data.JobPosts.All().FirstOrDefault(x => x.Id == indexParsed);
                if (item == null)
                {
                    errContainer.Visible = true;
                    errContainer.InnerText = "There is no Job Offer with that ID !";
                    return;
                }
                offerItem = item;
            }
        }

        public JobPost Select()
        {
            if (offerItem == null)
            {
                errContainer.Visible = true;
                errContainer.InnerText = "There is no Job Offer with that ID !";
                return null;
            }
            if (!IsPostBack && Session[offerItem.Id.ToString()]==null)
            {
                offerItem.Views += 1;
                Session[offerItem.Id.ToString()] = "viewed";
                if (offerItem.Author == null)
                {

                }
                data.JobPosts.Update(offerItem);
                data.SaveChanges();
            }
            return offerItem;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobHunters.WebFormsClient
{
    using Error_Handler_Control;

    using JobHunters.Data;
    using JobHunters.Data.UnitOfWork;
    using JobHunters.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class ApplyForJobOffer : System.Web.UI.Page
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
                    ErrorSuccessNotifier.AddErrorMessage("Invalid Item ID !");
                    //this.errContainer.Visible = true;
                    //this.errContainer.Text += "Invalid Item ID !";
                    return;
                }

                var item = data.JobPosts.All().FirstOrDefault(x => x.Id == indexParsed);
                if (item == null)
                {
                    ErrorSuccessNotifier.AddErrorMessage("There is no Job Offer with that ID !");
                    //this.errContainer.Visible = true;
                    this.itemFound.Visible = false;
                    //this.errContainer.Text += "There is no Job Offer with that ID !";
                    return;
                }
                this.offerItem = item;
                ((Literal)this.itemFound.FindControl("OfferTitle")).Text = offerItem.Title;
            }
            else
            {
                ErrorSuccessNotifier.AddErrorMessage("There is no Job Offer with that ID !");
                this.itemFound.Visible = false;
                //this.errContainer.Visible = true;
                //this.errContainer.Text += "There is no Job Offer with that ID !";
            }
        }

        protected void Apply_Click(object sender, EventArgs e)
        {
            var usrManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var currentUser = usrManager.FindById(currentUserId);
            data=new ApplicationData(context);

            if (this.Cv.HasFile)
            {
                if (this.Cv.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ||
                    this.Cv.PostedFile.ContentType == "application/msword")
                {
                    var userName = currentUser.UserName;
                    var filePath = userName + "_curriculum_"+offerItem.Id+"_"  + ".docx";
                    this.Cv.SaveAs(Server.MapPath("~/Uploads/cv/"+filePath));
                    var application = new JobApplication()
                    {
                        AuthorId = currentUserId,
                        Author = usrManager.FindById(currentUserId),
                        JobPostId = offerItem.Id,
                        Comment = this.Comment.Text,
                        CvPath = filePath
                    };
                    data.JobApplications.Add(application);
                    data.SaveChanges();
                    offerItem.Applicants.Add(application);
                    if (this.offerItem.Author == null)
                    {
                    }
                    data.JobPosts.Update(offerItem);
                    data.SaveChanges();
                }
                else
                {
                    this.errContainer.Visible = true;
                    this.errContainer.Text += "The only allowed file formats are 'doc' and 'docx' !";
                    return;
                }
            }
            else
            {
                this.errContainer.Visible = true;
                this.errContainer.Text += "Uploading your CV is mandatory!";
                return;
            }

            
            Response.Redirect("/Default.aspx");
        }
    }
}
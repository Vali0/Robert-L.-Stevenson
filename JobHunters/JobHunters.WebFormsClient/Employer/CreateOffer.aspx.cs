namespace JobHunters.WebFormsClient.Employer
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Web;
    using JobHunters.Data;
    using JobHunters.Data.UnitOfWork;
    using JobHunters.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class CreateOffer : System.Web.UI.Page
    {
        private static ApplicationDbContext context;
        private static IJobHuntersData data;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            context = new ApplicationDbContext();
            data = new ApplicationData(context);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.JobType.DataSource =
                    Enum.GetNames(typeof(OfferType))
                        .Select(x => new { Text = x, Value = (int)(Enum.Parse(typeof(OfferType), x)) })
                        .ToList();
                this.JobType.DataTextField = "Text";
                this.JobType.DataValueField = "Value";
                this.JobType.DataBind();

                this.HierarchyLvl.DataSource =
                    Enum.GetNames(typeof(HierarchyLevel))
                        .Select(x => new { Text = x, Value = (int)(Enum.Parse(typeof(HierarchyLevel), x)) })
                        .ToList();
                this.HierarchyLvl.DataTextField = "Text";
                this.HierarchyLvl.DataValueField = "Value";
                this.HierarchyLvl.DataBind();

                this.Employment.DataSource =
                    Enum.GetNames(typeof(WorkEmployment))
                        .Select(x => new { Text = x, Value = (int)(Enum.Parse(typeof(WorkEmployment), x)) })
                        .ToList();
                this.Employment.DataTextField = "Text";
                this.Employment.DataValueField = "Value";
                this.Employment.DataBind();
            }
        }

        protected void CreateOffer_Click(object sender, EventArgs e)
        {
            var selectedCityId = int.Parse(this.City.SelectedValue);
            var selectedCategoryId = int.Parse(this.Category.SelectedValue);
            var offerTypeParsed = (OfferType)Enum.Parse(typeof(OfferType), this.JobType.SelectedItem.Text);
            var hierarchyLevelParsed =
                (HierarchyLevel)Enum.Parse(typeof(HierarchyLevel), this.HierarchyLvl.SelectedItem.Text);
            var workEmploymentParsed =
                (WorkEmployment)Enum.Parse(typeof(WorkEmployment), this.Employment.SelectedItem.Text);
            var usrManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var filename = "";
            if (this.UploadImage.HasFile)
            {
                if (this.UploadImage.PostedFile.ContentType == "image/jpeg" ||
                    this.UploadImage.PostedFile.ContentType == "image/gif" ||
                    this.UploadImage.PostedFile.ContentType == "image/png")
                {
                    filename = usrManager.FindById(HttpContext.Current.User.Identity.GetUserId()).UserName;
                    this.UploadImage.SaveAs(this.Server.MapPath("~/Uploads/Images/") + filename);
                }
            }

            var offer = new JobPost()
            {
                Author = usrManager.FindById(HttpContext.Current.User.Identity.GetUserId()),
                AuthorId = HttpContext.Current.User.Identity.GetUserId(),
                CategoryId = data.Categories.All().First(x => x.Id == selectedCategoryId).Id,
                CityId = data.Cities.All().First(x => x.Id == selectedCityId).Id,
                CreatedOn = DateTime.Now,
                Description = this.Description.Text,
                HierarchyLevel = hierarchyLevelParsed,
                OfferType = offerTypeParsed,
                WorkEmployement = workEmploymentParsed,
                Title = this.JobTitle.Text,
                ProfileImage = filename
            };

            data.JobPosts.Add(offer);
            data.SaveChanges();
            this.Response.Redirect("MyOffers.aspx");
        }

        public IEnumerable Select_Cities()
        {
            return data.Cities.All().ToList();
        }

        public IEnumerable Select_Categories()
        {
            return data.Categories.All().ToList();
        }
    }
}
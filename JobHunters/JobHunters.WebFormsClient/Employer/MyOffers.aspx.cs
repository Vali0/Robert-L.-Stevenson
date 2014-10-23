namespace JobHunters.WebFormsClient.Employer
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.ModelBinding;
    using System.Web.UI.WebControls;

    using JobHunters.Data;
    using JobHunters.Data.UnitOfWork;
    using JobHunters.Models;

    using Microsoft.AspNet.Identity;

    public partial class MyOffers : System.Web.UI.Page
    {
        private static IJobHuntersData data;

        public SortDirection sortDirection
        {
            get
            {
                if (this.ViewState["sortdirection"] == null)
                {
                    this.ViewState["sortdirection"] = SortDirection.Ascending;
                    return SortDirection.Ascending;
                }
                else if ((SortDirection)this.ViewState["sortdirection"] == SortDirection.Ascending)
                {
                    this.ViewState["sortdirection"] = SortDirection.Descending;
                    return SortDirection.Descending;
                }
                else
                {
                    this.ViewState["sortdirection"] = SortDirection.Ascending;
                    return SortDirection.Ascending;
                }
            }
            set
            {
                this.ViewState["sortdirection"] = value;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            data = new ApplicationData(new ApplicationDbContext());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IQueryable<JobPost> ListViewMyOffers_Select([ViewState("OrderBy")] String OrderBy = null)
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var items = data.JobPosts.All().Where(j => j.AuthorId == currentUserId).Include("City").Include("Category");
            if (OrderBy != null)
            {
                switch (this.sortDirection)
                {
                    case SortDirection.Ascending:
                        items = items.OrderByDescending(OrderBy);
                        break;
                    case SortDirection.Descending:
                        items = items.OrderBy(OrderBy);
                        break;
                    default:
                        items = items.OrderByDescending(OrderBy);
                        break;
                }
                this.ViewState["SortOrder"] = null;
            }
            return items;
        }

        public IEnumerable Select_Cities()
        {
            data = new ApplicationData(new ApplicationDbContext());
            return data.Cities.All().ToList();
        }

        public IEnumerable Select_Categories()
        {
            return data.Categories.All().ToList();
        }

        public IEnumerable Select_Type()
        {
            return Enum.GetNames(typeof(OfferType)).Select(x => new { Text = x, Value = x }).ToList();
        }

        public IEnumerable Select_Hierarchy()
        {
            return Enum.GetNames(typeof(HierarchyLevel)).Select(x => new { Text = x, Value = x }).ToList();
        }

        public IEnumerable Select_Employmemnt()
        {
            return Enum.GetNames(typeof(WorkEmployment)).Select(x => new { Text = x, Value = x }).ToList();
        }

        public void Update(int Id)
        {
            data = new ApplicationData(new ApplicationDbContext());
            JobPost item = data.JobPosts.All().FirstOrDefault(x => x.Id == Id);
            if (item == null)
            {
                this.ModelState.AddModelError("", String.Format("Product with id {0} was not found", Id));
                return;
            }
            this.TryUpdateModel(item);
            if (this.ModelState.IsValid)
            {
                data.JobPosts.Update(item);
                data.SaveChanges();
            }
        }

        protected void ListViewMyOffers_Sorting(object sender, ListViewSortEventArgs e)
        {
            e.Cancel = true;
            this.ViewState["OrderBy"] = e.SortExpression;
            this.ListViewMyOffers.DataBind();
        }

        public void Delete(int Id)
        {
            data = new ApplicationData(new ApplicationDbContext());
            var item = data.JobPosts.All().FirstOrDefault(x => x.Id == Id);
            if (item == null)
            {
                this.ModelState.AddModelError("", String.Format("Product with id {0} was not found", Id));
                return;
            }
            item.Viewers.Clear();
            var applicationsToDelete = data.JobApplications.All().Where(x=>x.JobPostId==item.Id);

            foreach (var app in applicationsToDelete)
            {
                data.JobApplications.Delete(app);
            }
            data.SaveChanges();
            item.Applicants.Clear();
            data.JobPosts.Update(item);
            if (item.Author==null)
            {
                
            }
            data.SaveChanges();
            data.JobPosts.Delete(item);
            data.SaveChanges();
        }
    }
}
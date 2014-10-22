namespace JobHunters.WebFormsClient.Employer
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.UI.WebControls;

    using JobHunters.Data;
    using JobHunters.Data.UnitOfWork;
    using JobHunters.Models;

    using Microsoft.AspNet.Identity;

    public partial class MyOffers : System.Web.UI.Page
    {
        private static IJobHuntersData data;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            data = new ApplicationData(new ApplicationDbContext());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public IQueryable<JobPost> ListViewMyOffers_Select()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            return
                data.JobPosts.All()
                    .Where(j => j.AuthorId == currentUserId)
                    .Include("City")
                    .Include("Category")
                    .OrderByDescending(j => j.CreatedOn);
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
            return Enum.GetNames(typeof(OfferType))
                        .Select(x => new { Text = x, Value = x})
                        .ToList();
        }

        public IEnumerable Select_Hierarchy()
        {
            return Enum.GetNames(typeof(HierarchyLevel))
                .Select(x => new { Text = x, Value = x})
                .ToList();
        }

        public IEnumerable Select_Employmemnt()
        {
             return Enum.GetNames(typeof(WorkEmployment))
                        .Select(x => new { Text = x, Value = x})
                        .ToList();
        }

        protected void City_OnDataBound(object sender, EventArgs e)
        {
           // (sender as DropDownList).SelectedValue=BindingContaine
        }

        public void Update(int Id)
        {
            data=new ApplicationData(new ApplicationDbContext());
            JobPost item = data.JobPosts.All().FirstOrDefault(x=>x.Id==Id);
            if (item == null)
            {
                ModelState.AddModelError("",
                    String.Format("Product with id {0} was not found", Id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                data.JobPosts.Update(item);
                data.SaveChanges();
            }
        }
    }
}
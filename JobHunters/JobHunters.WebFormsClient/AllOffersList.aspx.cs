using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;
    using System.Web.ModelBinding;
    using System.Web.UI.WebControls;

    using JobHunters.Data;
    using JobHunters.Data.UnitOfWork;
    using JobHunters.Models;

namespace JobHunters.WebFormsClient
{
    public partial class AllOffersList : System.Web.UI.Page
    {
        private static IJobHuntersData data;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            data = new ApplicationData(new ApplicationDbContext());
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public SortDirection SortDirection
        {
            get
            {
                if (ViewState["sortdirection"] == null)
                {
                    ViewState["sortdirection"] = SortDirection.Ascending;
                    return SortDirection.Ascending;
                }
                else if ((SortDirection)ViewState["sortdirection"] == SortDirection.Ascending)
                {
                    ViewState["sortdirection"] = SortDirection.Descending;
                    return SortDirection.Descending;
                }
                else
                {
                    ViewState["sortdirection"] = SortDirection.Ascending;
                    return SortDirection.Ascending;
                }
            }
            set
            {
                ViewState["sortdirection"] = value;
            }
        }

        public IQueryable<JobPost> ListViewAllOffers_Select([ViewState("OrderBy")]String OrderBy = null)
        {
            var items = data.JobPosts.All().Include("City").Include("Category");
            if (OrderBy != null)
            {
                switch (SortDirection)
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
                ViewState["SortOrder"] = null;
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
            return Enum.GetNames(typeof(OfferType))
                        .Select(x => new { Text = x, Value = x })
                        .ToList();
        }

        public IEnumerable Select_Hierarchy()
        {
            return Enum.GetNames(typeof(HierarchyLevel))
                .Select(x => new { Text = x, Value = x })
                .ToList();
        }

        public IEnumerable Select_Employmemnt()
        {
            return Enum.GetNames(typeof(WorkEmployment))
                       .Select(x => new { Text = x, Value = x })
                       .ToList();
        }

        protected void ListViewAllOffers_Sorting(object sender, ListViewSortEventArgs e)
        {
            e.Cancel = true;
            ViewState["OrderBy"] = e.SortExpression;
            ListViewAllOffers.DataBind();
        }
    }
}
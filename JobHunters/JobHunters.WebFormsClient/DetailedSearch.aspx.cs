using JobHunters.Data;
using JobHunters.Data.UnitOfWork;
using JobHunters.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobHunters.WebFormsClient
{
    public partial class DetailedSearch : System.Web.UI.Page
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
            if (!Page.IsPostBack)
            {
                this.CheckBoxListLevels.DataSource =
                    Enum.GetNames(typeof(HierarchyLevel))
                        .Select(x => new { Text = x, Value = (int)(Enum.Parse(typeof(HierarchyLevel), x)) })
                        .ToList();
                this.CheckBoxListLevels.DataTextField = "Text";
                this.CheckBoxListLevels.DataValueField = "Value";
                this.CheckBoxListLevels.DataBind();

                this.RadioButtonListEployements.DataSource =
                    Enum.GetNames(typeof(WorkEmployment))
                        .Select(x => new { Text = x, Value = (int)(Enum.Parse(typeof(WorkEmployment), x)) })
                        .ToList();
                this.RadioButtonListEployements.DataTextField = "Text";
                this.RadioButtonListEployements.DataValueField = "Value";
                this.RadioButtonListEployements.DataBind();
            }
        }

        public IEnumerable Select_Cities()
        {
            return data.Cities.All().ToList();
        }

        public IEnumerable Select_Categories()
        {
            return data.Categories.All().ToList();
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

        protected void ListViewAllOffers_Sorting(object sender, ListViewSortEventArgs e)
        {
            e.Cancel = true;
            ViewState["OrderBy"] = e.SortExpression;
            this.GridViewFilteredOffers.DataBind();
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            var result = this.GetAllOffers();
            this.GridViewFilteredOffers.DataSource = result;
            this.GridViewFilteredOffers.DataBind();
        }

        protected void GridViewFilteredOffers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridViewFilteredOffers.PageIndex = e.NewPageIndex;
            this.GridViewFilteredOffers.DataSource = this.GetAllOffers();
            this.GridViewFilteredOffers.DataBind();
        }

        private List<JobPost> GetAllOffers()
        {
            var city = this.DropDownListCities.SelectedItem.Text;
            var category = this.DropDownListCategories.SelectedItem.Text;
            List<int> levels = new List<int>();

            foreach (ListItem item in this.CheckBoxListLevels.Items)
            {
                if (item.Selected)
                {
                    levels.Add(int.Parse(item.Value));
                }
            }

            var employement = this.RadioButtonListEployements.SelectedItem;
            var keyWords = this.TextBoxKeyWords.Text.Split(new char[] { ',', ' ', ';', ':', '.' },
                           StringSplitOptions.RemoveEmptyEntries);

            var offers = data.JobPosts.All();

            if (!string.IsNullOrEmpty(city) && city!="--Select All--")
            {
                offers = offers.Where(o => o.City.Name == city);
            }
            if (!string.IsNullOrEmpty(category) && category != "--Select All--")
            {
                offers = offers.Where(o => o.Category.Name == category);
            }
            if (levels.Count > 0)
            {
                foreach (var level in levels)
                {
                    offers = offers.Where(o => (int)o.HierarchyLevel == level);
                }
            }
            if (employement != null)
            {
                int empl = int.Parse(employement.Value);
                offers = offers.Where(o => (int)o.WorkEmployement == empl);
            }
            if (keyWords != null)
            {
                foreach (var word in keyWords)
                {
                    offers = offers.Where(o => o.Title.Contains(word));
                }
            }

            var result = offers.ToList();

            return result;
        }
    }
}
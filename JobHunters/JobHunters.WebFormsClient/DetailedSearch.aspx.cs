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
            if (!this.IsPostBack)
            {
                //this.JobType.DataSource =
                //    Enum.GetNames(typeof(OfferType))
                //        .Select(x => new { Text = x, Value = (int)(Enum.Parse(typeof(OfferType), x)) })
                //        .ToList();
                //this.JobType.DataTextField = "Text";
                //this.JobType.DataValueField = "Value";
                //this.JobType.DataBind();

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
    }
}
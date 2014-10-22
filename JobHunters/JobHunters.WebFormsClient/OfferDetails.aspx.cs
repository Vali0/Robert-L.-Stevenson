using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobHunters.WebFormsClient
{
    public partial class OfferDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var urlParams = ((List<string>)RouteData.DataTokens["FriendlyUrlSegments"]);
            if (urlParams.Any())
            {
                var index = urlParams[0];
            }
        }
    }
}
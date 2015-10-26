using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.Custom.Widgets
{
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.Custom.Designer.ClaimFormDesigner))]
    public partial class ClaimsForm : System.Web.UI.UserControl
    {
        public string MyLabel { get; set; }
        public bool MyCheckbox { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                string url = HttpContext.Current.Request.Url.AbsolutePath;
                Session["PageUrl"] = url;
            }

            MyLabel = Literal1.Text;
            MyCheckbox = CheckBox1.Checked;
        }
    }
}
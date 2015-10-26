using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.Custom.Widgets
{
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.Custom.Designer.ConfigDesigner))]
    public partial class ConfigControl : System.Web.UI.UserControl
    {
        public string ParentName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ParentLiteral.Text = ParentName;
        }
    }
}
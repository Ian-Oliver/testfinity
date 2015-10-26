using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.Custom.Widgets
{
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.Custom.Designer.TestDesigner))]
    public partial class TestControl : System.Web.UI.UserControl
    {
        public string MyLabel { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                string url = HttpContext.Current.Request.Url.AbsolutePath;
                Session["PageUrl"] = url;            
            }
                
            Label1.Text = MyLabel;            
        }       
        
    }
}
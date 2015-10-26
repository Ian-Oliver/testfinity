using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using SitefinityWebApp.Mvc.Models;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "MvcWidget", Title = "MvcWidget", SectionName = "MvcWidgets"), Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.WidgetDesigners.MvcWidget.MvcWidgetDesigner))]
    public class MvcWidgetController : Controller
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        
        public string LoginPlaceholder { get; set; }
        public string UsernamePlaceholder { get; set; }
        public string PasswordPlaceholder { get; set; }   

        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            if (HttpContext.Session != null)
            {
                string url = Request.Url.PathAndQuery;
                Session["PageUrl"] = url;
            }

            var model = new MvcWidgetModel();
            this.LoginPlaceholder = this.LoginPlaceholder ?? "Login";
            this.UsernamePlaceholder = this.UsernamePlaceholder ?? "Username";
            this.PasswordPlaceholder = this.PasswordPlaceholder ?? "Password";
            model.LoginPlaceholder = this.LoginPlaceholder;//this.LoginPlaceholder;
            model.UsernamePlaceholder = this.UsernamePlaceholder;
            model.PasswordPlaceholder = this.PasswordPlaceholder;
            
            return View("Default", model);
        }
    }
}
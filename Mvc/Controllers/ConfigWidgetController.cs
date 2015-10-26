using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using SitefinityWebApp.Mvc.Models;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "ConfigWidget", Title = "ConfigWidget", SectionName = "MvcWidgets"), Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.WidgetDesigners.ConfigWidget.ConfigWidgetDesigner))]
    public class ConfigWidgetController : Controller
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string ParentName { get; set; }
        public bool MyCheckbox { get; set; }

        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            var model = new ConfigWidgetModel();
            
            return View("Default", model);
        }
    }
}
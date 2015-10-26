using System;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Collections.Generic;
using Telerik.Sitefinity.Pages.Model;

namespace SitefinityWebApp.Custom.Designer
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="SitefinityWebApp.Custom.Widgets.ClaimsForm"/> widget
    /// </summary>
    public class ClaimFormDesigner : ControlDesignerBase
    {
        #region Properties
        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the layout template's relative or virtual path.
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(base.LayoutTemplatePath))
                    return ClaimFormDesigner.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        #endregion

        #region Control references
        /// <summary>
        /// Gets the control that is bound to the MyLabel property
        /// </summary>
        protected virtual Control MyLabel
        {
            get
            {
                return this.Container.GetControl<Control>("MyLabel", true);
            }
        }

        /// <summary>
        /// Gets the control that is bound to the MyCheckbox property
        /// </summary>
        protected virtual Control MyCheckbox
        {
            get
            {
                return this.Container.GetControl<Control>("MyCheckbox", true);
            }
        }

        protected Control StandardValue
        {
            get
            {
                return this.Container.GetControl<Control>("StandardValue", true);
            }
        }

        #endregion

        #region Methods
        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
            var url = System.Web.HttpContext.Current.Session["PageUrl"].ToString();
            var regEx = new System.Text.RegularExpressions.Regex(@"^/([a-zA-Z]+)/[a-zA-Z/]*$");
            var match = regEx.Match(url);
            string pageName = "";

            if (!match.Success)
                throw new NullReferenceException("The url did not contain the current page name");

            pageName = match.Groups[1].ToString().ToLower();

            var pManager = Telerik.Sitefinity.Modules.Pages.PageManager.GetManager();

            var sites = new Telerik.Sitefinity.Multisite.MultisiteManager();

            var homePage = pManager.GetPageDataList().Where(p => p.NavigationNode.Title.ToString().ToLower() == pageName && p.NavigationNode.ParentId == new Guid(System.Web.SiteMap.RootNode.Key)).FirstOrDefault();

            if (homePage == null)
                throw new NullReferenceException("The current published page could not be found");

            var homePagePreview = pManager.GetPreview(homePage.NavigationNode.PageId);

            if (homePagePreview == null)
                throw new NullReferenceException("The current preview page could not be found");

            var configPage = pManager.GetPageDataList().Where(p => p.NavigationNode.Title.ToString().ToLower() == "config" && p.NavigationNode.ParentId == new Guid(System.Web.SiteMap.RootNode.Key)).FirstOrDefault();

            if (configPage == null)
                throw new NullReferenceException("The config page could not be found");

            var parentName = configPage.Controls[0].Properties.FirstOrDefault(p => p.Name == "ParentName");

            if (parentName == null)//no parent, most likely is parent site
                return;

            var parentSiteName = parentName.Value;

            if (parentSiteName == "")
                throw new NullReferenceException("The ParentName property is empty");

            var parentSite = sites.GetSites().Where(p => p.Name == parentSiteName).FirstOrDefault();

            if (parentSite == null)
                throw new NullReferenceException("The parent site name: " + parentSiteName + " could not be found");

            var thisPage = pManager.GetPageDataList().Where(p => p.NavigationNode.Title.ToString().ToLower() == pageName && p.NavigationNode.ParentId == parentSite.SiteMapRootNodeId).FirstOrDefault();

            if (thisPage == null)
                throw new NullReferenceException("The current page of the parent site could not be found");

            thisPage.Controls[0].Properties.ToList().ForEach(p =>
            {
                ControlProperty homeProperty = null;
                if ((homeProperty = homePagePreview.Controls[0].Properties.FirstOrDefault(n => n.Name == p.Name)) != null)
                {
                    if (p.Value != homeProperty.Value)
                    {
                        StandardValue.Visible = false;
                    }
                }
            });
        }
        #endregion

        #region IScriptControl implementation
        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptDescriptor> GetScriptDescriptors()
        {
            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();

            descriptor.AddElementProperty("myLabel", this.MyLabel.ClientID);
            descriptor.AddElementProperty("myCheckbox", this.MyCheckbox.ClientID);

            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(ClaimFormDesigner.scriptReference));
            return scripts;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/Custom/Designer/ClaimFormDesigner.ascx";
        public const string scriptReference = "~/Custom/Designer/ClaimFormDesigner.js";
        #endregion
    }
}
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Modules.Pages;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Multisite;

namespace SitefinityWebApp.WidgetDesigners
{
    public class WidgetDesignerHelper
    {
        /// <summary>
        /// Gets the parent site of the current page by using the ParentName property of the Config page
        /// </summary>
        /// <param name="pageManager">The page manager</param>
        /// <returns>The parent site</returns>
        public Site GetParentSite(PageManager pageManager, Guid siteRootNode)
        {
            var sites = new MultisiteManager();

            var configPage = new PageData();

            foreach(var f in pageManager.GetPageDataList())
            {                
                if(f.NavigationNode.Title.GetString(f.NavigationNode.AvailableCultures[0], false).ToLower() == "config"
                    && !f.NavigationNode.IsDeleted
                    && f.NavigationNode.ParentId == siteRootNode)
                {
                    configPage = f;
                    break;
                }
            }
            

            if (configPage == null)
                return null;

            var parentName = configPage.Controls[0].Properties.FirstOrDefault(r => r.Name == "Settings").ChildProperties.FirstOrDefault(p => p.Name.ToLower() == "parentname");

            if (parentName == null)
                return null;

            var parentSiteName = parentName.Value;

            if (parentSiteName == "")
                return null;

            var parentSite = sites.GetSites().Where(p => p.Name == parentSiteName).FirstOrDefault();

            return parentSite;
        }

        /// <summary>
        /// Gets the current page name that is stored within the PageUrl key of the session
        /// </summary>
        /// <returns>The current page name</returns>
        public string GetPageName()
        {
            var url = HttpContext.Current.Session["PageUrl"].ToString();
            var regEx = new System.Text.RegularExpressions.Regex(@"/([a-zA-Z]+)/Action/Edit$");
            var match = regEx.Match(url);
            string pageName = "";

            if (match.Success)
                pageName = match.Groups[1].ToString();

            return pageName;
        }

        /// <summary>
        /// Gets the currnt page's preview version
        /// </summary>
        /// <param name="pageManager">The page manager</param>
        /// <param name="pageName">The current page name</param>
        /// <returns>The current page preview</returns>
        public PageDraft GetCurrentPagePreview(PageManager pageManager, string pageName)
        {
            var homePage = new PageData();

            foreach (var f in pageManager.GetPageDataList())
            {
                if (f.NavigationNode.Title.GetString(f.NavigationNode.AvailableCultures[0], false).ToLower() == pageName.ToLower()
                                                    && !f.NavigationNode.IsDeleted
                                                    && f.NavigationNode.ParentId == new Guid(System.Web.SiteMap.RootNode.Key))
                {
                    homePage = f;
                    break;
                }
            }
            
            if (homePage == null)
                return null;

            var homePagePreview = pageManager.GetPreview(homePage.NavigationNode.PageId);

            if (homePagePreview == null)
                return null;

            if (homePagePreview.Controls[0].Properties.FirstOrDefault(r => r.Name == "Settings") == null)
                return null;

            return homePagePreview;
        }

        /// <summary>
        /// Gets the current page's parent page from the parent site
        /// </summary>
        /// <param name="pageManager">The page manager</param>
        /// <param name="parentSite">The parent site</param>
        /// <param name="pageName">The current page name</param>
        /// <returns></returns>
        public PageData GetParentPage(PageManager pageManager, Site parentSite, string pageName)
        {
            var parentPage = new PageData();

            foreach (var f in pageManager.GetPageDataList())
            {
                if (f.NavigationNode.Title.GetString(f.NavigationNode.AvailableCultures[0], false).ToLower() == pageName.ToLower()
                                                                 && f.NavigationNode.ParentId == parentSite.SiteMapRootNodeId)
                {
                    parentPage = f;
                    break;
                }
            }
            
            return parentPage;
        }

        /// <summary>
        /// Performs the logic to mark a label as 'standard value' if the value matches the parent's value
        /// </summary>
        /// <param name="parentPage">The parent page</param>
        /// <param name="currentPagePreview">The current page preview</param>
        /// <param name="controls">The controls of the designer page</param>
        public void AddStandardValue(IEnumerable<Control> controls)
        {
            try
            {
                var pageManager = PageManager.GetManager();

                var parentSite = GetParentSite(pageManager, new Guid(System.Web.SiteMap.RootNode.Key));

                if (parentSite == null)
                    return;

                var pageName = GetPageName();

                if (pageName == "")
                    return;

                var currentPagePreview = GetCurrentPagePreview(pageManager, pageName);

                if (currentPagePreview == null)
                    return;

                var parentPage = GetParentPage(pageManager, parentSite, pageName);

                if (parentPage == null)
                    return;

                parentPage.Controls[0].Properties.First(r => r.Name.ToLower() == "settings").ChildProperties.ToList().ForEach(p =>
                {
                    ControlProperty currentProperty = null;
                    var settings = currentPagePreview.Controls[0].Properties.FirstOrDefault(r => r.Name.ToLower() == "settings");
                    if ((currentProperty = settings.ChildProperties.FirstOrDefault(n => n.Name.ToLower() == p.Name.ToLower())) != null)
                    {
                        if (p.Value == currentProperty.Value)
                        {
                            controls.ToList().ForEach(c =>
                            {
                                if (c is Label && ((Label)c).AssociatedControlID.ToLower() == currentProperty.Name.ToLower())
                                {
                                    ((Label)c).Text += "<span style='color:grey;font-style:italic;font-size:80%'>&nbsp;(Standard Value)</span>";
                                }
                            });
                        }
                    }
                });
            }
            catch (Exception)
            {
                //add logging here
            }            
        }
    }
}
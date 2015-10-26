using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using SitefinityWebApp.Mvc.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.ActionFilters;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Multisite;
using System.Globalization;
using Telerik.Sitefinity.Localization;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "PageCompareWidget", Title = "PageCompareWidget", SectionName = "MvcWidgets")]
    public class PageCompareWidgetController : Controller
    {
        /// <summary>
        /// Returns the view with the model populated with pages
        /// </summary>        
        public ActionResult Index()
        {
            var model = new PageCompareWidgetModel();
            model.Pages = new List<SitefinityPage>();
            model.Sites = new List<SitefinitySite>();

            var pageManager = PageManager.GetManager();

            var sites = new MultisiteManager();

            var allSites = sites.GetSites();

            Site standardSite = null;
                        
            foreach (var site in sites.GetSites())
            {
                if(allSites.ToList().IndexOf(site) == 0)
                    standardSite = site;                
            }

            int pageIndex = 1;
            foreach(var p in pageManager.GetPageDataList())
            {
                //if (p.NavigationNode.Title.Value.ToLower() == "test" && p.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live)
                //{
                //    CultureInfo targetCultureInfo = CultureInfo.GetCultureInfo("es");

                //    // Set values for PageNode properties
                //    p.NavigationNode.Title.SetString(targetCultureInfo, p.NavigationNode.Title.Value);
                //    p.NavigationNode.Description.SetString(targetCultureInfo, p.NavigationNode.Title.Value);
                //    p.NavigationNode.UrlName.SetString(targetCultureInfo, p.NavigationNode.Title.Value);

                //    if (p.NavigationNode.LocalizationStrategy != LocalizationStrategy.Synced)
                //    {
                //        // Set the pageNode LocalizationStrategy for the first method call
                //        pageManager.InitializePageLocalizationStrategy(p.NavigationNode, LocalizationStrategy.Synced, false);
                //    }


                //    // Set values for PageData properties 
                //    p.HtmlTitle.SetString(targetCultureInfo, p.NavigationNode.Title.Value);
                //    p.Description.SetString(targetCultureInfo, p.NavigationNode.Title.Value);
                //    p.Keywords.SetString(targetCultureInfo, p.NavigationNode.Title.Value);

                //    // Publish the page
                //    p.NavigationNode.ApprovalWorkflowState.SetString(targetCultureInfo, "Published");
                //    pageManager.SaveChanges();
                //    break;
                //}

                if(p.NavigationNode.ParentId == standardSite.SiteMapRootNodeId 
                    && !p.IsDeleted 
                    && p.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live
                    && p.Controls != null
                    && p.Controls.Count > 0
                    && p.Controls[0].Properties != null
                    && p.Controls[0].Properties.Count > 0
                    && p.Controls[0].Properties.FirstOrDefault(r => r.Name == "Settings") != null)
                {
                    model.Pages.Add(new SitefinityPage(pageIndex++, p.NavigationNode.Title.GetString(p.NavigationNode.AvailableCultures[0], false)));
                }                
            }
            
            return View("Default", model);
        }


        /// <summary>
        /// Gets all sites that have a parent site or a sibling site
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSites()
        {
            var pageManager = PageManager.GetManager();

            var sites = new MultisiteManager();

            var allSites = sites.GetSites();
            var dropDownSites = new List<SitefinitySite>();

            foreach (var site in sites.GetSites())
            {
                var helper = new WidgetDesigners.WidgetDesignerHelper();
                var parentSite = helper.GetParentSite(pageManager, site.SiteMapRootNodeId);

                var parentSiteName = "";

                if (parentSite != null)
                    parentSiteName = parentSite.Name;

                if (((List<SitefinitySite>)GetSitesWithParam(site.Name, parentSiteName).Data).Count != 0)
                    dropDownSites.Add(new SitefinitySite(site.Id, site.Name, parentSiteName, site.SiteMapRootNodeId));
            }

            return Json(dropDownSites, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets all sites that are a sibling or child of 'site'
        /// </summary>
        /// <param name="siteName">The site name</param>
        /// <param name="myParentSiteName">The parent site</param>
        /// <returns></returns>
        public JsonResult GetSitesWithParam(string siteName, string myParentSiteName)
        {
            var pageManager = PageManager.GetManager();
            var sites = new MultisiteManager();

            var allSites = sites.GetSites();
            var dropDownSites = new List<SitefinitySite>();
            var i = 0;
            
            foreach(var sitee in allSites)
            {                
                var helper = new WidgetDesigners.WidgetDesignerHelper();
                var parentSite = helper.GetParentSite(pageManager, sitee.SiteMapRootNodeId);

                var parentSiteName = "";

                if (parentSite != null)
                    parentSiteName = parentSite.Name;

                if(parentSiteName == siteName || (parentSiteName == myParentSiteName && sitee.Name != siteName))
                {
                    dropDownSites.Add(new SitefinitySite(sitee.Id, sitee.Name, siteName, sitee.SiteMapRootNodeId));
                    i++;
                }
            }

            return Json(dropDownSites, JsonRequestBehavior.AllowGet);
        }

        


        /// <summary>
        /// Performs logic to get the property of 'pageToLookUp'. If a propertyName is passed then it looks for the property with that name,
        /// if not then it returns the first property it finds.
        /// </summary>
        /// <param name="pManager">The page manager</param>
        /// <param name="siteMapRootNodeId">The sitemaprootnodeid</param>
        /// <param name="pageToLookUp">The page to look up</param>
        /// <param name="propertyName">The property name</param>
        /// <param name="pfi">The list of fields</param>
        /// <returns></returns>
        public ControlProperty BuildFieldInfo(PageManager pManager, Guid siteMapRootNodeId, string pageToLookUp, string propertyName, List<PageFieldInfo> pfi)
        {
            ControlProperty controlProperty = null;
            var page = pManager.GetPageDataList().FirstOrDefault(p => p.NavigationNode.Title.GetString(p.NavigationNode.AvailableCultures[0], false) == pageToLookUp
                                                                 && p.NavigationNode.ParentId == siteMapRootNodeId 
                                                                 && !p.NavigationNode.IsDeleted 
                                                                 && p.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live);

            if (page != null)
            {
                foreach (var property in page.Controls[0].Properties.First(r => r.Name.ToLower() == "settings").ChildProperties.ToList())
                {
                    if (pfi.Any(a => a.FieldName == property.Name))
                        continue;

                    if (propertyName == "" || propertyName == property.Name)
                    {                        
                        controlProperty = property;
                        break;
                    }                            
                }
            }

            return controlProperty;
        }

        /// <summary>
        /// Returns a partial view with the properties and values of the page and sites passed in
        /// </summary>
        /// <param name="info">An object that contains the page to look for properties for each site</param>
        /// <returns></returns>
        [HttpGet]
        [StandaloneResponseFilter]
        public PartialViewResult GetFields(PageCompareInfo info)
        {
            var pageManager = PageManager.GetManager();

            var pfi = new List<PageFieldInfo>();

            while (true)
            {
                var controlProperty = BuildFieldInfo(pageManager, info.SiteRootNodeId1, info.Page, "", pfi);

                if (controlProperty == null)
                    break;

                var fieldInfo = new PageFieldInfo();
                fieldInfo.FieldName = controlProperty.Name;
                fieldInfo.Site1 = controlProperty.Value;

                controlProperty = BuildFieldInfo(pageManager, info.SiteRootNodeId2, info.Page, controlProperty.Name, pfi);

                if (controlProperty == null)
                    break;

                fieldInfo.Site2 = controlProperty.Value;
                if (info.SiteRootNodeId3 != null)
                {
                    controlProperty = BuildFieldInfo(pageManager, info.SiteRootNodeId3, info.Page, controlProperty.Name, pfi);

                    if (controlProperty == null)
                        break;

                    fieldInfo.Site3 = controlProperty.Value;
                }

                pfi.Add(fieldInfo);
            }

            List<PageFieldInfo> pageFieldInfos = pfi;

            return PartialView(pageFieldInfos);
        }

    }
}
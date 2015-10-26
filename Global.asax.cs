using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;

namespace SitefinityWebApp
{
    public class Global : System.Web.HttpApplication
    {
        bool pageCreated = false;
        bool skippedFirstGetPageNode = false;

        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Initialized += Bootstrapper_Initialized;
        }

        void Bootstrapper_Initialized(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapped")
            {
                
                PageManager.Executing += new EventHandler<Telerik.Sitefinity.Data.ExecutingEventArgs>(PageManager_Executing);
                PageManager.Executed += new EventHandler<Telerik.Sitefinity.Data.ExecutedEventArgs>(PageManager_Executed);
            }
        }

        private void PageManager_Executed(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs e)
        {
            if(e.CommandName == "CreatePageNode")
            {
                pageCreated = true;
            }
            else if (e.CommandName == "GetPageNode" && pageCreated && !skippedFirstGetPageNode)
            {
                skippedFirstGetPageNode = true;
            }
            else if(pageCreated && skippedFirstGetPageNode && e.CommandName == "GetPageNode")
            {
                pageCreated = false;
                skippedFirstGetPageNode = false;
                //var provider = sender as PageDataProvider;
                var pageManager = PageManager.GetManager();
                //var nodes = pageManager.GetPageNodes();
                var pNode = (Telerik.Sitefinity.Pages.Model.PageNode)e.Data;

                var p = pNode.GetPageData();
                                
                var allSites = new MultisiteManager();

                var thisSite = allSites.GetSites().FirstOrDefault(s => s.SiteMapRootNodeId == new Guid(System.Web.SiteMap.RootNode.Key));

                var childSites = GetChildSites(thisSite.Name);

                foreach (var p2 in pageManager.GetPageDataList())
                {
                    if (p2.NavigationNode.Title.GetString(p2.NavigationNode.AvailableCultures[0], false) == p.NavigationNode.Title.GetString(p.NavigationNode.AvailableCultures[0], false)
                            && !p.NavigationNode.IsDeleted 
                            && p2.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live)
                    {
                        if (childSites.Any(s => s.SiteMapRootNodeId == p2.NavigationNode.ParentId))
                        {
                            System.Globalization.CultureInfo targetCultureInfo = System.Globalization.CultureInfo.GetCultureInfo("es");

                            // Set values for PageNode properties
                            p2.NavigationNode.Title.SetString(targetCultureInfo, "test");
                            p2.NavigationNode.Description.SetString(targetCultureInfo, "test");
                            p2.NavigationNode.UrlName.SetString(targetCultureInfo, "test");

                            if (p2.NavigationNode.LocalizationStrategy != Telerik.Sitefinity.Localization.LocalizationStrategy.Synced)
                            {
                                // Set the pageNode LocalizationStrategy for the first method call
                                pageManager.InitializePageLocalizationStrategy(p2.NavigationNode, Telerik.Sitefinity.Localization.LocalizationStrategy.Synced, false);
                            }
                            
                            // Set values for PageData properties 
                            p2.HtmlTitle.SetString(targetCultureInfo, "test");
                            p2.Description.SetString(targetCultureInfo, "test");
                            p2.Keywords.SetString(targetCultureInfo, "test");

                            // Publish the page
                            p2.NavigationNode.ApprovalWorkflowState.SetString(targetCultureInfo, "Published");
                            pageManager.SaveChanges();

                        }
                    }
                }
            }
        }

        public List<Site> GetChildSites(string siteName)
        {
            var siteList = new List<Site>();
            var pageManager = PageManager.GetManager();
            var sites = new MultisiteManager();

            var allSites = sites.GetSites();

            foreach (var sitee in allSites)
            {
                var helper = new WidgetDesigners.WidgetDesignerHelper();
                var parentSite = helper.GetParentSite(pageManager, sitee.SiteMapRootNodeId);

                var parentSiteName = "";

                if (parentSite != null)
                    parentSiteName = parentSite.Name;

                if (parentSiteName == siteName)
                {
                    siteList.Add(sitee);                    
                }
            }

            return siteList;
        }

        private void PageManager_Executing(object sender, Telerik.Sitefinity.Data.ExecutingEventArgs e)
        {
            //if(e.CommandName == "CreatePageNode")//if (e.CommandName == "CommitTransaction" || e.CommandName == "FlushTransaction")
            //{                
            //    var provider = sender as PageDataProvider;
                
            //    var dirtyItems = provider.GetDirtyItems();
            //    if (dirtyItems.Count != 0)
            //    {
            //        foreach (var item in dirtyItems)
            //        {
            //            //Can be New, Updated, or Deleted
            //            SecurityConstants.TransactionActionType itemStatus = provider.GetDirtyItemStatus(item);
            //            var page = item as PageDraft;
            //            if (page != null)
            //            {
            //                if (itemStatus == SecurityConstants.TransactionActionType.New
            //                    || itemStatus == SecurityConstants.TransactionActionType.Updated)
            //                {
            //                    //Edit Page
            //                    if (page.IsTempDraft)
            //                    {
            //                        //plug your logic here
            //                    }

            //                    //Publish Page
            //                    if (!page.IsTempDraft)
            //                    {
            //                        //plug your logic here
            //                    }

            //                }
            //            }
            //        }
            //    }
            //}
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
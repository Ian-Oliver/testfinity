using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SitefinityWebApp.Mvc.Models
{
    public class PageCompareWidgetModel
    {
        /// <summary>
        /// Gets or sets the list of sitefinity pages.
        /// </summary>
        public List<SitefinityPage> Pages { get; set; }
        public List<SitefinitySite> Sites { get; set; }
        public int SelectedPage { get; set; }
        public int SelectedSiteFirst { get; set; }
        public int SelectedSiteSecond { get; set; }

        public IEnumerable<SelectListItem> PageItems
        {
            get { return new SelectList(Pages, "id", "Name");  }
        }

        public IEnumerable<SelectListItem> SiteItemsFirst
        {
            get { return new SelectList(Sites, "id", "Name"); }
        }

        public IEnumerable<SelectListItem> SiteItemsSecond
        {
            get { return new SelectList(Sites, "id", "Name"); }
        }
    }
}
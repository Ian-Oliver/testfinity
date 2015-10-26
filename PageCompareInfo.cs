using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp
{
    public class PageCompareInfo
    {
        public string  Page { get; set; }
        public Guid SiteRootNodeId1 { get; set; }
        public Guid SiteRootNodeId2 { get; set; }
        public Guid SiteRootNodeId3 { get; set; }
    }
}
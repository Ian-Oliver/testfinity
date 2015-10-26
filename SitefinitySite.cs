using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp
{
    public class SitefinitySite
    {
        public Guid SiteId { get; set; }
        public string Name { get; set; }
        public string ParentSite { get; set; }
        public Guid SiteRootNodeId { get; set; }

        public SitefinitySite(Guid id, string name, string parentSite, Guid siteRootNodeId)
        {
            SiteId = id;
            Name = name;
            ParentSite = parentSite;
            SiteRootNodeId = siteRootNodeId;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp
{
    public class SitefinityPage
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SitefinityPage(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
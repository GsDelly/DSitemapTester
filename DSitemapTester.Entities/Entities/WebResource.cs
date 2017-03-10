using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Entities.Entities
{
    public class WebResource
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public virtual ICollection<SitemapResource> SitemapResources { get; set; }

        public virtual ICollection<WebResourceTest> Tests { get; set; }
    }
}

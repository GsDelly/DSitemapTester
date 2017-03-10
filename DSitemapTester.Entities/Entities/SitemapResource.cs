using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Entities.Entities
{
    public class SitemapResource
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public virtual WebResource WebResource { get; set; }
    }
}

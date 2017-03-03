using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Dtos
{
    public class WebResourceDto
    {
        public string Url { get; set; }

        public TestDto Test { get; set; }

        public IEnumerable<SitemapDto> Sitemaps { get; set; }
    }
}

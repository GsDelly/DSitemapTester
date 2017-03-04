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

        public IEnumerable<TestDto> Tests { get; set; }
    }
}

using DSitemapTester.Tester.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Entities
{
    public class Sitemap
    {
        public string Url { get; set; }

        public DateTime LastModification { get; set; }

        public Frequency Frequency { get; set; }

        public double Priority { get; set; }
    }
}

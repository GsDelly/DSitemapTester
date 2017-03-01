using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Entities.Entities
{
    public class TestResult
    {
        public int Id { get; set; }

        public SitemapResource SitemapResource { get; set; }

        public double ResponseTime { get; set; }

        public virtual Test Test { get; set; }
    }
}

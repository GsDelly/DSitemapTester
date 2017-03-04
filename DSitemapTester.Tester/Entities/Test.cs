using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Entities
{
    public class Test
    {
        public string Url { get; set; }

        public IEnumerable<TestResult> TestResults { get; set; }
    }
}

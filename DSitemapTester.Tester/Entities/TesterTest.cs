using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Entities
{
    public class TesterTest
    {
        public string Url { get; set; }

        public DateTime Date { get; set; }

        public int TestsCount { get; set; }

        public IEnumerable<TesterTestResult> TestResults { get; set; }
    }
}

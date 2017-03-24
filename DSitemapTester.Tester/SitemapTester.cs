using DSitemapTester.Tester.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester
{
    public class SitemapTester : ISitemapTester
    {
        public ISitemapReader Reader { get; }
        public IPerformanceAnalyzer Analyzer { get; }

        public SitemapTester(ISitemapReader reader, IPerformanceAnalyzer analyzer)
        {
            this.Reader = reader;
            this.Analyzer = analyzer;
        }
    }
}

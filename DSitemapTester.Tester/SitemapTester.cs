using DSitemapTester.Tester.Interfaces;

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

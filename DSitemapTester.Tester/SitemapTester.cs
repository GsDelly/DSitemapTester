using DSitemapTester.Tester.Dtos;
using DSitemapTester.Tester.Entities;
using System.Collections.Generic;

namespace DSitemapTester.Tester
{
    public class SitemapTester
    {
        public WebResourceDto GetTestResults(string url)
        {
            SitemapReader reader = new SitemapReader();
            IEnumerable<Sitemap> urls = reader.GetSitemapUrls(url);

            PerformanceAnalyzer analyzer = new PerformanceAnalyzer();
            return null;
        }
    }
}

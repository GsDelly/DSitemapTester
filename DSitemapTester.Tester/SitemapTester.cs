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
            IList<string> sUrls = new List<string>();
             
            foreach (Sitemap sitemapUrl in urls)
            {
                sUrls.Add(sitemapUrl.Url);
            }

            PerformanceAnalyzer analyzer = new PerformanceAnalyzer();
            IEnumerable<Test> test = analyzer.GetConnectionResults(sUrls);
            return null;
        }
    }
}

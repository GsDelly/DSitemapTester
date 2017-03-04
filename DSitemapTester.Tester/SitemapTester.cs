using AutoMapper;
using DSitemapTester.Tester.Configuration.Automapper;
using DSitemapTester.Tester.Dtos;
using DSitemapTester.Tester.Entities;
using System.Collections.Generic;

namespace DSitemapTester.Tester
{
    public class SitemapTester
    {
        public WebResourceDto GetTestResults(string url)
        {
            AutomapperConfig.Configure();

            SitemapReader reader = new SitemapReader();
            IEnumerable<Sitemap> urls = reader.GetSitemapUrls(url);
           
            IList<string> sUrls = new List<string>();
             
            foreach (Sitemap sitemapUrl in urls)
            {
                sUrls.Add(sitemapUrl.Url);
            }

            PerformanceAnalyzer analyzer = new PerformanceAnalyzer();
            IEnumerable<Test> tests = analyzer.GetConnectionResults(sUrls);

            WebResourceDto webResourceDto = new WebResourceDto();
            IList<TestDto> testDto = new List<TestDto>();
            

            foreach (Test test in tests)
            {
                testDto.Add(Mapper.Map<Test, TestDto>(test));
            }

            webResourceDto.Url = url;
            webResourceDto.Tests = testDto;

            return webResourceDto;
        }
    }
}

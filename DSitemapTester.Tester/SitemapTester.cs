using AutoMapper;
using DSitemapTester.Tester.Configuration.Automapper;
using DSitemapTester.Tester.Dtos;
using DSitemapTester.Tester.Entities;
using DSitemapTester.Tester.Interfaces;
using System.Collections.Generic;

namespace DSitemapTester.Tester
{
    public class SitemapTester : ISitemapTester
    {
        private readonly ISitemapReader reader;
        private readonly IPerformanceAnalyzer analyzer;

        public SitemapTester(ISitemapReader reader, IPerformanceAnalyzer analyzer)
        {
            this.reader = reader;
            this.analyzer = analyzer;
        }

        public WebResourceDto GetTestResults(string url, int timeout, int testsCount)
        {
            AutomapperConfig.Configure();

            IEnumerable<Sitemap> urls = this.reader.GetSitemapUrls(url);
           
            IList<string> sUrls = new List<string>();
             
            foreach (Sitemap sitemapUrl in urls)
            {
                sUrls.Add(sitemapUrl.Url);
            }

            IEnumerable<Test> tests = this.analyzer.GetConnectionResults(sUrls, timeout, testsCount);

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

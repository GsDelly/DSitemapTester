using AutoMapper;
using DSitemapTester.BLL.Configuration;
using DSitemapTester.BLL.Dtos;
using DSitemapTester.BLL.Interfaces;
using DSitemapTester.BLL.Utilities;
using DSitemapTester.DAL.Interfaces;
using DSitemapTester.Entities.Entities;
using DSitemapTester.Tester.Configuration.Connection;
using DSitemapTester.Tester.Dtos;
using DSitemapTester.Tester.Entities;
using DSitemapTester.Tester.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DSitemapTester.BLL.Services
{
    public class TestService : ITestService
    {
        private readonly ISitemapTester tester;
        private readonly ISaveService saver;
        private readonly IUnitOfWork dataUnit;

        public TestService(ISitemapTester tester, ISaveService saver, IUnitOfWork dataUnit)
        {
            this.tester = tester;
            this.saver = saver;
            this.dataUnit = dataUnit;
        }

        public PresentationWebResourceTestDto GetTest(int testId)
        {
            PresentationAutomapperConfig.Configure();

            WebResourceTest test = this.dataUnit.GetRepository<WebResourceTest>().GetById(testId);

            PresentationWebResourceTestDto presentationTestResults = new PresentationWebResourceTestDto();

            presentationTestResults = Mapper.Map<WebResourceTest, PresentationWebResourceTestDto>(test);

            presentationTestResults.TotalTestsCount = presentationTestResults.Tests.Sum(res => res.TestsCount);
            presentationTestResults.TotalWrongTestsCount = presentationTestResults.Tests.Sum(res => res.WrongTestsCount);
            presentationTestResults.WrongUrls = presentationTestResults.Tests.Where(res => res.WrongTestsCount == res.TestsCount).Count();
            presentationTestResults.SuccessfulUrls = presentationTestResults.Tests.Where(res => res.WrongTestsCount == 0).Count();
            presentationTestResults.Tests = presentationTestResults.Tests.OrderByDescending(res => res.AverageResponseTime.ResponseTime).ToList();

            return presentationTestResults;
        }

        public int RunTest(string url, int timeout, int testsCount)
        {            
            PresentationAutomapperConfig.Configure();
            if (timeout == 0)
            {
                timeout = ConnectionSettings.GetTimeout();
            }
            if (testsCount == 0)
            {
                testsCount = ConnectionSettings.GetTestsCount();
            }
            double interval = ConnectionSettings.GetInterval();

            url = UrlAdaptor.GetUrl(url);

            IEnumerable<string> sUrls = this.tester.Reader.GetSitemapUrls(url);

            WebResourceTest webResourceTest = saver.GetNewTest(url);

            foreach (string sUrl in sUrls)
            {
                TesterTest test = this.tester.Analyzer.GetResult(sUrl, timeout, testsCount, interval);

                saver.SaveTestData(webResourceTest, test);               
            }

            return webResourceTest.Id;
        }

        public int Count(int testId)
        {
            int testsCount = this.dataUnit.GetRepository<WebResourceTest>().GetById(testId).Tests.Count();

            return testsCount;
        }
    }
}
